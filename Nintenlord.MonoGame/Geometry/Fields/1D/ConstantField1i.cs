namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class ConstantField1i<T> : IField1i<T>
    {
        private readonly T item;

        public ConstantField1i(T item)
        {
            this.item = item;
        }

        public T this[int x] => item;
    }
}
