using System;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class ZipField2i<T1, T2, TOut> : IField2i<TOut>
    {
        private readonly IField2i<T1> field1;
        private readonly IField2i<T2> field2;
        private readonly Func<T1, T2, TOut> zipper;

        public ZipField2i(IField2i<T1> field1, IField2i<T2> field2, Func<T1, T2, TOut> zipper)
        {
            this.field1 = field1;
            this.field2 = field2;
            this.zipper = zipper;
        }

        public TOut this[int x, int y] => zipper(field1[x, y], field2[x, y]);
    }
}
