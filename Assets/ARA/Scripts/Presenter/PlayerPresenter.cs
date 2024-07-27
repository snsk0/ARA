using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARA
{
    public class PlayerPresenter : MonoBehaviour
    {
        private IMoveSelectView _moveSelectView;
        private IPlayerInputProcessor _playerInputProcessor;

        private void Awake()
        {
        }
    }
}
