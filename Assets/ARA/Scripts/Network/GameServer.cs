using ARA.Character;
using ARA.Game;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using Zenject;
using ARA.Repositry;

namespace ARA.Network
{
    [RequireComponent(typeof(NetworkObject))]
    public class GameServer : NetworkBehaviour
    {
        public static GameServer Instance;

        [SerializeField]
        private Vector2Int _gridSize;

        [SerializeField]
        private Vector2Int _initialPosition;

        [SerializeField]
        private uint _playerNumber;

        [SerializeField]
        private GameServerConnector _connector;

        [Inject]
        ICardRepositry _repositry;

        private Dictionary<ulong, CharacterCore> _players = new Dictionary<ulong, CharacterCore>();
        private Dictionary<ulong, NetworkInput> _inputDatas = new Dictionary<ulong, NetworkInput>();

        //�T�[�o�[�̎��̂ݐ�������
        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);

                NetworkManager.Singleton.OnClientConnectedCallback += ((clientId) =>
                {
                    //�v���C���[�̐���
                    TileMap gridField = new TileMap(_gridSize);
                    TilePosition movable = new TilePosition(gridField, _initialPosition);

                    //�f�b�L���\������
                    int[] deck = new int[3];
                    for (int j = 0; j < 3; j++)
                    {
                        deck[j] = Random.Range(0, 10);
                    }

                    //�L�����N�^�[����
                    CharacterCore playerCharacter = new CharacterCore(new CharacterParam(), deck, movable);

                    //Id�ƕR�Â��ĕۑ�����
                    _players.Add(clientId, playerCharacter);

                    //�v���C���[�������������Connector�Ɏw�����΂�
                    if (_playerNumber == _players.Count)
                    {
                        foreach (ulong clientIdCashe in _players.Keys)
                        {
                            NetworkResult playerResult;
                            NetworkResult enemyResult;

                            CharacterCore player;
                            CharacterCore enemy;

                            if (clientIdCashe == 0)
                            {
                                player = _players[clientIdCashe];
                                enemy = _players[clientIdCashe + 1];
                            }
                            else
                            {
                                player = _players[clientIdCashe];
                                enemy = _players[clientIdCashe - 1];
                            }

                            playerResult = new NetworkResult(
                                false,
                                player.GridTransform.CurrentPosition.Value,
                                player.GridTransform.Owner.GetMovablePositions(player.GridTransform).ToArray(),
                                player.Deck,
                                0,
                                100);

                            enemyResult = new NetworkResult(
                                false,
                                enemy.GridTransform.CurrentPosition.Value,
                                enemy.GridTransform.Owner.GetMovablePositions(enemy.GridTransform).ToArray(),
                                enemy.Deck,
                                0,
                                100);

                            _connector.InitializeGameRpc(_gridSize, playerResult, enemyResult, RpcTarget.Single(clientIdCashe, default));
                            StartGameLoop();
                        }
                    }
                });

                _connector.OnInputEvent += (input, param) =>
                {
                    //Input��ێ�����
                    _inputDatas.Add(param.Receive.SenderClientId, input);
                };
            }
            else
            {
                Destroy(this);
            }
        }

        private async void StartGameLoop()
        {
            //�Q�[���̏I������
            while (true)
            {
                //input���W�܂�̂�ҋ@����
                await UniTask.WaitUntil(() => _inputDatas.Count == _playerNumber);

                //�D��x
                int clientIdIndex;
                ulong[] clientIds = _players.Keys.ToArray();
                int fast1 = 0; /*_repositry.GetCardData(_players[clientIds[0]].Param.Role, _inputDatas[clientIds[0]].CardId).Parameter.Fast;*/
                int fast2 = 0;/*_repositry.GetCardData(_players[clientIds[1]].Param.Role, _inputDatas[clientIds[1]].CardId).Parameter.Fast;*/
                if(fast1 == fast2)
                {
                    //�����_���Ō��肷��
                    clientIdIndex = Random.Range(0, clientIds.Length);
                }
                else
                {
                    clientIdIndex = fast1 > fast2 ? 0 : 1;
                }

                //�v���C���[�����J��Ԃ�(��l��)
                for (int i = 0; i < _playerNumber; i++) 
                {
                    //�L�����N�^�[���擾
                    CharacterCore player = _players[clientIds[clientIdIndex]];

                    //���W���X�V����
                    player.GridTransform.Move(_inputDatas[clientIds[clientIdIndex]].Position);

                    //�U��������s�� TODO

                    //�w��index�̃f�b�L���l���X�V����
                    player.Deck[_inputDatas[clientIds[clientIdIndex]].DeckIndex] = Random.Range(0, 10);

                    //index�𔽓]
                    clientIdIndex ^= 1;
                }

                //���ʂ��R�l�N�^�ɋA��
                foreach (ulong clientId in _players.Keys)
                {
                    NetworkResult playerResult;
                    NetworkResult enemyResult;

                    CharacterCore player;
                    CharacterCore enemy;

                    //�v���C���[���搧���ǂ���
                    bool playerIsFormer = clientId == clientIds[clientIdIndex];

                    if (clientId == 0)
                    {
                        player = _players[clientId];
                        enemy = _players[clientId + 1];
                    }
                    else
                    {
                        player = _players[clientId];
                        enemy = _players[clientId - 1];
                    }

                    playerResult = new NetworkResult(
                        playerIsFormer,
                        player.GridTransform.CurrentPosition.Value,
                        player.GridTransform.Owner.GetMovablePositions(player.GridTransform).ToArray(),
                        player.Deck,
                        0,
                        100);

                    enemyResult = new NetworkResult(
                        !playerIsFormer,
                        enemy.GridTransform.CurrentPosition.Value,
                        enemy.GridTransform.Owner.GetMovablePositions(enemy.GridTransform).ToArray(),
                        enemy.Deck,
                        0,
                        100);

                    _connector.ProcessResultRpc(playerResult, enemyResult, RpcTarget.Single(clientId, default));
                }

                //input���N���A����
                _inputDatas.Clear();
            }
        }
    }
}
