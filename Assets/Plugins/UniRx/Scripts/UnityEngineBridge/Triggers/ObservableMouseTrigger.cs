#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_METRO)

using System; // require keep for Windows Universal App
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableMouseTrigger : ObservableTriggerBase
    {
        Subject<@bool> onMouseDown;

        /// <summary>OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider.</summary>
         void OnMouseDown()
        {
            if (onMouseDown != null) onMouseDown.OnNext(@bool.Default);
        }

        /// <summary>OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider.</summary>
        public IObservable<@bool> OnMouseDownAsObservable()
        {
            return onMouseDown ?? (onMouseDown = new Subject<@bool>());
        }

        Subject<@bool> onMouseDrag;

        /// <summary>OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.</summary>
         void OnMouseDrag()
        {
            if (onMouseDrag != null) onMouseDrag.OnNext(@bool.Default);
        }

        /// <summary>OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.</summary>
        public IObservable<@bool> OnMouseDragAsObservable()
        {
            return onMouseDrag ?? (onMouseDrag = new Subject<@bool>());
        }

        Subject<@bool> onMouseEnter;

        /// <summary>OnMouseEnter is called when the mouse entered the GUIElement or Collider.</summary>
         void OnMouseEnter()
        {
            if (onMouseEnter != null) onMouseEnter.OnNext(@bool.Default);
        }

        /// <summary>OnMouseEnter is called when the mouse entered the GUIElement or Collider.</summary>
        public IObservable<@bool> OnMouseEnterAsObservable()
        {
            return onMouseEnter ?? (onMouseEnter = new Subject<@bool>());
        }

        Subject<@bool> onMouseExit;

        /// <summary>OnMouseExit is called when the mouse is not any longer over the GUIElement or Collider.</summary>
         void OnMouseExit()
        {
            if (onMouseExit != null) onMouseExit.OnNext(@bool.Default);
        }

        /// <summary>OnMouseExit is called when the mouse is not any longer over the GUIElement or Collider.</summary>
        public IObservable<@bool> OnMouseExitAsObservable()
        {
            return onMouseExit ?? (onMouseExit = new Subject<@bool>());
        }

        Subject<@bool> onMouseOver;

        /// <summary>OnMouseOver is called every frame while the mouse is over the GUIElement or Collider.</summary>
         void OnMouseOver()
        {
            if (onMouseOver != null) onMouseOver.OnNext(@bool.Default);
        }

        /// <summary>OnMouseOver is called every frame while the mouse is over the GUIElement or Collider.</summary>
        public IObservable<@bool> OnMouseOverAsObservable()
        {
            return onMouseOver ?? (onMouseOver = new Subject<@bool>());
        }

        Subject<@bool> onMouseUp;

        /// <summary>OnMouseUp is called when the user has released the mouse button.</summary>
         void OnMouseUp()
        {
            if (onMouseUp != null) onMouseUp.OnNext(@bool.Default);
        }

        /// <summary>OnMouseUp is called when the user has released the mouse button.</summary>
        public IObservable<@bool> OnMouseUpAsObservable()
        {
            return onMouseUp ?? (onMouseUp = new Subject<@bool>());
        }

        Subject<@bool> onMouseUpAsButton;

        /// <summary>OnMouseUpAsButton is only called when the mouse is released over the same GUIElement or Collider as it was pressed.</summary>
         void OnMouseUpAsButton()
        {
            if (onMouseUpAsButton != null) onMouseUpAsButton.OnNext(@bool.Default);
        }

        /// <summary>OnMouseUpAsButton is only called when the mouse is released over the same GUIElement or Collider as it was pressed.</summary>
        public IObservable<@bool> OnMouseUpAsButtonAsObservable()
        {
            return onMouseUpAsButton ?? (onMouseUpAsButton = new Subject<@bool>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onMouseDown != null)
            {
                onMouseDown.OnCompleted();
            }
            if (onMouseDrag != null)
            {
                onMouseDrag.OnCompleted();
            }
            if (onMouseEnter != null)
            {
                onMouseEnter.OnCompleted();
            }
            if (onMouseExit != null)
            {
                onMouseExit.OnCompleted();
            }
            if (onMouseOver != null)
            {
                onMouseOver.OnCompleted();
            }
            if (onMouseUp != null)
            {
                onMouseUp.OnCompleted();
            }
            if (onMouseUpAsButton != null)
            {
                onMouseUpAsButton.OnCompleted();
            }
        }
    }
}

#endif