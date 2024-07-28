using System;
using System.Collections.Generic;

namespace ARA
{
    public interface IMoveSelectView
    {
        IObservable<int> MoveObservable { get; }
        void Initialize(float x, float y);
        void SetMovableGrids(IReadOnlyList<int> movableIndexes);
    }
}