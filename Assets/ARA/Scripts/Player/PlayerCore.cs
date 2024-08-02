using System;
using ARA.Grid;

namespace ARA.Player
{
    public class PlayerCore
    {
        public PlayerCore(PlayerParameter param, GridMovable movable)
        {
            Guid = Guid.NewGuid();

            GridMovable = movable;
            Param = param;

            movable.SetMoveRange(1);
        }

        public readonly Guid Guid;
        public readonly PlayerParameter Param;
        public readonly GridMovable GridMovable;
    }
}
