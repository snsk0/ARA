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
        public readonly CharacterRole Role;
        public readonly int CardId;

        public readonly CardParameter Parameter;  //パラメータ
        //カードオブジェクト
        //カードUI
        //カード使用アニメーション
    }
}

