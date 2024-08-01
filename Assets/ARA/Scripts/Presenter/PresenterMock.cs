using ARA.Presenter;
using ARA.UI;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ARA.Mock
{
    public class PresenterMock : MonoBehaviour
    {
        [SerializeField]
        private MoveSelectGrid _grid;

        [SerializeField]
        private int _currentIndex;

        [SerializeField]
        private List<bool> _actives;

        [SerializeField]
        private int _gridSize;

        private IMoveInputView _view => _grid;

        private void Awake()
        {
            _view.Initialize(_gridSize, _gridSize, _actives, _currentIndex);

            _view.ToMoveObservable.Subscribe(index =>
            {
                _view.ReceiveInputResult(index, _actives[index]);
            });
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Space)) 
            {
                _view.SetActive(true);
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                _view.SetActive(false);
            }
        }
    }
}
