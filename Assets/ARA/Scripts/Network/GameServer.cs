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

                NetworkManager.Singleton.OnClientConnectedCallback += ((cliendID) =>
                {
                    //プレイヤーの生成
                    TileMap gridField = new TileMap(_gridSize);
                    TilePosition movable = new TilePosition(gridField, _initialPosition);
                    CharacterCore player = new CharacterCore(new CharacterParam(), movable);

                    //Idと紐づけて保存する
                    _players.Add(cliendID, player);

                    //プレイヤー数がそろったらConnectorに指示を飛ばす
                    if (_players.Count == _playerNumber)
                    {
                        _connector.InitializeGameRpc(_gridSize, _initialPosition);
                        StartGameLoop();
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
                int fast1 = _repositry.GetCardData(_players[clientIds[0]].Param.Role, _inputDatas[clientIds[0]].CardId).Parameter.Fast;
                int fast2 = _repositry.GetCardData(_players[clientIds[1]].Param.Role, _inputDatas[clientIds[1]].CardId).Parameter.Fast;
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

                    //indexを反転
                    clientIdIndex ^= 1;
                }

                //結果をコネクタに帰す
                foreach (ulong clientId in _players.Keys)
                {
                    //仮コード
                    NetworkResult result;
                    if (clientId == 0)
                    {
                        result = new NetworkResult(_players[clientId].GridTransform.CurrentPosition.Value, _players[clientId + 1].GridTransform.CurrentPosition.Value);
                    }
                    else
                    {
                        result = new NetworkResult(_players[clientId].GridTransform.CurrentPosition.Value, _players[clientId - 1].GridTransform.CurrentPosition.Value);
                    }

                    _connector.ProcessResultRpc(result, RpcTarget.Single(clientId, default));
                }

                //inputをクリアする
                _inputDatas.Clear();
            }
        }
    }
}
