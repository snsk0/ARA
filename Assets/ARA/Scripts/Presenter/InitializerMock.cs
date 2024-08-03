using ARA.Presenter;
using ARA.UI;
using UnityEngine;
using UniRx;
using ARA.Animation;
using ARA.Grid;
using ARA.Player;

namespace ARA.Mock
{
    public class InitializerMock : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Vector2Int _initialPosition;

        [SerializeField] private MoveSelectGrid MoveSelectGrid;
        [SerializeField] private InputAnimator InputAnimator;
        [SerializeField] private GridFloatView GridFloatView;

        private IMoveInputView _moveInputView => MoveSelectGrid;
        private IInputAnimator _inputAnimator => InputAnimator;
        private IGridFloatView _gridFloatView => GridFloatView;

        private PlayerCore _player;
        private void Awake()
        {
            //必要オブジェクトを生成する
            GridField gridField = new GridField(_gridSize);
            GridMovable movable = new GridMovable(gridField, _initialPosition);
            //GridMovable movable2 = new GridMovable(gridField, new Vector2Int(1,0)); 被りテスト
            PlayerCore player = new PlayerCore(new PlayerParameter(), movable);
            PlayerInputHandler inputHandler = new PlayerInputHandler(player);

            //Presenter層の生成
            new PlayerPresenter(player, _moveInputView, _gridFloatView);
            new PlayerInputController(inputHandler, _moveInputView);
            new PlayerInputPresenter(inputHandler, _moveInputView, _inputAnimator);

            _player = player;
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Space)) 
            {
                _moveInputView.SetActive(true);
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                _moveInputView.SetActive(false);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                //プレイヤーの移動を決定してみる
                _player.GridMovable.Move(new Vector2Int(1, 0));
            }
        }
    }
}
