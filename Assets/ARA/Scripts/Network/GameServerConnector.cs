using ARA.Animation;
using ARA.Game;
using ARA.InputHandle;
using ARA.Player;
using ARA.Presenter;
using ARA.UI;
using Unity.Netcode;
using UnityEngine;

namespace ARA.Network
{
    //同期用オブジェクト
    public class GameServerConnector : NetworkBehaviour
    {
        [SerializeField] private MoveSelectGrid MoveSelectGrid;
        [SerializeField] private InputAnimator InputAnimator;
        [SerializeField] private GridFloatView GridFloatView;
        [SerializeField] private UIManager _uiManager;

        private IMoveInputView _moveInputView => MoveSelectGrid;
        private IInputAnimator _inputAnimator => InputAnimator;
        private IGridFloatView _gridFloatView => GridFloatView;
        private IDecidableView _decidableView => _uiManager;

        private PlayerCore _player;

        //クライアントコード
        [Rpc(SendTo.ClientsAndHost)]
        public void InitializeGameRpc(Vector2Int gridSize, Vector2Int initialPosition)
        {
            //必要オブジェクトを生成する
            GridField gridField = new GridField(gridSize);
            GridTransform movable = new GridTransform(gridField, initialPosition);
            InputHandler inputHandler = new InputHandler();
            PlayerCore player = new PlayerCore(new PlayerParameter(), movable);

            //Presenter層の生成
            new PlayerPresenter(player, _moveInputView, _gridFloatView);
            new PlayerInputController(inputHandler, _moveInputView);
            new PlayerInputPresenter(inputHandler, player, _moveInputView, _inputAnimator);

            Debug.LogError(player);
        } 
    }
}
