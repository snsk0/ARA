using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System;

namespace ARA.Grid
{
    //TODO 将来的にはボディサイズを追加する
    public class GridMovable : GridField.IGridMovable
    {
        public GridMovable(GridField gridField, Vector2Int initialPosition)
        {
            _owner = gridField;

            _disposables = new CompositeDisposable();
            _currentPosition = new ReactiveProperty<Vector2Int>();
            _moveRange = new ReactiveProperty<int>();
            
            _disposables.Add(_currentPosition);

            _currentPosition.Value = initialPosition;

            gridField.RegisterGridMovable(this);
        }

        ~GridMovable()
        {
            _disposables.Dispose();
        }

        private readonly GridField _owner;
        public GridField Owner => _owner;

        private CompositeDisposable _disposables;

        private ReactiveProperty<Vector2Int> _currentPosition;
        public IReadOnlyReactiveProperty<Vector2Int> CurrentPosition => _currentPosition;

        private ReactiveProperty<int> _moveRange;
        public IReactiveProperty<int> MoveRange => _moveRange;

        public void SetMoveRange(int moveRange)
        {
            _moveRange.Value = moveRange;
        }

        public void Move(Vector2Int target) 
        {
            if (_owner.GetMovablePositions(this).Contains(target))
            {
                _currentPosition.SetValueAndForceNotify(target);
            }
            else
            {
                throw new Exception("Input Cant Movable Error");
            }
        }

        public List<Vector2Int> GetMovablePositions()
        {
            return _owner.GetMovablePositions(this);
        }
    }
}
