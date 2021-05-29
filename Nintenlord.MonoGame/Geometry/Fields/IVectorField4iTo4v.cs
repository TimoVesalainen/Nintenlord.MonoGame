using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public interface IVectorField4iTo4v
    {
        Vector4 this[int x, int y, int z, int w] { get; }
    }
}
