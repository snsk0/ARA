using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ARA.Game
{
    public interface IGameAnimationPlayer
    {
        UniTask PlayAnimation(Vector2Int playerPosition, Vector2Int enemyPosition);
    }
}
