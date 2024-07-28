using UnityEngine;
using UniRx;

namespace ARA
{
    public class PlayerInputController : MonoBehaviour
    {
        private IMoveSelectView _moveSelectView;
        private IPlayerInputProcessor _playerInputProcessor;

        private void Awake()
        {
            _moveSelectView.MoveObservable.Subscribe(toMove =>
            {
                _playerInputProcessor.MoveInput(toMove);
            });
        }
    }
}
