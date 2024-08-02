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

        //TODO ŽÀ‘•
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
                throw new System.Exception("Input Cant Movable Error");
            }
        }

        //‰¼ŽÀ‘• –{—ˆ‚ÍGridField‚ðŽQÆ‚µ‚È‚ª‚çˆÚ“®‚Å‚«‚éêŠ‚ð’T‚·
        private void UpdateMovablePositions()
        {
            Vector2Int vector0 = _currentPosition.Value;
            Vector2Int vector1 = _currentPosition.Value + Vector2Int.left;
            Vector2Int vector2 = _currentPosition.Value + Vector2Int.right;
            Vector2Int vector3 = _currentPosition.Value + Vector2Int.up;
            Vector2Int vector4 = _currentPosition.Value + Vector2Int.down;

            _movablePositions.Clear();
            _movablePositions.Add(vector0);
            if(vector1.x >= 0 && vector1.y >= 0)
            {
                _movablePositions.Add(vector1);
            }
            if (vector2.x >= 0 && vector2.y >= 0)
            {
                _movablePositions.Add(vector2);
            }
            if (vector3.x >= 0 && vector3.y >= 0)
            {
                _movablePositions.Add(vector3);
            }
            if (vector4.x >= 0 && vector4.y >= 0)
            {
                _movablePositions.Add(vector4);
            }

            _movablePositionSubject.OnNext(_movablePositions);
        }
    }
}
