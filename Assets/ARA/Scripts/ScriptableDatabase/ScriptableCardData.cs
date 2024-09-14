using ARA.Character;
using UnityEngine;
using ARA.Card;

namespace ARA.ScriptableDatabase
{
    [CreateAssetMenu(menuName = "CardData")]
    public class ScriptableCardData : ScriptableObject
    {
        //ロジック関連データ
        [SerializeField] private CharacterRole role;
        [SerializeField] private int cardId;
        [SerializeField] private CardParameter _parameter;

        public CardParameter Parameter => _parameter;

        //演出関連データ
        //エフェクト
        //カードUi
        //カードオブジェクト
    }
}