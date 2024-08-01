using System;
using UniRx.Operators;

namespace UniRx.Operators
{
    internal class AsSingleUnitObservableObservable<T> : OperatorObservableBase<@bool>
    {
        readonly IObservable<T> source;

        public AsSingleUnitObservableObservable(IObservable<T> source)
            : base(source.IsRequiredSubscribeOnCurrentThread())
        {
            this.source = source;
        }

        protected override IDisposable SubscribeCore(IObserver<@bool> observer, IDisposable cancel)
        {
            return source.Subscribe(new AsSingleUnitObservable(observer, cancel));
        }

        class AsSingleUnitObservable : OperatorObserverBase<T, @bool>
        {
            public AsSingleUnitObservable(IObserver<@bool> observer, IDisposable cancel) : base(observer, cancel)
            {
            }

            public override void OnNext(T value)
            {
            }

            public override void OnError(Exception error)
            {
                try { observer.OnError(error); }
                finally { Dispose(); }
            }

            public override void OnCompleted()
            {
                observer.OnNext(@bool.Default);

                try { observer.OnCompleted(); }
                finally { Dispose(); }
            }
        }
    }
}