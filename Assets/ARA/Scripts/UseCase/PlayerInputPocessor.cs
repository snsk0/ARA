using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

namespace ARA
{
    public class PlayerInputPocessor : MonoBehaviour, IPlayerInputProcessor, IPlayerInputEventProvider
    {
        private Subject<MoveInputResult> _moveInputSubject;
        private Subject<SelectCardResult> _selectCardInputSubject;
        private Subject<PlayerParameter> _resetInputSubject;

        public IObservable<MoveInputResult> MoveInputObservable => _moveInputSubject;
        public IObservable<SelectCardResult> SelectCardInputObservable => _selectCardInputSubject;
        public IObservable<PlayerParameter> ResetInputObservable => _resetInputSubject;

        private int _moveInputCashe;

        private bool _isDecided;

        public void MoveInput(int index)
        {
            //テストコード 必ず移動に成功する
            MoveInputResult result = new MoveInputResult(index, true);

            if(result.IsSuccess)
            {
                _moveInputCashe = result.ToMoveIndex;
            }

            _moveInputSubject.OnNext(result);
        }

        public void SelectCard(SelectActionType actionType, int cardId)
        {

        }

        public void CancelCard(SelectActionType actionType, int cardId)
        {

        }

        public void DecideInput()
        {

        }

        public void ResetInput(PlayerParameter parameter)
        {
            _moveInputCashe = parameter.CurrentIndex;
        }

        public async UniTask<PlayerInput> GetInputAsync()
        {
            await UniTask.WaitUntil(() => _isDecided);
            return new PlayerInput();
        }
    }
}
