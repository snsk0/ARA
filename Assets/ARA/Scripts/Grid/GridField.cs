using UniRx;
using UnityEngine;

namespace ARA.Grid
{
    public class GridField
    {
        public GridField() 
        {
            _gridSize = new ReactiveProperty<Vector2Int>();
        }

        ~GridField()
        {
            _gridSize.Dispose();
        }

        public void Initialize(Vector2Int gridSize)
        {
            _gridSize.Value = gridSize;
        }

        private ReactiveProperty<Vector2Int> _gridSize;
        public IReadOnlyReactiveProperty<Vector2Int> GridSize => _gridSize;
    }
}
