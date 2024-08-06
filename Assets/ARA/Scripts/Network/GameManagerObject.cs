using Unity.Netcode;
using UnityEngine;

namespace ARA.Network
{
    [RequireComponent(typeof(NetworkObject))]
    public class GameManagerObject : NetworkBehaviour
    {
        public GameManagerObject Singleton;
    }
}
