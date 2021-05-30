using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry;
using Nintenlord.MonoGame.Geometry.Fields;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise3DBase : INoise3D
    {
        /**
         * Generate overlapping cubic lattices for 3D Re-oriented BCC noise.
         * Lookup table implementation inspired by DigitalShadow.
         * It was actually faster to narrow down the points in the loop itself,
         * than to build up the index with enough info to isolate 4 points.
         */
        public double Noise(Vector3 position)
        {
            // Get base and offsets inside cube of first lattice.
            IntegerVector3 cubeCoordinate = IntegerVector3.Floor(position);

            Vector3 inCubeCoordinate = position - (Vector3)cubeCoordinate;

            // Identify which octant of the cube we're in. This determines which cell
            // in the other cubic lattice we're in, and also narrows down one point on each.
            IntegerVector3 latticeOctantCoord = (IntegerVector3)(inCubeCoordinate + Vector3.One * 0.5f);
            int octantIndex = (latticeOctantCoord.X << 0) | (latticeOctantCoord.Y << 1) | (latticeOctantCoord.Z << 2);

            // Point contributions
            double value = 0;
            LatticePoint3D[] c = LOOKUP_3D[octantIndex];
            int index = 0;
            while (index >= 0)
            {
                Vector3 dr = inCubeCoordinate + c[index].dr;
                float attn = 0.5f - dr.LengthSquared();
                if (attn < 0)
                {
                    index = c[index].NextOnFailure;
                }
                else
                {
                    IntegerVector3 gradientPositions = cubeCoordinate + c[index].rv;
                    Vector3 gradient = gradientField[gradientPositions.X, gradientPositions.Y, gradientPositions.Z];
                    float extrapolation = Vector3.Dot(gradient, dr);

                    attn *= attn;
                    value += attn * attn * extrapolation;
                    index = c[index].NextOnSuccess;
                }
            }
            return value;
        }

        private struct LatticePoint3D
        {
            public Vector3 dr;
            public IntegerVector3 rv;

            public int NextOnFailure, NextOnSuccess;
            public LatticePoint3D(int xrv, int yrv, int zrv, int lattice, int success, int failure)
            {
                var prv = new IntegerVector3(xrv, yrv, zrv);

                dr = new Vector3(0.5f) * lattice - (Vector3)prv;
                rv = new IntegerVector3(1024) * lattice + prv;
                NextOnFailure = failure;
                NextOnSuccess = success;
            }
        }

        private readonly LatticePoint3D[][] LOOKUP_3D;
        private readonly IVectorField3iTo3v gradientField;

        public OpenSimplexNoise3DBase(IVectorField3iTo3v gradientField)
        {
            this.gradientField = gradientField;

            LOOKUP_3D = new LatticePoint3D[8][];
            for (int i = 0; i < LOOKUP_3D.Length; i++)
            {
                LOOKUP_3D[i] = GetLattice(i);
            }
        }

        private static LatticePoint3D[] GetLattice(int index)
        {
            int i1, j1, k1, i2, j2, k2;
            i1 = (index >> 0) & 1;
            j1 = (index >> 1) & 1;
            k1 = (index >> 2) & 1;
            i2 = i1 ^ 1;
            j2 = j1 ^ 1;
            k2 = k1 ^ 1;

            // The two points within this octant, one from each of the two cubic half-lattices.
            LatticePoint3D c0 = new LatticePoint3D(i1, j1, k1, 0, 1, 1);
            LatticePoint3D c1 = new LatticePoint3D(i1 + i2, j1 + j2, k1 + k2, 1, 2, 2);

            // Each single step away on the first half-lattice.
            LatticePoint3D c2 = new LatticePoint3D(i2, j1, k1, 0, 3, 6);
            LatticePoint3D c3 = new LatticePoint3D(i1, j2, k1, 0, 4, 5);
            LatticePoint3D c4 = new LatticePoint3D(i1, j1, k2, 0, 5, 5);

            // Each single step away on the second half-lattice.
            LatticePoint3D c5 = new LatticePoint3D(i1 + i1, j1 + j2, k1 + k2, 1, 6, -1);
            LatticePoint3D c6 = new LatticePoint3D(i1 + i2, j1 + j1, k1 + k2, 1, 7, -1);
            LatticePoint3D c7 = new LatticePoint3D(i1 + i2, j1 + j2, k1 + k1, 1, -1, -1);

            return new[]
            {
                c0,
                c1,
                c2,
                c3,
                c4,
                c5,
                c6,
                c7
            };
        }
    }
}
