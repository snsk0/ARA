using ARA.Animation;
using ARA.Game;
using ARA.InputHandle;
using ARA.Presenter;
using ARA.UI;
using System;
using Unity.Netcode;
using UnityEngine;
using System.Linq;

namespace ARA.Network
{
    //同期用オブジェクト
    public class GameServerConnector : NetworkBehaviour, INetworkSendInterface
    {
        [SerializeField] private PlayerTileInputManager MoveSelectGrid;
        [SerializeField] private EnemyTileViewManager EnemySelectGrid;
        [SerializeField] private InputAnimator InputAnimator;
        [SerializeField] private GridFloatView GridFloatView;
        [SerializeField] private GridFloatView EGridFloatView;
        [SerializeField] private DecideInputView DecideInputView;
        [SerializeField] private TurnUI _waitingUi;
        [SerializeField] private ResultAnimation _resultAnimation;
        [SerializeField] private PlayerCardInputManager _playerCardSelectView;
        [SerializeField] private PlayerCardInputManager _enemyCardSelectView;

        private ITilePositionInputView _moveInputView => MoveSelectGrid;
        private IInputAnimator _inputAnimator => InputAnimator;
        private ITilePositionFloatView _gridFloatView => GridFloatView;

        private GameClientManager _context;
        private INetworkReciveInterface _receiveInterface;

        public event Action<NetworkInput, RpcParams> OnInputEvent;

        public override void OnNetworkSpawn()
        {
            //InitializeGameRpc(new Vector2Int(3, 3), new Vector2Int(1, 1));
        }

        //クライアントコード
        [Rpc(SendTo.SpecifiedInParams)]
        public void InitializeGameRpc(Vector2Int gridSize, NetworkResult playerResult, NetworkResult enemyResult, RpcParams rpcParams)
        {
            //初期化
            _moveInputView.Initialize(gridSize);
            _gridFloatView.Initialize(gridSize);
            EnemySelectGrid.Initialize(gridSize);
            EGridFloatView.Initialize(gridSize);

            _moveInputView.UpdateView(playerResult.Position, playerResult.MovablePositions);
            EnemySelectGrid.UpdateView(enemyResult.Position, enemyResult.MovablePositions);

            _playerCardSelectView.SetDeckList(playerResult.UsableCardIds);
            _enemyCardSelectView.SetDeckList(enemyResult.UsableCardIds);

            InputHandler inputHandler = new InputHandler();

            new InputPresenter(inputHandler, _moveInputView, DecideInputView, _inputAnimator, new IWaitingInputReceivable[] { MoveSelectGrid, DecideInputView, _waitingUi });

            //GameManagerの生成
            _context = new GameClientManager(playerResult, inputHandler, this, _resultAnimation);
            _receiveInterface = _context;

            //ゲームの開始
            _context.StartGameLoop();
        }

        //特定クライアントのみに返す
        [Rpc(SendTo.SpecifiedInParams)]
        public void ProcessResultRpc(NetworkResult playerResult, NetworkResult enemyResult, RpcParams rpcParams)
        {
            //rpcParamによって自動的にクライアントに送信される

            _receiveInterface.ProcessResult(playerResult, enemyResult);
        }

        //サーバーコード
        [Rpc(SendTo.Server)]
        private void ProcessInputRpc(NetworkInput input, RpcParams rpcParams = default)
        {
            OnInputEvent.DynamicInvoke(input, rpcParams);
        }

        //インタフェースの実装
        public void ProcessInput(NetworkInput input)
        {
            ProcessInputRpc(input);
        }
    }
}
