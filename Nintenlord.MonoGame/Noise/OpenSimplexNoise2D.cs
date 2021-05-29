using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;
using System;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class OpenSimplexNoise2D : INoise2D
    {
        readonly IVectorField2iTo2v gradients;

        public double Noise(Vector2 position)
        {
            // Get points for A2* lattice
            double s = 0.366025403784439 * (position.X + position.Y);
            double xs = position.X + s, ys = position.Y + s;

            return noise2_Base(xs, ys);
        }

        private static LatticePoint2D[] LOOKUP_2D;

        private struct LatticePoint2D
        {
            public int xsv, ysv;
            public double dx, dy;
            public LatticePoint2D(int xsv, int ysv)
            {
                this.xsv = xsv; this.ysv = ysv;
                double ssv = (xsv + ysv) * -0.211324865405187;
                this.dx = -xsv - ssv;
                this.dy = -ysv - ssv;
            }
        }

        /**
         * 2D Simplex noise base.
         * Lookup table implementation inspired by DigitalShadow.
         */
        private double noise2_Base(double xs, double ys)
        {
            double value = 0;

            // Get base points and offsets
            int xsb = (int)Math.Floor(xs), ysb = (int)Math.Floor(ys);
            double xsi = xs - xsb, ysi = ys - ysb;

            // Index to point list
            int index = (int)((ysi - xsi) / 2 + 1);

            double ssi = (xsi + ysi) * -0.211324865405187;
            double xi = xsi + ssi, yi = ysi + ssi;

            // Point contributions
            for (int i = 0; i < 3; i++)
            {
                LatticePoint2D c = LOOKUP_2D[index + i];

                double dx = xi + c.dx;
                double dy = yi + c.dy;
                double attn = 0.5 - dx * dx - dy * dy;
                if (attn <= 0) continue;

                Vector2 grad = gradients[xsb + c.xsv, ysb + c.ysv];
                double extrapolation = grad.X * dx + grad.Y * dy;

                attn *= attn;
                value += attn * attn * extrapolation;
            }

            return value;
        }
    }
}
