using ARA.Presenter;
using ARA.UI;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ARA.Animation;
using ARA.Grid;
using ARA.Player;
using System.Linq;

namespace ARA.Mock
{
    public class PresenterMock : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Vector2Int _initialPosition;

        [SerializeField] private MoveSelectGrid MoveSelectGrid;
        [SerializeField] private InputAnimator InputAnimator;
        [SerializeField] private GridFloatView GridFloatView;

        private IMoveInputView _moveInputView => MoveSelectGrid;
        private IInputAnimator _inputAnimator => InputAnimator;
        private IGridFloatView _gridFloatView => GridFloatView;

        private void Awake()
        {
            //必要オブジェクトを生成する
            GridField gridField = new GridField(_gridSize);
            GridMovable movable = new GridMovable(gridField, _initialPosition);
            PlayerCore player = new PlayerCore(new PlayerParameter(), movable);

            //Event紐づけ
            player.GridMovable.GridField.GridSize.Subscribe(size =>
            {
                _moveInputView.Initialize(size);
                _gridFloatView.Initialize(size);
            });

            player.GridMovable.CurrentPosition.Subscribe(position =>
            {
                _moveInputView.UpdateUI(player.GridMovable.CurrentPosition.Value, player.GridMovable.MovablePositions);
            });

            _moveInputView.ToMoveObservable.Subscribe(position =>
            {
                bool isSuceed = player.GridMovable.MovablePositions.Contains(position);
                _moveInputView.ReceiveInputResult(position, isSuceed);

                if (isSuceed)
                {
                    _inputAnimator.PlayPreMoveAnimation(player.GridMovable.CurrentPosition.Value, position);
                }
            });
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
        }
    }
}
