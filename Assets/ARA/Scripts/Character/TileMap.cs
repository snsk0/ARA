using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

namespace ARA.Character
{
    public class TileMap
    {
        public interface ITilePosition
        {
            public TileMap Owner { get; } //循環を許容
            public IReadOnlyReactiveProperty<Vector2Int> CurrentPosition { get; }
        }

        public TileMap(Vector2Int gridSize) 
        {
            _disposables = new CompositeDisposable();

            _gridSize = new ReactiveProperty<Vector2Int>(gridSize);
            _gridMovables = new Dictionary<Vector2Int, ITilePosition>();

            _disposables.Add(_gridSize);
        }

        ~TileMap()
        {
            _gridSize.Dispose();
        }

        private CompositeDisposable _disposables;

        private ReactiveProperty<Vector2Int> _gridSize;
        public IReadOnlyReactiveProperty<Vector2Int> GridSize => _gridSize;

        private readonly Dictionary<Vector2Int, ITilePosition> _gridMovables;

        public bool RegisterGridMovable(ITilePosition gridMovable)
        {
            //座標の衝突、登録済みのMovableがある,Ownerが違うなら失敗
            bool isRegisterable = _gridMovables.ContainsKey(gridMovable.CurrentPosition.Value) || _gridMovables.ContainsValue(gridMovable) || gridMovable.Owner != this;
            
            if (isRegisterable)
            {
                return false;
            }

            _gridMovables.Add(gridMovable.CurrentPosition.Value, gridMovable);
            _disposables.Add(gridMovable.CurrentPosition.Subscribe(position =>
            {
                //位置が更新された時にDictionaryに反映する
                UpdateManagedCurrentPosition(gridMovable, position);
            }));
            return true;
        }

        public IReadOnlyList<Vector2Int> GetMovablePositions(TilePosition movable)
        {
            int range = movable.MoveRange.Value;
            Vector2Int currentPosition = movable.CurrentPosition.Value;

            List<Vector2Int> movablePositions = new List<Vector2Int>();

            for (int x = -range + currentPosition.x; x <= range + currentPosition.x; x++)
            {
                for(int y = -range + currentPosition.y; y <= range + currentPosition.y; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);

                    //届くかどうか
                    bool isReach = Mathf.Abs(x - currentPosition.x) + Mathf.Abs(y - currentPosition.y) <= range;

                    //グリッド範囲内か
                    bool isNotOut = x >= 0 && y >= 0 && x < _gridSize.Value.x && y < _gridSize.Value.y;

                    //TODO 対象グリッドが衝突するか
                    bool isConflict = _gridMovables.ContainsKey(position);
                    if (isConflict)
                    {
                        //座標がある場合問い合わせているmovable出ない場合衝突
                        ITilePosition target = _gridMovables[position];
                        isConflict = movable != target;
                    }

                    if(isReach && isNotOut && !isConflict)
                    {
                        movablePositions.Add(new Vector2Int(x, y));
                    }
                }
            }
            return movablePositions;
        }

        private void UpdateManagedCurrentPosition(ITilePosition movable, Vector2Int position)
        {
            //TODO GetMovablePositionをここでキャッシュするのもOK

            //キーから現在別の物があるなら失敗
            if (_gridMovables.TryGetValue(position, out ITilePosition currentMovable))
            {
                //別オブジェクトの場合エラー
                if(currentMovable != movable)
                {
                    throw new System.Exception("Faild Update Managed Current Positions");
                }
                //対象オブジェクトの場合据え置き
                else
                {
                    return;
                }
            }

            //過去キーを取得して削除
            Vector2Int pastPosition = _gridMovables.Where(pair => pair.Value == movable).Select(pair => pair.Key).Single();
            _gridMovables.Remove(pastPosition);
            _gridMovables.Add(position, movable);
        }
    }
}
