using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ARA.Character
{
    //TODO 将来的にはボディサイズを追加する
    public class TilePosition : TileMap.ITilePosition
    {
        public TilePosition(TileMap gridField, Vector2Int initialPosition)
        {
            _owner = gridField;

            _disposables = new CompositeDisposable();
            _currentPosition = new ReactiveProperty<Vector2Int>();
            _moveRange = new ReactiveProperty<int>();
            
            _disposables.Add(_currentPosition);

            _currentPosition.Value = initialPosition;

            gridField.RegisterGridMovable(this);
        }

        ~TilePosition()
        {
            _disposables.Dispose();
        }

        private readonly TileMap _owner;
        public TileMap Owner => _owner;

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

        public IReadOnlyList<Vector2Int> GetMovablePositions()
        {
            return _owner.GetMovablePositions(this);
        }
    }
}
