using System; // require keep for Windows Universal App
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableJointTrigger : ObservableTriggerBase
    {
        BehaviourSubject<float> onJointBreak;

        void OnJointBreak(float breakForce)
        {
            if (onJointBreak != null) onJointBreak.OnNext(breakForce);
        }

        public IObservable<float> OnJointBreakAsObservable()
        {
            return onJointBreak ?? (onJointBreak = new BehaviourSubject<float>());
        }
        
        
        BehaviourSubject<Joint2D> onJointBreak2D;

        void OnJointBreak2D(Joint2D brokenJoint)
        {
            if (onJointBreak2D != null) onJointBreak2D.OnNext(brokenJoint);
        }

        public IObservable<Joint2D> OnJointBreak2DAsObservable()
        {
            return onJointBreak2D ?? (onJointBreak2D = new BehaviourSubject<Joint2D>());
        }
        

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onJointBreak != null)
            {
                onJointBreak.OnCompleted();
            }
            if (onJointBreak2D != null)
            {
                onJointBreak2D.OnCompleted();
            }
        }
    }
}