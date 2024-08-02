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
            _currentPosition = new ReactiveProperty<Vector2Int>();
            _movablePositions = new List<Vector2Int>();
            
            _disposables.Add(_currentPosition);

            SetCurrentPosition(initialPosition);
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
        public IReadOnlyList<Vector2Int> MovablePositions => _movablePositions;

        private int _moveRange;

        public void SetMoveRange(int moveRange)
        {
            _moveRange = moveRange;

            UpdateMovablePositions(_currentPosition.Value);
        }

        public void Move(Vector2Int target) 
        {
            if (_movablePositions.Contains(target))
            {
                SetCurrentPosition(target);
            }
            else
            {
                throw new Exception("Input Cant Movable Error");
            }
        }

        private void SetCurrentPosition(Vector2Int currentPosition)
        {
            UpdateMovablePositions(currentPosition);

            //パラメータを更新
            _currentPosition.Value = currentPosition;
        }

        private void UpdateMovablePositions(Vector2Int Position)
        {
            //移動可能座標を取得
            _movablePositions = GridField.GetMovablePositions(Position, _moveRange);
        }
    }
}
