namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class StretchZField3i<T> : IField3i<T>
    {
        private readonly IField2i<T> item;

        public StretchZField3i(IField2i<T> item)
        {
            this.item = item;
        }

        public T this[int x, int y, int z] => item[x, y];
    }
}
