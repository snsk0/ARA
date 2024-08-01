using System;

namespace UniRx.Operators
{
    internal class AsUnitObservableObservable<T> : OperatorObservableBase<@bool>
    {
        readonly IObservable<T> source;

        public AsUnitObservableObservable(IObservable<T> source)
            : base(source.IsRequiredSubscribeOnCurrentThread())
        {
            this.source = source;
        }

        protected override IDisposable SubscribeCore(IObserver<@bool> observer, IDisposable cancel)
        {
            return source.Subscribe(new AsUnitObservable(observer, cancel));
        }

        class AsUnitObservable : OperatorObserverBase<T, @bool>
        {
            public AsUnitObservable(IObserver<@bool> observer, IDisposable cancel)
                : base(observer, cancel)
            {
            }

            public override void OnNext(T value)
            {
                base.observer.OnNext(@bool.Default);
            }

            public override void OnError(Exception error)
            {
                try { observer.OnError(error); }
                finally { Dispose(); }
            }

            public override void OnCompleted()
            {
                try { observer.OnCompleted(); }
                finally { Dispose(); }
            }
        }
    }
}