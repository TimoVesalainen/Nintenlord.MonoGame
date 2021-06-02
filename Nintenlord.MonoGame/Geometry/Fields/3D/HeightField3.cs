namespace Nintenlord.MonoGame.Geometry.Fields._3D
{
    public sealed class HeightField3 : IField3i<bool>
    {
        private readonly IField2i<int> heightMap;

        public HeightField3(IField2i<int> heightMap)
        {
            this.heightMap = heightMap;
        }

        public bool this[int x, int y, int z] => heightMap[x, y] < z;
    }
}
