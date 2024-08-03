using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using System;

namespace ARA.Player
{
    public class PlayerInputHandler
    {
        public PlayerInputHandler(PlayerCore playerCore)
        { 
            Owner = playerCore;
        }

        public readonly PlayerCore Owner;

        private bool _isInputWaiting;

        private Vector2Int _inputPosition;
        private bool _isMoveInputed;
        private Subject<InputResult<Vector2Int>> _moveInputSubject = new Subject<InputResult<Vector2Int>>();
        public IObservable<InputResult<Vector2Int>> MoveInputObservable => _moveInputSubject;
 
        public void MoveInput(Vector2Int position)
        {
            if(_isInputWaiting)
            {
                return;
            }

            bool isSucceed = Owner.GridMovable.GetMovablePositions().Contains(position);

            if (isSucceed)
            {
                _isMoveInputed = true;
                _inputPosition = position;
            }

            InputResult<Vector2Int> inputResult = new InputResult<Vector2Int>(position, isSucceed);
            _moveInputSubject.OnNext(inputResult);
        }

        public async void StartWaitInput()
        {
            _isInputWaiting = true;

            _isMoveInputed = false;
            _inputPosition = Owner.GridMovable.CurrentPosition.Value;

            await UniTask.WaitUntil(() => _isMoveInputed);

            _isInputWaiting = false;
        }
    }
}
