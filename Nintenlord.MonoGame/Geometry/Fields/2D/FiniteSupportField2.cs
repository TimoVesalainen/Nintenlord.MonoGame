namespace Nintenlord.MonoGame.Geometry.Fields._2D
{
    public sealed class FiniteSupportField2<T> : IField2i<T>
    {
        readonly int startX;
        readonly int startY;
        readonly T[,] items;
        readonly T rest;

        public FiniteSupportField2(int startX, int startY, T[,] items, T rest)
        {
            this.startY = startY;
            this.startX = startX;
            this.items = items;
            this.rest = rest;
        }


        public T this[int x, int y]
        {
            get
            {
                if (x >= startX && x < startX + items.GetLength(1) &&
                    y >= startY && y < startY + items.GetLength(0))
                {
                    return items[y - startY, x - startX];
                }
                else
                {
                    return rest;
                }
            }
        }
    }
}
