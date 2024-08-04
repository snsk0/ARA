using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace ARA.UI 
{
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class MoveSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private Color _defaultColor;

        [SerializeField]
        private Color _preSelectColor;

        [SerializeField]
        private Color _selectColor;

        [SerializeField]
        private Color _nonActiveColor;

        [SerializeField]
        private float _easingTime;

        private BehaviourSubject<Unit> _onClickSubject = new BehaviourSubject<Unit>();
        public IObservable<Unit> OnClickObservable => _onClickSubject;

        private Image _image;

        private bool _active;
        private bool _isSelected;

        private void Awake()
        {
            _onClickSubject.AddTo(this);

            _image = GetComponent<Image>();
            _image.color = _defaultColor;

            _active = true;
        }

        //選択無効化する
        public void SetActive(bool active)
        {
            _active = active;

            if (_active)
            {
                _image.color = _defaultColor;
            }
            else
            {
                _image.color = _nonActiveColor;
            }
        }

        //選択時のリアクション
        public async void SelectedReaction()
        {
            if (_active)
            {
                _isSelected = true;

                //Selectが解除されるまで点滅
                _image.color = _preSelectColor;
                var tween = _image.DOColor(_selectColor, _easingTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                await UniTask.WaitWhile(() => _isSelected);

                tween.Kill();
                _image.color = _defaultColor;
            }
            else
            {
                throw new Exception("Not Active Button Selected");
            }
        }

        //選択解除
        public void CanselReaction()
        {
            _isSelected = false;
        }

        //失敗時のリアクション
        public void FailedReaction()
        {
            Debug.Log("FaildReaction");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isSelected)
            {
                if (_active)
                {
                    _image.color = _preSelectColor;
                }
                else
                {
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isSelected)
            {
                if (_active)
                {
                    _image.color = _defaultColor;
                }
                else
                {
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //イベントを発行
            if (_active)
            {
                _onClickSubject.OnNext(Unit.Default);
            }
        }
    }
}
