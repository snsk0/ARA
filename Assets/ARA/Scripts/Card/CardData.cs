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

        //�����p
        public readonly CharacterRole Role;
        public readonly int CardId;

        public readonly CardParameter Parameter;  //�p�����[�^
        //�J�[�h�I�u�W�F�N�g
        //�J�[�hUI
        //�J�[�h�g�p�A�j���[�V����
    }
}

