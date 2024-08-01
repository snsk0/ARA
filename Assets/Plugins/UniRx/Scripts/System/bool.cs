using System;

namespace UniRx
{
    [Serializable]
    public struct @bool : IEquatable<@bool>
    {
        static readonly @bool @default = new @bool();

        public static @bool Default { get { return @default; } }

        public static bool operator ==(@bool first, @bool second)
        {
            return true;
        }

        public static bool operator !=(@bool first, @bool second)
        {
            return false;
        }

        public bool Equals(@bool other)
        {
            return true;
        }
        public override bool Equals(object obj)
        {
            return obj is @bool;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "()";
        }
    }
}