using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System;

namespace ARA.Grid
{
    public class GridMovable
    {
        public GridMovable()
        {
            _disposables = new CompositeDisposable();

            _gridField = new ReactiveProperty<GridField>();
            _currentPosition = new ReactiveProperty<Vector2Int>();
            _movablePositions = new List<Vector2Int>();
            _movablePositionSubject = new Subject<IReadOnlyList<Vector2Int>>();
            
            _disposables.Add(_gridField);
            _disposables.Add(_currentPosition);
            _disposables.Add(_movablePositionSubject);
        }

        ~GridMovable()
        {
            _disposables.Dispose();
        }

        private CompositeDisposable _disposables;

        private ReactiveProperty<GridField> _gridField;
        public IReadOnlyReactiveProperty<GridField> GridField => _gridField;

        private ReactiveProperty<Vector2Int> _currentPosition;
        public IReadOnlyReactiveProperty<Vector2Int> CurrentPosition => _currentPosition;

        private List<Vector2Int> _movablePositions;
        private Subject<IReadOnlyList<Vector2Int>> _movablePositionSubject;
        public IObservable<IReadOnlyList<Vector2Int>> MovablePositionObservable => _movablePositionSubject;

        private int _moveRange;

        public void Initialize(GridField gridField, Vector2Int initialPosition)
        {
            _gridField.Value = gridField;
            _currentPosition.Value = initialPosition;

            UpdateMovablePositions();
        }

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
            _movablePositions = _gridField.Value.GetMovablePositions(_currentPosition.Value, _moveRange);
            _movablePositionSubject.OnNext(_movablePositions);
        }
    }
}
