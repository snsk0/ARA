using UnityEngine;
using UnityEngine.UI;
using ARA.Presenter;
using System;
using UniRx;

namespace ARA.UI
{
    public class UIManager : MonoBehaviour, IDecidableView
    {
        [SerializeField]
        private Button _decideButton;

        [SerializeField]
        private PlayerTileInputManager _moveSelectGrid;

        private bool _isDesidable = false;

        private Subject<Unit> _decideSubject = new Subject<Unit>();
        public IObservable<Unit> DecideObservable => _decideSubject;

        private void Awake()
        {
            _decideSubject.AddTo(this);

            _decideButton.onClick.AddListener(() =>
            {
                if (_isDesidable)
                {
                }
                _decideSubject.OnNext(Unit.Default);
            });
        }

        public void SetDesidable(bool isDecidable)
        {
            _isDesidable = isDecidable;
        }
    }
}