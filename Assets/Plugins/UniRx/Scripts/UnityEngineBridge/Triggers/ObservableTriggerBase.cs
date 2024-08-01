using System; // require keep for Windows Universal App
using UnityEngine;

namespace UniRx.Triggers
{
    public abstract class ObservableTriggerBase : MonoBehaviour
    {
        bool calledAwake = false;
        Subject<@bool> awake;

        /// <summary>Awake is called when the script instance is being loaded.</summary>
        void Awake()
        {
            calledAwake = true;
            if (awake != null) { awake.OnNext(@bool.Default); awake.OnCompleted(); }
        }

        /// <summary>Awake is called when the script instance is being loaded.</summary>
        public IObservable<@bool> AwakeAsObservable()
        {
            if (calledAwake) return Observable.Return(@bool.Default);
            return awake ?? (awake = new Subject<@bool>());
        }

        bool calledStart = false;
        Subject<@bool> start;

        /// <summary>Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.</summary>
        void Start()
        {
            calledStart = true;
            if (start != null) { start.OnNext(@bool.Default); start.OnCompleted(); }
        }

        /// <summary>Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.</summary>
        public IObservable<@bool> StartAsObservable()
        {
            if (calledStart) return Observable.Return(@bool.Default);
            return start ?? (start = new Subject<@bool>());
        }


        bool calledDestroy = false;
        Subject<@bool> onDestroy;

        /// <summary>This function is called when the MonoBehaviour will be destroyed.</summary>
        void OnDestroy()
        {
            calledDestroy = true;
            if (onDestroy != null) { onDestroy.OnNext(@bool.Default); onDestroy.OnCompleted(); }

            RaiseOnCompletedOnDestroy();
        }

        /// <summary>This function is called when the MonoBehaviour will be destroyed.</summary>
        public IObservable<@bool> OnDestroyAsObservable()
        {
            if (this == null) return Observable.Return(@bool.Default);
            if (calledDestroy) return Observable.Return(@bool.Default);
            return onDestroy ?? (onDestroy = new Subject<@bool>());
        }

        protected abstract void RaiseOnCompletedOnDestroy();
    }
}