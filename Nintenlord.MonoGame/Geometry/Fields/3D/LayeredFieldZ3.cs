using System;

namespace Nintenlord.MonoGame.Geometry.Fields._3D
{
    public sealed class LayeredFieldZ3<T> : IField3i<T>
    {
        private readonly Func<int, IField3i<T>> getFieldForLayer;

        public LayeredFieldZ3(Func<int, IField3i<T>> getFieldForLayer)
        {
            this.getFieldForLayer = getFieldForLayer;
        }

        public T this[int x, int y, int z] => getFieldForLayer(z)[x, y, z];
    }
}
