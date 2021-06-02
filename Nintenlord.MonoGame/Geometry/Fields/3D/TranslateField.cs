namespace Nintenlord.MonoGame.Geometry.Fields._3D
{
    public sealed class TranslateField<T> : IField3i<T>
    {
        private readonly int dx;
        private readonly int dy;
        private readonly int dz;
        private readonly IField3i<T> originalField;

        public TranslateField(IField3i<T> originalField, int dx, int dy, int dz)
        {
            this.dx = dx;
            this.dy = dy;
            this.dz = dz;
            this.originalField = originalField;
        }

        public T this[int x, int y, int z] => originalField[x + dx, y + dy, z + dz];
    }
}
