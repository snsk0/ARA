using ARA.Player;
using ARA.InputHandle;

namespace ARA.Game
{
    public class GameManager
    {
        public GameManager(InputHandler inputHandler, PlayerCore player, PlayerCore enemy)
        {
            _inputHandler = inputHandler;
            _player = player;
            _enemy = enemy;
        }

        //コアロジック
        private InputHandler _inputHandler;
        private PlayerCore _player;
        private PlayerCore _enemy;

        //演出再生用
        private IGameAnimationPlayer _animationPlayer;

        public async void StartGameLoop()
        {
            //ゲームの終了条件
            while (true)
            {
                //Inputを待つ
                var containers = await _inputHandler.StartWaitInput(_player.GridTransform.CurrentPosition.Value);

                //Inputを送信する

                //結果からアニメーションを再生
                //await _animationPlayer.PlayAnimation();
            }
        }
    }
}
