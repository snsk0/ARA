using UnityEngine;

namespace ARA.Presenter
{
    public interface IInputAnimator
    {
        void UpdateFromPosition(Vector2Int fromPosition);
        void PlayPreMoveAnimation(Vector2Int toPosition);
    }
}
