using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARA.UI
{
    public class EnemyTileView : MonoBehaviour
    {
        [SerializeField] private Color _unMovableColor;
        [SerializeField] private Color _movableBaseColor;
        [SerializeField] private Color _movableFadeColor;
        [SerializeField] private Color _currentColor;
        [SerializeField] private float _easingTime;

        private Image _image;
        private Tween _tween;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetMovable()
        {
            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }
            _image.color = _movableBaseColor;
            _tween = _image.DOColor(_movableFadeColor, _easingTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }

        public void SetUnMovable()
        {
            if(_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }
            _image.color = _unMovableColor;
        }

        public void SetCurrent()
        {
            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }
            _image.color = _currentColor;
        }
    }
}
