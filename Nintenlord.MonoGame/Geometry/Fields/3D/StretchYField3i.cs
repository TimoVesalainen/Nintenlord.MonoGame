namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class StretchYField3i<T> : IField3i<T>
    {
        private readonly IField2i<T> item;

        public StretchYField3i(IField2i<T> item)
        {
            this.item = item;
        }

        public T this[int x, int y, int z] => item[x, z];
    }
}
