namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class StretchYField2i<T> : IField2i<T>
    {
        private readonly IField1i<T> item;

        public StretchYField2i(IField1i<T> item)
        {
            this.item = item;
        }

        public T this[int x, int y] => item[x];
    }
}
