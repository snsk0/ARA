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
    public class PlayerTileButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public enum ButtonColor
        {
            Select,
            PreSelect,
            Movable,
            UnMovable,
        }

        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _preSelectColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _currentColor;
        [SerializeField] private Color _nonActiveColor;

        [SerializeField]
        private float _easingTime;

        private BehaviourSubject<Unit> _onClickSubject = new BehaviourSubject<Unit>();
        public IObservable<Unit> OnClickObservable => _onClickSubject;

        private Image _image;
        private Tween _tween;
        private Color _tempColor;

        private bool _interactable;
        private bool _isSelected;
        private bool _isCurrent;

        private void Awake()
        {
            _onClickSubject.AddTo(this);

            _image = GetComponent<Image>();

            _interactable = false;
        }

        //interactを無効化する
        public void SetInteractable(bool interactable)
        {
            _interactable = interactable;

            if (_interactable)
            {
                SetButtonColor(ButtonColor.Movable);
            }
            else
            {
                SetButtonColor(ButtonColor.UnMovable);
            }
        }

        private void SetButtonColor(ButtonColor color)
        {
            if(_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }

            switch (color)
            {
                case ButtonColor.Movable:
                    if (_isCurrent)
                    {
                        _image.color = _currentColor;
                    }
                    else
                    {
                        _image.color = _defaultColor;
                    }
                    break;

                case ButtonColor.UnMovable:
                    _image.color = _nonActiveColor;
                    break;

                case ButtonColor.PreSelect:
                    _image.color = _preSelectColor;
                    break;

                case ButtonColor.Select:
                    _image.color = _defaultColor;
                    _tween = _image.DOColor(_selectColor, _easingTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                    break;
            }
        }

        //選択時のリアクション
        public void SelectedReaction()
        {
            if (_interactable)
            {
                _isSelected = true;
                SetButtonColor(ButtonColor.Select);
            }
            else
            {
                throw new Exception("Not Active Button Selected");
            }
        }

        //選択解除
        public void CanselReaction()
        {
            if (_interactable)
            {
                _isSelected = false;
                SetButtonColor(ButtonColor.Movable);
            }
            else
            {
                throw new Exception("Not Active Button Selected");
            }
        }

        //失敗時のリアクション
        public void FailedReaction()
        {
            if (_interactable)
            {
                Debug.Log("FaildReaction");
            }
            else
            {
                throw new Exception("Not Active Button Selected");
            }
        }

        //現在選択
        public void SelectCurrent(bool isCurrent)
        {
            _isCurrent = isCurrent;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isSelected && _interactable)
            {
                _tempColor = _image.color;
                _image.color = _preSelectColor;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isSelected && _interactable)
            {
                _image.color = _tempColor;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //イベントを発行
            if (_interactable)
            {
                _onClickSubject.OnNext(Unit.Default);
            }
        }
    }
}
