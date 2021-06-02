namespace Nintenlord.MonoGame.Geometry.Fields
{
    public interface IField4i<out T>
    {
        T this[int x, int y, int z, int w] { get; }
    }
}
