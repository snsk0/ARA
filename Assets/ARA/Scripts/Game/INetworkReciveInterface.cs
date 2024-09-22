using UnityEngine;

namespace ARA.Game
{
    public interface INetworkReciveInterface
    {
        void ProcessResult(NetworkResult playerResult, NetworkResult enemyResult);
    }
}
