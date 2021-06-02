using System;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class ZipField3i<T1, T2, TOut> : IField3i<TOut>
    {
        private readonly IField3i<T1> field1;
        private readonly IField3i<T2> field2;
        private readonly Func<T1, T2, TOut> zipper;

        public ZipField3i(IField3i<T1> field1, IField3i<T2> field2, Func<T1, T2, TOut> zipper)
        {
            this.field1 = field1;
            this.field2 = field2;
            this.zipper = zipper;
        }

        public TOut this[int x, int y, int z] => zipper(field1[x, y, z], field2[x, y, z]);
    }
}
