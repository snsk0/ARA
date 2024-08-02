// for uGUI(from 4.6)
#if !(UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5)

using System; // require keep for Windows Universal App
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableEventTrigger : ObservableTriggerBase, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
    {
        #region IDeselectHandler

        BehaviourSubject<BaseEventData> onDeselect;

        void IDeselectHandler.OnDeselect(BaseEventData eventData)
        {
            if (onDeselect != null) onDeselect.OnNext(eventData);
        }

        public IObservable<BaseEventData> OnDeselectAsObservable()
        {
            return onDeselect ?? (onDeselect = new BehaviourSubject<BaseEventData>());
        }

        #endregion

        #region IMoveHandler

        BehaviourSubject<AxisEventData> onMove;

        void IMoveHandler.OnMove(AxisEventData eventData)
        {
            if (onMove != null) onMove.OnNext(eventData);
        }

        public IObservable<AxisEventData> OnMoveAsObservable()
        {
            return onMove ?? (onMove = new BehaviourSubject<AxisEventData>());
        }

        #endregion

        #region IPointerDownHandler

        BehaviourSubject<PointerEventData> onPointerDown;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (onPointerDown != null) onPointerDown.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnPointerDownAsObservable()
        {
            return onPointerDown ?? (onPointerDown = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region IPointerEnterHandler

        BehaviourSubject<PointerEventData> onPointerEnter;

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (onPointerEnter != null) onPointerEnter.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnPointerEnterAsObservable()
        {
            return onPointerEnter ?? (onPointerEnter = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region IPointerExitHandler

        BehaviourSubject<PointerEventData> onPointerExit;

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (onPointerExit != null) onPointerExit.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnPointerExitAsObservable()
        {
            return onPointerExit ?? (onPointerExit = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region IPointerUpHandler

        BehaviourSubject<PointerEventData> onPointerUp;

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (onPointerUp != null) onPointerUp.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnPointerUpAsObservable()
        {
            return onPointerUp ?? (onPointerUp = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region ISelectHandler

        BehaviourSubject<BaseEventData> onSelect;

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            if (onSelect != null) onSelect.OnNext(eventData);
        }

        public IObservable<BaseEventData> OnSelectAsObservable()
        {
            return onSelect ?? (onSelect = new BehaviourSubject<BaseEventData>());
        }

        #endregion

        #region IPointerClickHandler

        BehaviourSubject<PointerEventData> onPointerClick;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (onPointerClick != null) onPointerClick.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnPointerClickAsObservable()
        {
            return onPointerClick ?? (onPointerClick = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region ISubmitHandler

        BehaviourSubject<BaseEventData> onSubmit;

        void ISubmitHandler.OnSubmit(BaseEventData eventData)
        {
            if (onSubmit != null) onSubmit.OnNext(eventData);
        }

        public IObservable<BaseEventData> OnSubmitAsObservable()
        {
            return onSubmit ?? (onSubmit = new BehaviourSubject<BaseEventData>());
        }

        #endregion

        #region IDragHandler

        BehaviourSubject<PointerEventData> onDrag;

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (onDrag != null) onDrag.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnDragAsObservable()
        {
            return onDrag ?? (onDrag = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region IBeginDragHandler

        BehaviourSubject<PointerEventData> onBeginDrag;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (onBeginDrag != null) onBeginDrag.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnBeginDragAsObservable()
        {
            return onBeginDrag ?? (onBeginDrag = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region IEndDragHandler

        BehaviourSubject<PointerEventData> onEndDrag;

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (onEndDrag != null) onEndDrag.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnEndDragAsObservable()
        {
            return onEndDrag ?? (onEndDrag = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region IDropHandler

        BehaviourSubject<PointerEventData> onDrop;

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            if (onDrop != null) onDrop.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnDropAsObservable()
        {
            return onDrop ?? (onDrop = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region IUpdateSelectedHandler

        BehaviourSubject<BaseEventData> onUpdateSelected;

        void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
        {
            if (onUpdateSelected != null) onUpdateSelected.OnNext(eventData);
        }

        public IObservable<BaseEventData> OnUpdateSelectedAsObservable()
        {
            return onUpdateSelected ?? (onUpdateSelected = new BehaviourSubject<BaseEventData>());
        }

        #endregion

        #region IInitializePotentialDragHandler

        BehaviourSubject<PointerEventData> onInitializePotentialDrag;

        void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (onInitializePotentialDrag != null) onInitializePotentialDrag.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnInitializePotentialDragAsObservable()
        {
            return onInitializePotentialDrag ?? (onInitializePotentialDrag = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        #region ICancelHandler

        BehaviourSubject<BaseEventData> onCancel;

        void ICancelHandler.OnCancel(BaseEventData eventData)
        {
            if (onCancel != null) onCancel.OnNext(eventData);
        }

        public IObservable<BaseEventData> OnCancelAsObservable()
        {
            return onCancel ?? (onCancel = new BehaviourSubject<BaseEventData>());
        }

        #endregion

        #region IScrollHandler

        BehaviourSubject<PointerEventData> onScroll;

        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            if (onScroll != null) onScroll.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnScrollAsObservable()
        {
            return onScroll ?? (onScroll = new BehaviourSubject<PointerEventData>());
        }

        #endregion

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onDeselect != null)
            {
                onDeselect.OnCompleted();
            }
            if (onMove != null)
            {
                onMove.OnCompleted();
            }
            if (onPointerDown != null)
            {
                onPointerDown.OnCompleted();
            }
            if (onPointerEnter != null)
            {
                onPointerEnter.OnCompleted();
            }
            if (onPointerExit != null)
            {
                onPointerExit.OnCompleted();
            }
            if (onPointerUp != null)
            {
                onPointerUp.OnCompleted();
            }
            if (onSelect != null)
            {
                onSelect.OnCompleted();
            }
            if (onPointerClick != null)
            {
                onPointerClick.OnCompleted();
            }
            if (onSubmit != null)
            {
                onSubmit.OnCompleted();
            }
            if (onDrag != null)
            {
                onDrag.OnCompleted();
            }
            if (onBeginDrag != null)
            {
                onBeginDrag.OnCompleted();
            }
            if (onEndDrag != null)
            {
                onEndDrag.OnCompleted();
            }
            if (onDrop != null)
            {
                onDrop.OnCompleted();
            }
            if (onUpdateSelected != null)
            {
                onUpdateSelected.OnCompleted();
            }
            if (onInitializePotentialDrag != null)
            {
                onInitializePotentialDrag.OnCompleted();
            }
            if (onCancel != null)
            {
                onCancel.OnCompleted();
            }
            if (onScroll != null)
            {
                onScroll.OnCompleted();
            }
        }
    }
}

#endif