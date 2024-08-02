using System.Collections.Generic;
using UnityEngine;
using UniRx;

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

        public List<Vector2Int> GetMovablePositions(Vector2Int currentPosition, int range)
        {
            List<Vector2Int> movablePositions = new List<Vector2Int>{ currentPosition };

            for (int x = -range + currentPosition.x; x + currentPosition.x <= range; x++)
            {
                for(int y = -range + currentPosition.y; y <= range + +currentPosition.y; y++)
                {
                    //“Í‚­‚©‚Ç‚¤‚©
                    bool isReach = Mathf.Abs(x) + Mathf.Abs(y) <= range;

                    //ƒOƒŠƒbƒh”ÍˆÍ“à‚©
                    bool isNotOut = x >= 0 && y >= 0 && x < _gridSize.Value.x && y < _gridSize.Value.y;

                    if(isReach && isNotOut)
                    {
                        movablePositions.Add(new Vector2Int(x, y));
                    }
                }
            }

            return movablePositions;
        }
    }
}
