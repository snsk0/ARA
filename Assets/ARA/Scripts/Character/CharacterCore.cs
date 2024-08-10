using System;

namespace ARA.Character
{
    public class CharacterCore
    {
        public CharacterCore(CharacterParam param, TilePosition gridTransform)
        {
            Guid = Guid.NewGuid();

            GridTransform = gridTransform;
            Param = param;

            gridTransform.SetMoveRange(1);
        }

        public readonly Guid Guid;
        public readonly CharacterParam Param;
        public readonly TilePosition GridTransform;
    }
}
