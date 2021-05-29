using Microsoft.Xna.Framework;
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
            double r = (2.0 / 3.0) * (position.X + position.Y + position.Z);
            double xr = r - position.X;
            double yr = r - position.Y;
            double zr = r - position.Z;

            // Evaluate both lattices to form a BCC lattice.
            return noise3_BCC(xr, yr, zr);
        }

        /**
         * Generate overlapping cubic lattices for 3D Re-oriented BCC noise.
         * Lookup table implementation inspired by DigitalShadow.
         * It was actually faster to narrow down the points in the loop itself,
         * than to build up the index with enough info to isolate 4 points.
         */
        private double noise3_BCC(double xr, double yr, double zr)
        {
            // Get base and offsets inside cube of first lattice.
            int xrb = (int)Math.Floor(xr);
            int yrb = (int)Math.Floor(yr);
            int zrb = (int)Math.Floor(zr);

            double xri = xr - xrb;
            double yri = yr - yrb;
            double zri = zr - zrb;

            // Identify which octant of the cube we're in. This determines which cell
            // in the other cubic lattice we're in, and also narrows down one point on each.
            int xht = (int)(xri + 0.5);
            int yht = (int)(yri + 0.5);
            int zht = (int)(zri + 0.5);
            int index = (xht << 0) | (yht << 1) | (zht << 2);

            // Point contributions
            double value = 0;
            LatticePoint3D c = LOOKUP_3D[index];
            while (c != null)
            {
                double dxr = xri + c.dxr;
                double dyr = yri + c.dyr;
                double dzr = zri + c.dzr;
                double attn = 0.5 - dxr * dxr - dyr * dyr - dzr * dzr;
                if (attn < 0)
                {
                    c = c.NextOnFailure;
                }
                else
                {
                    int pxm = (xrb + c.xrv) & PMASK;
                    int pym = (yrb + c.yrv) & PMASK;
                    int pzm = (zrb + c.zrv) & PMASK;
                    Grad3 grad = permGrad3[perm[perm[pxm] ^ pym] ^ pzm];
                    double extrapolation = grad.dx * dxr + grad.dy * dyr + grad.dz * dzr;

                    attn *= attn;
                    value += attn * attn * extrapolation;
                    c = c.NextOnSuccess;
                }
            }
            return value;
        }

        private struct Grad3
        {
            public double dx, dy, dz;
            public Grad3(double dx, double dy, double dz)
            {
                this.dx = dx;
                this.dy = dy;
                this.dz = dz;
            }
        }

        private class LatticePoint3D
        {
            public double dxr, dyr, dzr;
            public int xrv, yrv, zrv;

            public LatticePoint3D NextOnFailure, NextOnSuccess;
            public LatticePoint3D(int xrv, int yrv, int zrv, int lattice)
            {
                this.dxr = -xrv + lattice * 0.5;
                this.dyr = -yrv + lattice * 0.5;
                this.dzr = -zrv + lattice * 0.5;
                this.xrv = xrv + lattice * 1024;
                this.yrv = yrv + lattice * 1024;
                this.zrv = zrv + lattice * 1024;
            }
        }

        private const int PSIZE = 2048;
        private const int PMASK = 2047;
        private const double N3 = 0.030485933181293584;
        private readonly short[] perm;
        private readonly Grad3[] permGrad3;
        private readonly LatticePoint3D[] LOOKUP_3D;
        private readonly Grad3[] GRADIENTS_3D;

        public OpenSimplexNoise3D(long seed)
        {
            perm = new short[PSIZE];
            permGrad3 = new Grad3[PSIZE];
            short[] source = new short[PSIZE];
            for (short i = 0; i < PSIZE; i++)
                source[i] = i;
            for (int i = PSIZE - 1; i >= 0; i--)
            {
                seed = seed * 6364136223846793005L + 1442695040888963407L;
                int r = (int)((seed + 31) % (i + 1));
                if (r < 0)
                    r += (i + 1);
                perm[i] = source[r];
                permGrad3[i] = GRADIENTS_3D[perm[i]];
                source[r] = source[i];
            }

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


            GRADIENTS_3D = new Grad3[PSIZE];
            Grad3[] grad3 = {
                new Grad3(-2.22474487139,      -2.22474487139,      -1.0),
                new Grad3(-2.22474487139,      -2.22474487139,       1.0),
                new Grad3(-3.0862664687972017, -1.1721513422464978,  0.0),
                new Grad3(-1.1721513422464978, -3.0862664687972017,  0.0),
                new Grad3(-2.22474487139,      -1.0,                -2.22474487139),
                new Grad3(-2.22474487139,       1.0,                -2.22474487139),
                new Grad3(-1.1721513422464978,  0.0,                -3.0862664687972017),
                new Grad3(-3.0862664687972017,  0.0,                -1.1721513422464978),
                new Grad3(-2.22474487139,      -1.0,                 2.22474487139),
                new Grad3(-2.22474487139,       1.0,                 2.22474487139),
                new Grad3(-3.0862664687972017,  0.0,                 1.1721513422464978),
                new Grad3(-1.1721513422464978,  0.0,                 3.0862664687972017),
                new Grad3(-2.22474487139,       2.22474487139,      -1.0),
                new Grad3(-2.22474487139,       2.22474487139,       1.0),
                new Grad3(-1.1721513422464978,  3.0862664687972017,  0.0),
                new Grad3(-3.0862664687972017,  1.1721513422464978,  0.0),
                new Grad3(-1.0,                -2.22474487139,      -2.22474487139),
                new Grad3( 1.0,                -2.22474487139,      -2.22474487139),
                new Grad3( 0.0,                -3.0862664687972017, -1.1721513422464978),
                new Grad3( 0.0,                -1.1721513422464978, -3.0862664687972017),
                new Grad3(-1.0,                -2.22474487139,       2.22474487139),
                new Grad3( 1.0,                -2.22474487139,       2.22474487139),
                new Grad3( 0.0,                -1.1721513422464978,  3.0862664687972017),
                new Grad3( 0.0,                -3.0862664687972017,  1.1721513422464978),
                new Grad3(-1.0,                 2.22474487139,      -2.22474487139),
                new Grad3( 1.0,                 2.22474487139,      -2.22474487139),
                new Grad3( 0.0,                 1.1721513422464978, -3.0862664687972017),
                new Grad3( 0.0,                 3.0862664687972017, -1.1721513422464978),
                new Grad3(-1.0,                 2.22474487139,       2.22474487139),
                new Grad3( 1.0,                 2.22474487139,       2.22474487139),
                new Grad3( 0.0,                 3.0862664687972017,  1.1721513422464978),
                new Grad3( 0.0,                 1.1721513422464978,  3.0862664687972017),
                new Grad3( 2.22474487139,      -2.22474487139,      -1.0),
                new Grad3( 2.22474487139,      -2.22474487139,       1.0),
                new Grad3( 1.1721513422464978, -3.0862664687972017,  0.0),
                new Grad3( 3.0862664687972017, -1.1721513422464978,  0.0),
                new Grad3( 2.22474487139,      -1.0,                -2.22474487139),
                new Grad3( 2.22474487139,       1.0,                -2.22474487139),
                new Grad3( 3.0862664687972017,  0.0,                -1.1721513422464978),
                new Grad3( 1.1721513422464978,  0.0,                -3.0862664687972017),
                new Grad3( 2.22474487139,      -1.0,                 2.22474487139),
                new Grad3( 2.22474487139,       1.0,                 2.22474487139),
                new Grad3( 1.1721513422464978,  0.0,                 3.0862664687972017),
                new Grad3( 3.0862664687972017,  0.0,                 1.1721513422464978),
                new Grad3( 2.22474487139,       2.22474487139,      -1.0),
                new Grad3( 2.22474487139,       2.22474487139,       1.0),
                new Grad3( 3.0862664687972017,  1.1721513422464978,  0.0),
                new Grad3( 1.1721513422464978,  3.0862664687972017,  0.0)
            };
            for (int i = 0; i < grad3.Length; i++)
            {
                grad3[i].dx /= N3; grad3[i].dy /= N3; grad3[i].dz /= N3;
            }
            for (int i = 0; i < PSIZE; i++)
            {
                GRADIENTS_3D[i] = grad3[i % grad3.Length];
            }
        }
    }
}
