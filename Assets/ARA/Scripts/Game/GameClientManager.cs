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

        //�R�A���W�b�N
        private InputHandler _inputHandler;
        private INetworkSendInterface _networkInterface;

        //���o�Đ��p
        private IGameAnimationPlayer _animationPlayer;

        //�l�b�g���[�N�ҋ@
        private NetworkResult _playerResultCashe;
        private NetworkResult _enemyResultCashe;
        private bool _isNetworkWaiting;

        public async void StartGameLoop()
        {
            //�Q�[���̏I������
            while (true)
            {
                //Input��҂�
                var containers = await _inputHandler.StartWaitInput(_playerResultCashe.Position);

                //Input�𑗐M����
                _networkInterface.ProcessInput(new NetworkInput(containers.Position, containers.DeckIndex));

                //���ʂ�҂�
                _isNetworkWaiting = true;
                await UniTask.WaitWhile(() => _isNetworkWaiting);

                //���ʂ���ύX�𔽉f
                await _animationPlayer.PlayAnimation(_playerResultCashe, _enemyResultCashe);
            }
        }

        //�T�[�o�[�����̌��ʂ��󂯎��
        public void ProcessResult(NetworkResult playerResult, NetworkResult enemyResult)
        {
            _playerResultCashe = playerResult;
            _enemyResultCashe = enemyResult;
            _isNetworkWaiting = false;
        }
    }
}
