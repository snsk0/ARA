using System;
using ARA.Grid;

namespace ARA.Player
{
    public class PlayerCore
    {
        public PlayerCore(PlayerParameter param, GridMovable gridField)
        {
            Guid = Guid.NewGuid();

            GridMovable = gridField;
            Param = param;

            gridField.SetMoveRange(Param.MoveRange);
        }

        public readonly Guid Guid;
        public readonly PlayerParameter Param;
        public readonly GridMovable GridMovable;
    }
}
