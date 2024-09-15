using ARA.Character;
using ARA.InputHandle;
using Cysharp.Threading.Tasks;

namespace ARA.Game
{
    public class GameManager : INetworkReciveInterface
    {
        public GameManager(InputHandler inputHandler, CharacterCore player, CharacterCore enemy, 
            INetworkSendInterface networkInterface, IGameAnimationPlayer animationPlayer)
        {
            _inputHandler = inputHandler;
            _player = player;
            _enemy = enemy;
            _networkInterface = networkInterface;
            _animationPlayer = animationPlayer;
        }

        //コアロジック
        private InputHandler _inputHandler;
        private CharacterCore _player;
        private CharacterCore _enemy;
        private INetworkSendInterface _networkInterface;

        //演出再生用
        private IGameAnimationPlayer _animationPlayer;

        //ネットワーク待機
        private NetworkResult _resultCashe;
        private bool _isNetworkWaiting;

        public async void StartGameLoop()
        {
            //ゲームの終了条件
            while (true)
            {
                //Inputを待つ
                var containers = await _inputHandler.StartWaitInput(_player.GridTransform.CurrentPosition.Value);

                //Inputを送信する
                _networkInterface.ProcessInput(new NetworkInput(containers.Position, 0));

                //結果を待つ
                _isNetworkWaiting = true;
                await UniTask.WaitWhile(() => _isNetworkWaiting);

                //結果から変更を反映
                _player.GridTransform.Move(_resultCashe.PlayerPosition);
                _enemy.GridTransform.Move(_resultCashe.EnemyPosition);
                await _animationPlayer.PlayAnimation(_resultCashe.PlayerPosition, _resultCashe.EnemyPosition);
            }
        }

        //サーバー応答の結果を受け取る
        public void ProcessResult(NetworkResult result)
        {
            _resultCashe = result;
            _isNetworkWaiting = false;
        }
    }
}
