using UniRx;
using UnityEngine;

namespace ARA.Grid
{
    public interface IGridMovable
    {
        public GridField Owner { get; } //zŠÂ‚ğ‹–—e
        public IReadOnlyReactiveProperty<Vector2Int> CurrentPosition { get; }
    }
}
