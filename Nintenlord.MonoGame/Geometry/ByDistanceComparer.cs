using Nintenlord.MonoGame.Geometry.Vectors;
using System;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.Geometry
{
    public class ByDistanceComparer : IComparer<IntegerVector3>
    {
        private readonly IntegerVector3 zero;

        public ByDistanceComparer(IntegerVector3 zero)
        {
            this.zero = zero;
        }

        #region IComparer<IntegerVector3> Members

        public int Compare(IntegerVector3 x, IntegerVector3 y)
        {
            int dx = IntegerVector3.DistanceSquared(x, zero);
            int dy = IntegerVector3.DistanceSquared(y, zero);

            if (dx < dy)
            {
                return -1;
            }
            else if (dx > dy)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }

    public class ByDistanceComparer<T> : IComparer<T>
    {
        private readonly IntegerVector3 zero;
        private readonly Func<T, IntegerVector3> getPosition;

        public ByDistanceComparer(IntegerVector3 zero, Func<T, IntegerVector3> getPosition)
        {
            this.zero = zero;
            this.getPosition = getPosition;
        }

        #region IComparer<IntegerVector3> Members

        public int Compare(T x, T y)
        {
            int dx = IntegerVector3.DistanceSquared(getPosition(x), zero);
            int dy = IntegerVector3.DistanceSquared(getPosition(y), zero);

            if (dx < dy)
            {
                return -1;
            }
            else if (dx > dy)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}