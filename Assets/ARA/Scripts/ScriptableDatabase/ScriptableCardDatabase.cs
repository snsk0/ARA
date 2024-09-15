using ARA.Character;
using ARA.Masterdata;
using ARA.Repositry;
using UnityEngine;
using ARA.Card;

namespace ARA.ScriptableDatabase
{
    public class ScriptableCardDatabase : ScriptableObject, ICardRepositry
    {
        public CardData GetCardData(CharacterRole role, int cardId)
        {
            return new CardData(role, cardId, new CardParameter(10, 1, RangeType.Melee, new int[1,3]));
        }
    }
}