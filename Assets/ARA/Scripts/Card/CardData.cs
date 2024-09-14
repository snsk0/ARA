using ARA.Card;
using ARA.Character;

namespace ARA.Masterdata
{
    public class CardData
    {
        public CardData(CharacterRole role, int cardId, CardParameter parameter) 
        {
            Role = role;
            CardId = cardId;
            Parameter = parameter;
        }

        //検索用
        private readonly CharacterRole Role;
        private readonly int CardId;

        private readonly CardParameter Parameter;  //パラメータ
        //カードオブジェクト
        //カードUI
        //カード使用アニメーション
    }
}

