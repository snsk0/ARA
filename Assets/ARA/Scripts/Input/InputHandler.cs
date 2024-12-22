using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

namespace ARA.InputHandle
{
    public class InputHandler
    {
        private ReactiveProperty<bool> _isInputWaiting = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsInputWaiting => _isInputWaiting;

        //tilePosition�̓��͒l
        private Vector2Int _tilePositionCashe;
        public Vector2Int TilePositionCashe => _tilePositionCashe;  //IInputAnimator�Q�Ɨp
        private Subject<InputResult<Vector2Int>> _tilePositionInputSubject = new Subject<InputResult<Vector2Int>>();
        public IObservable<InputResult<Vector2Int>> TilePositionInputObservable => _tilePositionInputSubject;

        //�J�[�h�I��Index
        private int _selectDeckIndex;
 
        //decide���\�ȏ�Ԃ��ǂ���
        private ReactiveProperty<bool> _isDecidable = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsDecidable => _isDecidable;

        public void TilePositionInput(Vector2Int position)
        {
            if(!_isInputWaiting.Value)
            {
                return;
            }

            bool isSucceed = position != _tilePositionCashe;
            _tilePositionCashe = position;

            InputResult<Vector2Int> inputResult = new InputResult<Vector2Int>(position, isSucceed);
            _tilePositionInputSubject.OnNext(inputResult);
        }

        public void DecideInput()
        {
            if(_isInputWaiting.Value && _isDecidable.Value)
            {
                _isInputWaiting.Value = false; 
            }
        }

        public async UniTask<InputContainer> StartWaitInput(Vector2Int defaultInputPosition)
        {
            //input��������
            _tilePositionCashe = defaultInputPosition;
            _selectDeckIndex = 0;
            _isDecidable.Value = false;

            //�ҋ@���J�n
            _isInputWaiting.Value = true;

            //���R�[�h
            _isDecidable.Value = true;

            await UniTask.WaitWhile(() => _isInputWaiting.Value);

            return new InputContainer(_tilePositionCashe, _selectDeckIndex);
        }
    }
}
