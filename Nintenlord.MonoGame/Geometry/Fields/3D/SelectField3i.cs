using System;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class SelectField3i<TIn, TOut> : IField3i<TOut>
    {
        private readonly IField3i<TIn> field;
        private readonly Func<TIn, TOut> selector;

        public SelectField3i(IField3i<TIn> field, Func<TIn, TOut> selector)
        {
            this.field = field;
            this.selector = selector;
        }

        public TOut this[int x, int y, int z] => selector(field[x, y, z]);
    }
}
