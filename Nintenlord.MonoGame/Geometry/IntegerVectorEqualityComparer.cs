using System.Collections.Generic;

namespace Nintenlord.MonoGame.Geometry
{
    public sealed class IntegerVectorEqualityComparer : IEqualityComparer<IntegerVector3>
    {
        #region IEqualityComparer<IntegerVector3> Members

        public bool Equals(IntegerVector3 x, IntegerVector3 y)
        {
            return x.X == y.X && x.Y == y.Y && x.Z == y.Z;
        }

        public int GetHashCode(IntegerVector3 obj)
        {
            int xHash = obj.X & 0x1FFF;
            int yHash = obj.Y & 0x1FFF;
            int zHash = obj.Z & 0x1F;

            //z = 5 bits, y, x = 13
            return zHash | (yHash << 5) | (xHash << (5 + 13));
        }

        #endregion
    }
}