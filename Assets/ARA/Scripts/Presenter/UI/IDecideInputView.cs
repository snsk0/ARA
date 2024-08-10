using System;
using UniRx;

namespace ARA.Presenter
{
    public interface IDecideInputView
    {
        void SetDesidable(bool isDecidable);
        IObservable<Unit> InputObservable { get; }
    }
}
