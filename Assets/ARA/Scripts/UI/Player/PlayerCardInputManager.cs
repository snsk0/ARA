using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARA.UI
{
    public class PlayerCardInputManager : MonoBehaviour
    {
        [SerializeField] private List<Text> _texts;

        public void SetDeckList(int[] deck)
        {
            int count = 0;
            foreach(int cardId in deck)
            {
                _texts[count++].text = cardId.ToString();
            }
        }
    }
}
