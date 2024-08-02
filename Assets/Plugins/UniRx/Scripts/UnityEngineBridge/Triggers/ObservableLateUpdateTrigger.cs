using System; // require keep for Windows Universal App
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableLateUpdateTrigger : ObservableTriggerBase
    {
        BehaviourSubject<Unit> lateUpdate;

        /// <summary>LateUpdate is called every frame, if the Behaviour is enabled.</summary>
        void LateUpdate()
        {
            if (lateUpdate != null) lateUpdate.OnNext(Unit.Default);
        }

        /// <summary>LateUpdate is called every frame, if the Behaviour is enabled.</summary>
        public IObservable<Unit> LateUpdateAsObservable()
        {
            return lateUpdate ?? (lateUpdate = new BehaviourSubject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (lateUpdate != null)
            {
                lateUpdate.OnCompleted();
            }
        }
    }
}