using System;

namespace ARA.Player
{
    public class PlayerCore
    {
        public PlayerCore(PlayerParameter param, GridTransform gridTransform)
        {
            Guid = Guid.NewGuid();

            GridTransform = gridTransform;
            Param = param;

            gridTransform.SetMoveRange(1);
        }

        public readonly Guid Guid;
        public readonly PlayerParameter Param;
        public readonly GridTransform GridTransform;
    }
}
