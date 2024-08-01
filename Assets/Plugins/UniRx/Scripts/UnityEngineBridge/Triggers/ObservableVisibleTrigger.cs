using System; // require keep for Windows Universal App
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableVisibleTrigger : ObservableTriggerBase
    {
        Subject<@bool> onBecameInvisible;

        /// <summary>OnBecameInvisible is called when the renderer is no longer visible by any camera.</summary>
        void OnBecameInvisible()
        {
            if (onBecameInvisible != null) onBecameInvisible.OnNext(@bool.Default);
        }

        /// <summary>OnBecameInvisible is called when the renderer is no longer visible by any camera.</summary>
        public IObservable<@bool> OnBecameInvisibleAsObservable()
        {
            return onBecameInvisible ?? (onBecameInvisible = new Subject<@bool>());
        }

        Subject<@bool> onBecameVisible;

        /// <summary>OnBecameVisible is called when the renderer became visible by any camera.</summary>
        void OnBecameVisible()
        {
            if (onBecameVisible != null) onBecameVisible.OnNext(@bool.Default);
        }

        /// <summary>OnBecameVisible is called when the renderer became visible by any camera.</summary>
        public IObservable<@bool> OnBecameVisibleAsObservable()
        {
            return onBecameVisible ?? (onBecameVisible = new Subject<@bool>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onBecameInvisible != null)
            {
                onBecameInvisible.OnCompleted();
            }
            if (onBecameVisible != null)
            {
                onBecameVisible.OnCompleted();
            }
        }
    }
}