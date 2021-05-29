using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public interface IVectorField2iTo2v
    {
        Vector2 this[int x, int y] { get; }
    }
}
