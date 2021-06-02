namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class PairField3i<T1, T2> : IField3i<(T1, T2)>
    {
        private readonly IField3i<T1> field1;
        private readonly IField3i<T2> field2;

        public PairField3i(IField3i<T1> field1, IField3i<T2> field2)
        {
            this.field1 = field1;
            this.field2 = field2;
        }

        public (T1, T2) this[int x, int y, int z] => (field1[x, y, z], field2[x, y, z]);
    }
}
