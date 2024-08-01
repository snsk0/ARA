using System;
using System.Collections.Generic;

namespace ARA.Presenter
{
    public interface IMoveInputView
    {
        IObservable<int> ToMoveObservable { get; }
        void Initialize(int x, int y, List<bool> isActives, int currentIndex);
        void Initialize(List<bool> _isActives, int currentIndex);
        void SetActive(bool isActive);
        void ReceiveInputResult(int index, bool isSucceeded);
    }
}
