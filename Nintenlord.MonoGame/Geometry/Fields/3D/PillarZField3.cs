using System;

namespace Nintenlord.MonoGame.Geometry.Fields._3D
{
    public sealed class PillarZField3<TIn, TOut> : IField3i<TOut>
    {
        private readonly IField2i<TIn> pillarHeight;
        private readonly Func<TIn, int, TOut> toPillarBlock;


        public PillarZField3(IField2i<TIn> heightMap, Func<TIn, int, TOut> toPillarBlock)
        {
            this.pillarHeight = heightMap;
            this.toPillarBlock = toPillarBlock;
        }

        public TOut this[int x, int y, int z] => toPillarBlock(pillarHeight[x, y], z);
    }
}
