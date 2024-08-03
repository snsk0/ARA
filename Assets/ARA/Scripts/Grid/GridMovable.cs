using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System;

namespace ARA.Grid
{
    public class GridMovable
    {
        public GridMovable(GridField gridField, Vector2Int initialPosition)
        {
            Owner = gridField;

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

        public readonly GridField Owner;

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
            if (Owner.GetMovablePositions(this).Contains(target))
            {
                _currentPosition.Value = target;
            }
            else
            {
                throw new Exception("Input Cant Movable Error");
            }
        }

        public List<Vector2Int> GetMovablePositions()
        {
            return Owner.GetMovablePositions(this);
        }
    }
}
