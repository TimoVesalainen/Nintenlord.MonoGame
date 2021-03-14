using System;

namespace Nintenlord.MonoGame.Geometry
{
    [Flags]
    public enum BlockCorner
    {
        None = 0,
        UpNorthEast = 1,
        UpNorthWest = 2,
        UpSouthEast = 4,
        UpSouthWest = 8,

        DownNorthEast = 16,
        DownNorthWest = 32,
        DownSouthEast = 64,
        DownSouthWest = 128,

        Up = 15,
        Down = 240,
        North = 51,
        South = 204,
        East = 85,
        West = 170,

        All = 255
    }
}