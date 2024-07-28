using UnityEngine;
using UniRx;

namespace ARA
{
    public class PlayterInputPresenter : MonoBehaviour
    {
        private IMoveSelectView _moveSelectView;
        private IPlayerInputEventProvider _eventProvider;

        private void Awake()
        {
            _eventProvider.ResetInputObservable.Subscribe(param =>
            {
                _moveSelectView.SetMovableGrids(param.MovableIndexes);
            });
        }
    }
}