using UnityEngine;
using ARA.Presenter;
using DG.Tweening;

namespace ARA.Animation
{
    public class InputAnimator : MonoBehaviour, IInputAnimator
    {
        [SerializeField]
        private GameObject _transparentObject;

        [SerializeField]
        private GridFloatView _gridFloatView;

        private Tween tweenCashe;
        private void Awake()
        {
            _transparentObject.SetActive(false);
        }

        public void PlayPreMoveAnimation(Vector2Int fromPosition, Vector2Int toPosition)
        {
            if(fromPosition == toPosition)
            {
                _transparentObject.SetActive(false);
            }
            else
            {
                //çƒê∂íÜÇ»ÇÁÉ^ÉXÉLÉã
                if (tweenCashe != null && tweenCashe.active)
                {
                    tweenCashe.Kill();
                    tweenCashe = null;
                }
                _transparentObject.SetActive(true);
                _transparentObject.transform.position = _gridFloatView.Transforms[fromPosition].position;
                tweenCashe = _transparentObject.transform.DOMove(_gridFloatView.Transforms[toPosition].position, 1.0f);
            }
        }
    }
}
