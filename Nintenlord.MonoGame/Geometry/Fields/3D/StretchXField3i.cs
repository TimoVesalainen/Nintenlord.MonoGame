namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class StretchXField3i<T> : IField3i<T>
    {
        private readonly IField2i<T> item;

        public StretchXField3i(IField2i<T> item)
        {
            this.item = item;
        }

        public T this[int x, int y, int z] => item[y, z];
    }
}
