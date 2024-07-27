using System;

namespace ARA
{
    public interface IMoveSelectView
    {
        IObservable<int> MoveObservable { get; }
        void Initialize(float x, float y);
    }
}