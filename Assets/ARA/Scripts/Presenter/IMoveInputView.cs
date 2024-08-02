using System;
using System.Collections.Generic;
using UnityEngine;

namespace ARA.Presenter
{
    public interface IMoveInputView
    {
        IObservable<Vector2Int> ToMoveObservable { get; }
        void Initialize(Vector2Int gridSize);
        void UpdateUI(Vector2Int currentPosition, IReadOnlyList<Vector2Int> isActivePositions);
        void SetActive(bool isActive);
        void ReceiveInputResult(Vector2Int inputedPosition, bool isSucceeded);
    }
}
