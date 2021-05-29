using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public interface IVectorField3iTo3v
    {
        Vector3 this[int x, int y, int z] { get; }
    }
}
