using Cysharp.Threading.Tasks;

namespace ARA.Game
{
    public interface IGameAnimationPlayer
    {
        UniTask PlayAnimation(NetworkResult result);
    }
}
