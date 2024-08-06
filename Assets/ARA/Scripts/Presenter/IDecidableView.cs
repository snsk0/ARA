using System;
using UniRx;

namespace ARA.Presenter
{
    public interface IDecidableView
    {
        void SetDesidable(bool isDecidable);
        IObservable<Unit> DecideObservable { get; }
    }
}
