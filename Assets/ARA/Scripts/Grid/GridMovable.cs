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
            GridField = gridField;

            _disposables = new CompositeDisposable();

            _currentPosition = new ReactiveProperty<Vector2Int>(initialPosition);
            _movablePositions = new List<Vector2Int>();
            _movablePositionSubject = new BehaviourSubject<IReadOnlyList<Vector2Int>>();
            
            _disposables.Add(_currentPosition);
            _disposables.Add(_movablePositionSubject);

            UpdateMovablePositions();
        }

        ~GridMovable()
        {
            _disposables.Dispose();
        }
        public readonly GridField GridField;

        private CompositeDisposable _disposables;

        private ReactiveProperty<Vector2Int> _currentPosition;
        public IReadOnlyReactiveProperty<Vector2Int> CurrentPosition => _currentPosition;

        private List<Vector2Int> _movablePositions;
        private BehaviourSubject<IReadOnlyList<Vector2Int>> _movablePositionSubject;
        public IObservable<IReadOnlyList<Vector2Int>> MovablePositionObservable => _movablePositionSubject;

        private int _moveRange;

        public void SetMoveRange(int moveRange)
        {
            _moveRange = moveRange;
            UpdateMovablePositions();
        }

        public void Move(Vector2Int target) 
        {
            if (_movablePositions.Contains(target))
            {
                _currentPosition.Value = target;
                UpdateMovablePositions();
            }
            else
            {
                throw new Exception("Input Cant Movable Error");
            }
        }

        private void UpdateMovablePositions()
        {
            _movablePositions = GridField.GetMovablePositions(_currentPosition.Value, _moveRange);
            _movablePositionSubject.OnNext(_movablePositions);
        }
    }
}
