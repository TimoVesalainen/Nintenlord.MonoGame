namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class ConstantField3i<T> : IField3i<T>
    {
        private readonly T item;

        public ConstantField3i(T item)
        {
            this.item = item;
        }

        public T this[int x, int y, int z] => item;
    }
}
