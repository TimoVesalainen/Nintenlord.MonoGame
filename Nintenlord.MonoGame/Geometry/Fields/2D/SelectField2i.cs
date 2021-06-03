using System;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class SelectField2i<TIn, TOut> : IField2i<TOut>
    {
        private readonly IField2i<TIn> field;
        private readonly Func<TIn, TOut> selector;

        public SelectField2i(IField2i<TIn> field, Func<TIn, TOut> selector)
        {
            this.field = field;
            this.selector = selector;
        }

        public TOut this[int x, int y] => selector(field[x, y]);
    }
}
