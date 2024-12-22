using System.Collections.Generic;
using UnityEngine;

namespace ARA.Presenter
{
    public interface ITilePositionView
    {
        void Initialize(Vector2Int size);
        void UpdateView(Vector2Int currentPosition, IReadOnlyList<Vector2Int> movablePositions);
    }
}