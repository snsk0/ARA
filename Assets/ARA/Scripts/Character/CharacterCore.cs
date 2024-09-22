using System;

namespace ARA.Character
{
    public class CharacterCore
    {
        public CharacterCore(CharacterParam param, int[] deck, TilePosition gridTransform)
        {
            Guid = Guid.NewGuid();

            GridTransform = gridTransform;
            Param = param;

            Deck = deck;

            gridTransform.SetMoveRange(1);
        }

        public readonly Guid Guid;
        public readonly CharacterParam Param;
        public readonly int[] Deck;
        public readonly TilePosition GridTransform;
    }
}
