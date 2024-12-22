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

        //サーバーの時のみ生成する
        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);

                NetworkManager.Singleton.OnClientConnectedCallback += ((clientId) =>
                {
                    //プレイヤーの生成
                    TileMap gridField = new TileMap(_gridSize);
                    TilePosition movable = new TilePosition(gridField, _initialPosition);

                    //デッキを構成する
                    int[] deck = new int[3];
                    for (int j = 0; j < 3; j++)
                    {
                        deck[j] = Random.Range(0, 10);
                    }

                    //キャラクター生成
                    CharacterCore playerCharacter = new CharacterCore(new CharacterParam(), deck, movable);

                    //Idと紐づけて保存する
                    _players.Add(clientId, playerCharacter);

                    //プレイヤー数がそろったらConnectorに指示を飛ばす
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
                    //Inputを保持する
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
            //ゲームの終了条件
            while (true)
            {
                //inputが集まるのを待機する
                await UniTask.WaitUntil(() => _inputDatas.Count == _playerNumber);

                //優先度
                int clientIdIndex;
                ulong[] clientIds = _players.Keys.ToArray();
                int fast1 = 0; /*_repositry.GetCardData(_players[clientIds[0]].Param.Role, _inputDatas[clientIds[0]].CardId).Parameter.Fast;*/
                int fast2 = 0;/*_repositry.GetCardData(_players[clientIds[1]].Param.Role, _inputDatas[clientIds[1]].CardId).Parameter.Fast;*/
                if(fast1 == fast2)
                {
                    //ランダムで決定する
                    clientIdIndex = Random.Range(0, clientIds.Length);
                }
                else
                {
                    clientIdIndex = fast1 > fast2 ? 0 : 1;
                }

                //プレイヤー数分繰り返す(二人分)
                for (int i = 0; i < _playerNumber; i++) 
                {
                    //キャラクターを取得
                    CharacterCore player = _players[clientIds[clientIdIndex]];

                    //座標を更新する
                    player.GridTransform.Move(_inputDatas[clientIds[clientIdIndex]].Position);

                    //攻撃判定を行う TODO

                    //指定indexのデッキ数値を更新する
                    player.Deck[_inputDatas[clientIds[clientIdIndex]].DeckIndex] = Random.Range(0, 10);

                    //indexを反転
                    clientIdIndex ^= 1;
                }

                //結果をコネクタに帰す
                foreach (ulong clientId in _players.Keys)
                {
                    NetworkResult playerResult;
                    NetworkResult enemyResult;

                    CharacterCore player;
                    CharacterCore enemy;

                    //プレイヤーが先制かどうか
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

                //inputをクリアする
                _inputDatas.Clear();
            }
        }
    }
}
