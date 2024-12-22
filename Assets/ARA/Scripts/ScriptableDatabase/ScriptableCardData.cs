using ARA.Character;
using UnityEngine;
using ARA.Card;

namespace ARA.ScriptableDatabase
{
    [CreateAssetMenu(menuName = "CardData")]
    public class ScriptableCardData : ScriptableObject
    {
        //���W�b�N�֘A�f�[�^
        [SerializeField] private CharacterRole role;
        [SerializeField] private int cardId;
        [SerializeField] private CardParameter _parameter;

        public CardParameter Parameter => _parameter;

        //���o�֘A�f�[�^
        //�G�t�F�N�g
        //�J�[�hUi
        //�J�[�h�I�u�W�F�N�g
    }
}