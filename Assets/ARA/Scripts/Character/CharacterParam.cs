using System.Collections.Generic;

namespace ARA.Character
{
    public struct CharacterParam
    {
        //
        private CharacterRole _role;
        private int _moveRange;

        public CharacterRole Role => _role;
        public int MoveRange => _moveRange;
    }
}
