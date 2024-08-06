using ARA.Presenter;
using ARA.UI;
using UnityEngine;
using ARA.Animation;
using ARA.Grid;
using ARA.Player;
using ARA.Game;
using UniRx;

namespace ARA.Mock
{
    public class InitializerMock : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Vector2Int _initialPosition;

        [SerializeField] private MoveSelectGrid MoveSelectGrid;
        [SerializeField] private InputAnimator InputAnimator;
        [SerializeField] private GridFloatView GridFloatView;
        [SerializeField] private SystemMassageManager Manager;
        [SerializeField] private UIManager _uiManager;

        private IMoveInputView _moveInputView => MoveSelectGrid;
        private IInputAnimator _inputAnimator => InputAnimator;
        private IGridFloatView _gridFloatView => GridFloatView;
        private IGameAnimationPlayer _animatorPlayer;
        private IDecidableView _decidableView => _uiManager;

        private PlayerCore _player;
        private void Awake()
        {
            //必要オブジェクトを生成する
            GridField gridField = new GridField(_gridSize);
            GridTransform movable = new GridTransform(gridField, _initialPosition);
            //GridMovable movable2 = new GridMovable(gridField, new Vector2Int(1,0)); 被りテスト
            PlayerInputHandler inputHandler = new PlayerInputHandler();
            PlayerCore player = new PlayerCore(new PlayerParameter(), movable, inputHandler);

            //Presenter層の生成
            new PlayerPresenter(player, _moveInputView, _gridFloatView);
            new PlayerInputController(inputHandler, _moveInputView);
            new PlayerInputPresenter(player, _moveInputView, _inputAnimator);

            _decidableView.DecideObservable.Subscribe(v =>
            {
                Debug.Log("test");
                player.InputHandler.DecideInput();
            });

            new GameManager(new PlayerCore[] { player }).StartGameLoop();
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
                _player.GridTransform.Move(new Vector2Int(1, 0));
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Manager.DisplaySystemMassage("Test");
            }
        }
    }
}
