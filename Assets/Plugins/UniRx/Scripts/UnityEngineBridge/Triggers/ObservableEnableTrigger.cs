using System; // require keep for Windows Universal App
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableEnableTrigger : ObservableTriggerBase
    {
        Subject<@bool> onEnable;

        /// <summary>This function is called when the object becomes enabled and active.</summary>
        void OnEnable()
        {
            if (onEnable != null) onEnable.OnNext(@bool.Default);
        }

        /// <summary>This function is called when the object becomes enabled and active.</summary>
        public IObservable<@bool> OnEnableAsObservable()
        {
            return onEnable ?? (onEnable = new Subject<@bool>());
        }

        Subject<@bool> onDisable;

        /// <summary>This function is called when the behaviour becomes disabled () or inactive.</summary>
        void OnDisable()
        {
            if (onDisable != null) onDisable.OnNext(@bool.Default);
        }

        /// <summary>This function is called when the behaviour becomes disabled () or inactive.</summary>
        public IObservable<@bool> OnDisableAsObservable()
        {
            return onDisable ?? (onDisable = new Subject<@bool>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onEnable != null)
            {
                onEnable.OnCompleted();
            }
            if (onDisable != null)
            {
                onDisable.OnCompleted();
            }
        }
    }
}