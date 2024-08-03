using UnityEngine;

namespace ARA.Presenter
{
    public interface IInputAnimator
    {
        void PlayPreMoveAnimation(Vector2Int fromPosition, Vector2Int toPosition);
    }
}
