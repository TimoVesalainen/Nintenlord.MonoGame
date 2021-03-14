using System;

namespace Nintenlord.MonoGame.Geometry
{
    [Flags]
    public enum BlockFace
    {
        None = 0,
        East = 1,
        West = 2,
        North = 4,
        South = 8,
        Up = 16,
        Down = 32,
        All = 63
    }
}