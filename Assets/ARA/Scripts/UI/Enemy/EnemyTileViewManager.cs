using ARA.Presenter;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ARA.UI
{
    public class EnemyTileViewManager : MonoBehaviour, ITilePositionView
    {
        [SerializeField] private TileViewGenerator _generator;
        [SerializeField] private EnemyTileView _tilePrefab;

        private Dictionary<Vector2Int, EnemyTileView> _tiles;

        public void Initialize(Vector2Int size)
        {
            _tiles = _generator.Generate(GetComponent<RectTransform>(), _tilePrefab, size);
        }

        public void UpdateView(Vector2Int currentPosition, IReadOnlyList<Vector2Int> movablePositions)
        {
            foreach (Vector2Int position in _tiles.Keys)
            {
                if(currentPosition == position)
                {
                    _tiles[position].SetCurrent();
                    continue;
                }

                bool movable = movablePositions.Contains(position);
                if (movable)
                {
                    _tiles[position].SetMovable();
                }
                else
                {
                    _tiles[position].SetUnMovable();
                }
            }
        }

    }
}
