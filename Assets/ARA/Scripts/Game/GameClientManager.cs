using ARA.InputHandle;
using Cysharp.Threading.Tasks;

namespace ARA.Game
{
    public class GameClientManager : INetworkReciveInterface
    {
        public GameClientManager(NetworkResult initializeResult, InputHandler inputHandler, INetworkSendInterface networkInterface, IGameAnimationPlayer animationPlayer)
        {
            _playerResultCashe = initializeResult;
            _inputHandler = inputHandler;
            _networkInterface = networkInterface;
            _animationPlayer = animationPlayer;
        }

        //コアロジック
        private InputHandler _inputHandler;
        private INetworkSendInterface _networkInterface;

        //演出再生用
        private IGameAnimationPlayer _animationPlayer;

        //ネットワーク待機
        private NetworkResult _playerResultCashe;
        private NetworkResult _enemyResultCashe;
        private bool _isNetworkWaiting;

        public async void StartGameLoop()
        {
            //ゲームの終了条件
            while (true)
            {
                //Inputを待つ
                var containers = await _inputHandler.StartWaitInput(_playerResultCashe.Position);

                //Inputを送信する
                _networkInterface.ProcessInput(new NetworkInput(containers.Position, containers.DeckIndex));

                //結果を待つ
                _isNetworkWaiting = true;
                await UniTask.WaitWhile(() => _isNetworkWaiting);

                //結果から変更を反映
                await _animationPlayer.PlayAnimation(_playerResultCashe, _enemyResultCashe);
            }
        }

        //サーバー応答の結果を受け取る
        public void ProcessResult(NetworkResult playerResult, NetworkResult enemyResult)
        {
            _playerResultCashe = playerResult;
            _enemyResultCashe = enemyResult;
            _isNetworkWaiting = false;
        }
    }
}
