using System; // require keep for Windows Universal App
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableUpdateTrigger : ObservableTriggerBase
    {
        Subject<@bool> update;

        /// <summary>Update is called every frame, if the MonoBehaviour is enabled.</summary>
        void Update()
        {
            if (update != null) update.OnNext(@bool.Default);
        }

        /// <summary>Update is called every frame, if the MonoBehaviour is enabled.</summary>
        public IObservable<@bool> UpdateAsObservable()
        {
            return update ?? (update = new Subject<@bool>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (update != null)
            {
                update.OnCompleted();
            }
        }
    }
}