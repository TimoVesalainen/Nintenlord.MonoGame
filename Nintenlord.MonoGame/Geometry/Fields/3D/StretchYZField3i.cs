namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class StretchYZField3i<T> : IField3i<T>
    {
        private readonly IField1i<T> item;

        public StretchYZField3i(IField1i<T> item)
        {
            this.item = item;
        }

        public T this[int x, int y, int z] => item[x];
    }
}
