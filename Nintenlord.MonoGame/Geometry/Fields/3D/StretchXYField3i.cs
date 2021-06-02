namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class StretchXYField3i<T> : IField3i<T>
    {
        private readonly IField1i<T> item;

        public StretchXYField3i(IField1i<T> item)
        {
            this.item = item;
        }

        public T this[int x, int y, int z] => item[z];
    }
}
