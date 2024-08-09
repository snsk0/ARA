using UnityEngine;
using ARA.Presenter;
using DG.Tweening;

namespace ARA.Animation
{
    public class InputAnimator : MonoBehaviour, IInputAnimator
    {
        [SerializeField]
        private Animator _animationObject;

        [SerializeField]
        private GridFloatView _gridFloatView;

        private Tween tweenCashe;

        private void Awake()
        {
            _animationObject.gameObject.SetActive(false);
        }

        public void PlayPreMoveAnimation(Vector2Int fromPosition, Vector2Int toPosition)
        {
            if(fromPosition == toPosition)
            {
                _animationObject.gameObject.SetActive(false);
            }
            else
            {
                //çƒê∂íÜÇ»ÇÁÉ^ÉXÉLÉã
                if (tweenCashe != null && tweenCashe.active)
                {
                    tweenCashe.Kill();
                    tweenCashe = null;
                }
                _animationObject.gameObject.SetActive(true);
                _animationObject.transform.position = _gridFloatView.Transforms[fromPosition].position;
                tweenCashe = _animationObject.transform.DOMove(_gridFloatView.Transforms[toPosition].position, 1.0f).SetEase(Ease.InOutQuart);
                _animationObject.SetTrigger("Move");
            }
        }
    }
}
