using UnityEngine;

namespace ARA.Game
{
    public interface INetworkSendInterface
    {
        void ProcessInput(NetworkInput input);
    }
}
