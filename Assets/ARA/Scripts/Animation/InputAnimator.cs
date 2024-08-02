using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ARA.Presenter;
using DG.Tweening;

namespace ARA.Animation
{
    public class InputAnimator : MonoBehaviour, IInputAnimator
    {
        [SerializeField]
        private GameObject _playerObject;

        [SerializeField]
        private GameObject _transparentObject;

        [SerializeField]
        private List<Transform> _transforms;

        private int _currentIndexTemp;

        public void Initialize(int currentIndex)
        {
            _currentIndexTemp = currentIndex;
        }

        public void PlayPreMoveAnimation(int index)
        {
            if(_currentIndexTemp != index)
            {
                _transparentObject.SetActive(true); 
                _transparentObject.transform.position = _transforms[_currentIndexTemp].position;
                _transparentObject.transform.DOMove(_transforms[index].position, 1.0f);
            }
            else
            {
                _transparentObject.SetActive(false);
            }
        }

        private void Awake()
        {
            _transparentObject.SetActive(false);
        }


    }
}
