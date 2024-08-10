using System;
using UnityEngine;

namespace ARA.Presenter
{
    public interface ITilePositionInputView : ITilePositionView
    {
        IObservable<Vector2Int> InputObservable { get; }
        void ProcessInputResult(Vector2Int input, bool isSucceeded);
    }
}