using ARA.Animation;
using ARA.Game;
using ARA.InputHandle;
using ARA.Character;
using ARA.Presenter;
using ARA.UI;
using System;
using Unity.Netcode;
using UnityEngine;

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

        private ITilePositionInputView _moveInputView => MoveSelectGrid;
        private IInputAnimator _inputAnimator => InputAnimator;
        private IGridFloatView _gridFloatView => GridFloatView;

        private GameManager _context;
        private INetworkReciveInterface _receiveInterface;

        public event Action<NetworkInput, RpcParams> OnInputEvent;

        public override void OnNetworkSpawn()
        {
            InitializeGameRpc(new Vector2Int(3, 3), new Vector2Int(1, 1));
        }

        //クライアントコード
        [Rpc(SendTo.ClientsAndHost)]
        public void InitializeGameRpc(Vector2Int gridSize, Vector2Int initialPosition)
        {
            //必要オブジェクトを生成する
            TileMap gridField = new TileMap(gridSize);
            TilePosition transform = new TilePosition(gridField, initialPosition);
            InputHandler inputHandler = new InputHandler();
            CharacterCore player = new CharacterCore(new CharacterParam(), transform);

            TileMap enemyGridField = new TileMap(gridSize);
            TilePosition enemyTransform = new TilePosition(enemyGridField, initialPosition);
            CharacterCore enemy = new CharacterCore(new CharacterParam(), enemyTransform);

            //Presenter層の生成
            new CharacterPresenter(player, _moveInputView, _gridFloatView);
            new CharacterPresenter(enemy, EnemySelectGrid, EGridFloatView);
            new InputPresenter(inputHandler, player, _moveInputView, DecideInputView, _inputAnimator, new IWaitingInputReceivable[] { MoveSelectGrid, DecideInputView, _waitingUi });

            //GameManagerの生成
            _context = new GameManager(inputHandler, player, enemy, this, _resultAnimation);
            _receiveInterface = _context;

            //ゲームの開始
            _context.StartGameLoop();
        }

        //特定クライアントのみに返す
        [Rpc(SendTo.SpecifiedInParams)]
        public void ProcessResultRpc(NetworkResult result, RpcParams rpcParams)
        {
            //rpcParamによって自動的にクライアントに送信される

            _receiveInterface.ProcessResult(result);
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
