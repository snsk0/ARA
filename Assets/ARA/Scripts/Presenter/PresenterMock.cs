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

        private IMoveInputView _moveInputView => MoveSelectGrid;
        private IInputAnimator _inputAnimator => InputAnimator;

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
            });

            player.GridMovable.CurrentPosition.Subscribe(position =>
            {
                _moveInputView.UpdateUI(player.GridMovable.CurrentPosition.Value, player.GridMovable.MovablePositions);
            });

            _moveInputView.ToMoveObservable.Subscribe(position =>
            {
                _moveInputView.ReceiveInputResult(position, player.GridMovable.MovablePositions.Contains(position));
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
