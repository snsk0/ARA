using ARA.Character;
using ARA.Masterdata;
using ARA.Repositry;
using UnityEngine;

namespace ARA.ScriptableDatabase
{
    public class ScriptableCardDatabase : ScriptableObject, ICardRepositry
    {
        [SerializeField] private ScriptableCardData _data;

        public CardData GetCardData(CharacterRole role, int cardId)
        {
            return new CardData(role, cardId, _data.Parameter);
        }
    }
}