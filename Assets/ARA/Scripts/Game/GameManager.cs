using ARA.Player;
using ARA.InputHandle;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ARA.Game
{
    public class GameManager : INetworkReciveInterface
    {
        public GameManager(InputHandler inputHandler, PlayerCore player, PlayerCore enemy, INetworkSendInterface networkInterface)
        {
            _inputHandler = inputHandler;
            _player = player;
            _enemy = enemy;
            _networkInterface = networkInterface;
        }

        //コアロジック
        private InputHandler _inputHandler;
        private PlayerCore _player;
        private PlayerCore _enemy;
        private INetworkSendInterface _networkInterface;

        //演出再生用
        private IGameAnimationPlayer _animationPlayer;

        //ネットワーク待機
        private Vector2Int _resultCashe;
        private bool _isNetworkWaiting;

        public async void StartGameLoop()
        {
            //ゲームの終了条件
            while (true)
            {
                //Inputを待つ
                var containers = await _inputHandler.StartWaitInput(_player.GridTransform.CurrentPosition.Value);

                //Inputを送信する
                _networkInterface.ProcessInput(containers.Position);

                //結果を待つ
                _isNetworkWaiting = true;
                await UniTask.WaitWhile(() => _isNetworkWaiting);

                //結果から変更を反映
                _player.GridTransform.Move(_resultCashe);
                //await _animationPlayer.PlayAnimation();
            }
        }

        //サーバー応答の結果を受け取る
        public void ProcessResult(Vector2Int result)
        {
            _resultCashe = result;
            _isNetworkWaiting = false;
        }
    }
}
