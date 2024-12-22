using ARA.Character;
using ARA.Masterdata;

namespace ARA.Repositry
{
    public interface ICardRepositry
    {
        public CardData GetCardData(CharacterRole role, int cardId);
    }
}
