using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ARA.Grid
{
    public class GridField
    {
        private ReactiveProperty<Vector2Int> _gridSize;
        public IReadOnlyReactiveProperty<Vector2Int> GridSize => _gridSize;
    }
}
