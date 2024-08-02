using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ARA.Grid
{
    public class GridField
    {
        public GridField(Vector2Int gridSize) 
        {
            _gridSize = new ReactiveProperty<Vector2Int>(gridSize);
        }

        ~GridField()
        {
            _gridSize.Dispose();
        }

        private ReactiveProperty<Vector2Int> _gridSize;
        public IReadOnlyReactiveProperty<Vector2Int> GridSize => _gridSize;

        public List<Vector2Int> GetMovablePositions(Vector2Int currentPosition, int range)
        {
            List<Vector2Int> movablePositions = new List<Vector2Int>();

            for (int x = -range + currentPosition.x; x <= range + currentPosition.x; x++)
            {
                for(int y = -range + currentPosition.y; y <= range + currentPosition.y; y++)
                {
                    //“Í‚­‚©‚Ç‚¤‚©
                    bool isReach = Mathf.Abs(x - currentPosition.x) + Mathf.Abs(y - currentPosition.y) <= range;

                    //ƒOƒŠƒbƒh”ÍˆÍ“à‚©
                    bool isNotOut = x >= 0 && y >= 0 && x < _gridSize.Value.x && y < _gridSize.Value.y;

                    if(isReach && isNotOut)
                    {
                        movablePositions.Add(new Vector2Int(x, y));
                        Debug.Log(new Vector2(x, y));
                    }
                }
            }

            return movablePositions;
        }
    }
}
