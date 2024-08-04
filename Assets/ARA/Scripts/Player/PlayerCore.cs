using System;
using ARA.Grid;

namespace ARA.Player
{
    public class PlayerCore
    {
        public PlayerCore(PlayerParameter param, GridTransform gridTransform, PlayerInputHandler inputHandler)
        {
            Guid = Guid.NewGuid();

            GridTransform = gridTransform;
            Param = param;
            InputHandler = inputHandler;

            gridTransform.SetMoveRange(1);
        }

        public readonly Guid Guid;
        public readonly PlayerParameter Param;
        public readonly GridTransform GridTransform;
        public readonly PlayerInputHandler InputHandler;
    }
}
