namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class ConstantField2i<T> : IField2i<T>
    {
        private readonly T item;

        public ConstantField2i(T item)
        {
            this.item = item;
        }

        public T this[int x, int y] => item;
    }
}
