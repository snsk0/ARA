// after uGUI(from 4.6)
#if !(UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5)

using System;
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableRectTransformTrigger : ObservableTriggerBase
    {
        Subject<@bool> onRectTransformDimensionsChange;

        // Callback that is sent if an associated RectTransform has it's dimensions changed
        void OnRectTransformDimensionsChange()
        {
            if (onRectTransformDimensionsChange != null) onRectTransformDimensionsChange.OnNext(@bool.Default);
        }

        /// <summary>Callback that is sent if an associated RectTransform has it's dimensions changed.</summary>
        public IObservable<@bool> OnRectTransformDimensionsChangeAsObservable()
        {
            return onRectTransformDimensionsChange ?? (onRectTransformDimensionsChange = new Subject<@bool>());
        }

        Subject<@bool> onRectTransformRemoved;

        // Callback that is sent if an associated RectTransform is removed
        void OnRectTransformRemoved()
        {
            if (onRectTransformRemoved != null) onRectTransformRemoved.OnNext(@bool.Default);
        }

        /// <summary>Callback that is sent if an associated RectTransform is removed.</summary>
        public IObservable<@bool> OnRectTransformRemovedAsObservable()
        {
            return onRectTransformRemoved ?? (onRectTransformRemoved = new Subject<@bool>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onRectTransformDimensionsChange != null)
            {
                onRectTransformDimensionsChange.OnCompleted();
            }
            if (onRectTransformRemoved != null)
            {
                onRectTransformRemoved.OnCompleted();
            }
        }

    }
}

#endif