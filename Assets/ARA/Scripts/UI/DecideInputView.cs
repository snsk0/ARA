using ARA.Presenter;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ARA.UI
{
    public class DecideInputView : MonoBehaviour, IDecideInputView, IWaitingInputReceivable
    {
        [SerializeField] private Button _button;

        private Subject<Unit> _inputSubject = new Subject<Unit>();
        public IObservable<Unit> InputObservable => _inputSubject;

        private bool _isDecidable = false;

        private void Awake()
        {
            _inputSubject.AddTo(this);

            _button.onClick.AddListener(() =>
            {
                if (_isDecidable)
                {
                    _inputSubject.OnNext(Unit.Default);
                }
                else
                {
                    //TODO 決定できないシステムメッセージを流す
                }
            });
        }

        public void SetDesidable(bool isDecidable)
        {
            _isDecidable |= isDecidable;
        }

        public void NotfyWaitingInput(bool isWaitingInput)
        {
            gameObject.SetActive(isWaitingInput);
        }
    }
}
