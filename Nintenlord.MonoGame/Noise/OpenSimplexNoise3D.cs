using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry;
using Nintenlord.MonoGame.Geometry.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class OpenSimplexNoise3D : INoise3D
    {
        /**
         * 3D Re-oriented 4-point BCC noise, classic orientation.
         * Proper substitute for 3D Simplex in light of Forbidden Formulae.
         * Use noise3_XYBeforeZ or noise3_XZBeforeY instead, wherever appropriate.
         */
        public double Noise(Vector3 position)
        {

            // Re-orient the cubic lattices via rotation, to produce the expected look on cardinal planar slices.
            // If texturing objects that don't tend to have cardinal plane faces, you could even remove this.
            // Orthonormal rotation. Not a skew transform.
            float r = (2.0f / 3.0f) * (position.X + position.Y + position.Z);
            var rv = new Vector3(r) - position;

            // Evaluate both lattices to form a BCC lattice.
            return noise3_BCC(rv);
        }

        /**
         * Generate overlapping cubic lattices for 3D Re-oriented BCC noise.
         * Lookup table implementation inspired by DigitalShadow.
         * It was actually faster to narrow down the points in the loop itself,
         * than to build up the index with enough info to isolate 4 points.
         */
        private double noise3_BCC(Vector3 position)
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
            LatticePoint3D c = LOOKUP_3D[octantIndex];
            while (c != null)
            {
                Vector3 dr = inCubeCoordinate + c.dr;
                float attn = 0.5f - dr.LengthSquared();
                if (attn < 0)
                {
                    c = c.NextOnFailure;
                }
                else
                {
                    IntegerVector3 gradientPositions = cubeCoordinate + c.rv;
                    Vector3 gradient = gradientField[gradientPositions.X, gradientPositions.Y, gradientPositions.Z];
                    float extrapolation = Vector3.Dot(gradient, dr);

                    attn *= attn;
                    value += attn * attn * extrapolation;
                    c = c.NextOnSuccess;
                }
            }
            return value;
        }

        private class LatticePoint3D
        {
            public Vector3 dr;
            public IntegerVector3 rv;

            public LatticePoint3D NextOnFailure, NextOnSuccess;
            public LatticePoint3D(int xrv, int yrv, int zrv, int lattice)
            {
                var prv = new IntegerVector3(xrv, yrv, zrv);

                dr = new Vector3(0.5f) * lattice - (Vector3)prv;
                rv = new IntegerVector3(1024) * lattice + prv;
            }
        }

        private readonly LatticePoint3D[] LOOKUP_3D;
        private readonly IVectorField3iTo3v gradientField;

        public OpenSimplexNoise3D(IVectorField3iTo3v gradientField)
        {
            this.gradientField = gradientField;

            LOOKUP_3D = new LatticePoint3D[8];
            for (int i = 0; i < 8; i++)
            {
                int i1, j1, k1, i2, j2, k2;
                i1 = (i >> 0) & 1;
                j1 = (i >> 1) & 1;
                k1 = (i >> 2) & 1;
                i2 = i1 ^ 1;
                j2 = j1 ^ 1;
                k2 = k1 ^ 1;

                // The two points within this octant, one from each of the two cubic half-lattices.
                LatticePoint3D c0 = new LatticePoint3D(i1, j1, k1, 0);
                LatticePoint3D c1 = new LatticePoint3D(i1 + i2, j1 + j2, k1 + k2, 1);

                // Each single step away on the first half-lattice.
                LatticePoint3D c2 = new LatticePoint3D(i2, j1, k1, 0);
                LatticePoint3D c3 = new LatticePoint3D(i1, j2, k1, 0);
                LatticePoint3D c4 = new LatticePoint3D(i1, j1, k2, 0);

                // Each single step away on the second half-lattice.
                LatticePoint3D c5 = new LatticePoint3D(i1 + i1, j1 + j2, k1 + k2, 1);
                LatticePoint3D c6 = new LatticePoint3D(i1 + i2, j1 + j1, k1 + k2, 1);
                LatticePoint3D c7 = new LatticePoint3D(i1 + i2, j1 + j2, k1 + k1, 1);

                // First two are guaranteed.
                c0.NextOnFailure = c0.NextOnSuccess = c1;
                c1.NextOnFailure = c1.NextOnSuccess = c2;

                // Once we find one on the first half-lattice, the rest are out.
                // In addition, knowing c2 rules out c5.
                c2.NextOnFailure = c3; c2.NextOnSuccess = c6;
                c3.NextOnFailure = c4; c3.NextOnSuccess = c5;
                c4.NextOnFailure = c4.NextOnSuccess = c5;

                // Once we find one on the second half-lattice, the rest are out.
                c5.NextOnFailure = c6; c5.NextOnSuccess = null;
                c6.NextOnFailure = c7; c6.NextOnSuccess = null;
                c7.NextOnFailure = c7.NextOnSuccess = null;

                LOOKUP_3D[i] = c0;
            }
        }
    }
}
