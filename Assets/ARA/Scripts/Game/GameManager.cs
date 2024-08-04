using ARA.Player;
using Cysharp.Threading.Tasks;

namespace ARA.Game
{
    public class GameManager
    {
        public GameManager(PlayerCore[] players, IGameAnimationPlayer animationPlayer)
        {
            _players = players;
            _animationPlayer = animationPlayer;
        }

        private PlayerCore[] _players;
        private IGameAnimationPlayer _animationPlayer;

        public async void StartGameLoop()
        {
            //ゲームの終了条件
            while (true)
            {
                //両者のInputを待つ
                /*
                await UniTask.WhenAll(
                    _players[0].InputHandler.StartWaitInput(_players[0].GridTransform.CurrentPosition.Value),
                    _players[1].InputHandler.StartWaitInput(_players[1].GridTransform.CurrentPosition.Value)
                    );
                */
                var containers = await UniTask.WhenAll(_players[0].InputHandler.StartWaitInput(_players[0].GridTransform.CurrentPosition.Value));

                //コンテナから結果を計算
                _players[0].GridTransform.Move(containers[0].Position);

                //結果からアニメーションを再生
                await _animationPlayer.PlayAnimation();
            }
        }
    }
}
