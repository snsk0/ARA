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
            public TileMap Owner { get; } //�z�����e
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
            //���W�̏ՓˁA�o�^�ς݂�Movable������,Owner���Ⴄ�Ȃ玸�s
            bool isRegisterable = _gridMovables.ContainsKey(gridMovable.CurrentPosition.Value) || _gridMovables.ContainsValue(gridMovable) || gridMovable.Owner != this;
            
            if (isRegisterable)
            {
                return false;
            }

            _gridMovables.Add(gridMovable.CurrentPosition.Value, gridMovable);
            _disposables.Add(gridMovable.CurrentPosition.Subscribe(position =>
            {
                //�ʒu���X�V���ꂽ����Dictionary�ɔ��f����
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

                    //�͂����ǂ���
                    bool isReach = Mathf.Abs(x - currentPosition.x) + Mathf.Abs(y - currentPosition.y) <= range;

                    //�O���b�h�͈͓���
                    bool isNotOut = x >= 0 && y >= 0 && x < _gridSize.Value.x && y < _gridSize.Value.y;

                    //TODO �ΏۃO���b�h���Փ˂��邩
                    bool isConflict = _gridMovables.ContainsKey(position);
                    if (isConflict)
                    {
                        //���W������ꍇ�₢���킹�Ă���movable�o�Ȃ��ꍇ�Փ�
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
            //TODO GetMovablePosition�������ŃL���b�V������̂�OK

            //�L�[���猻�ݕʂ̕�������Ȃ玸�s
            if (_gridMovables.TryGetValue(position, out ITilePosition currentMovable))
            {
                //�ʃI�u�W�F�N�g�̏ꍇ�G���[
                if(currentMovable != movable)
                {
                    throw new System.Exception("Faild Update Managed Current Positions");
                }
                //�ΏۃI�u�W�F�N�g�̏ꍇ�����u��
                else
                {
                    return;
                }
            }

            //�ߋ��L�[���擾���č폜
            Vector2Int pastPosition = _gridMovables.Where(pair => pair.Value == movable).Select(pair => pair.Key).Single();
            _gridMovables.Remove(pastPosition);
            _gridMovables.Add(position, movable);
        }
    }
}
