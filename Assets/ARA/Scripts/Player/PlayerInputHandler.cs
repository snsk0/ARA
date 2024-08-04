using UnityEngine;
using UniRx;
using System;

namespace ARA.Player
{
    public class PlayerInputHandler
    {
        private bool _isInputWaiting = true;

        private Vector2Int _inputPosition;
        private Subject<InputResult<Vector2Int>> _moveInputSubject = new Subject<InputResult<Vector2Int>>();
        public IObservable<InputResult<Vector2Int>> MoveInputObservable => _moveInputSubject;
 
        public void MoveInput(Vector2Int position)
        {
            if(!_isInputWaiting)
            {
                return;
            }

            bool isSucceed = position != _inputPosition;
            _inputPosition = position;

            InputResult<Vector2Int> inputResult = new InputResult<Vector2Int>(position, isSucceed);
            _moveInputSubject.OnNext(inputResult);
        }

        public void StartWaitInput(Vector2Int defaultInputPosition)
        {
            _isInputWaiting = true;

            _inputPosition = defaultInputPosition;

            _isInputWaiting = false;
        }
    }
}
