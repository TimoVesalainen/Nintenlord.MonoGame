using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;
using System;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class OpenSimplexNoise4D : INoise4D
    {
        readonly IVectorField4iTo4v gradientField;
        private static LatticePoint4D[] VERTICES_4D;

        public OpenSimplexNoise4D(IVectorField4iTo4v gradientField)
        {
            this.gradientField = gradientField;

            VERTICES_4D = new LatticePoint4D[16];
            for (int i = 0; i < VERTICES_4D.Length; i++)
            {
                VERTICES_4D[i] = new LatticePoint4D((i >> 0) & 1, (i >> 1) & 1, (i >> 2) & 1, (i >> 3) & 1);
            }
        }

        private struct LatticePoint4D
        {
            public int xsv, ysv, zsv, wsv;
            public double dx, dy, dz, dw;
            public double xsi, ysi, zsi, wsi;
            public double ssiDelta;
            public LatticePoint4D(int xsv, int ysv, int zsv, int wsv)
            {
                this.xsv = xsv + 409;
                this.ysv = ysv + 409;
                this.zsv = zsv + 409;
                this.wsv = wsv + 409;
                double ssv = (xsv + ysv + zsv + wsv) * 0.309016994374947;
                this.dx = -xsv - ssv;
                this.dy = -ysv - ssv;
                this.dz = -zsv - ssv;
                this.dw = -wsv - ssv;
                this.xsi = xsi = 0.2 - xsv;
                this.ysi = ysi = 0.2 - ysv;
                this.zsi = zsi = 0.2 - zsv;
                this.wsi = wsi = 0.2 - wsv;
                this.ssiDelta = (0.8 - xsv - ysv - zsv - wsv) * 0.309016994374947;
            }
        }

        public double Noise(Vector4 position)
        {
            // Get points for A4 lattice
            double s = -0.138196601125011 * (Vector4.Dot(position, Vector4.One));
            double xs = position.X + s, ys = position.Y + s, zs = position.Z + s, ws = position.W + s;

            return noise4_Base(xs, ys, zs, ws);
        }

        /**
         * 4D OpenSimplex2F noise base.
         * Current implementation not fully optimized by lookup tables.
         * But still comes out slightly ahead of Gustavson's Simplex in tests.
         */
        private double noise4_Base(double xs, double ys, double zs, double ws)
        {
            double value = 0;

            // Get base points and offsets
            int xsb = (int)Math.Floor(xs), ysb = (int)Math.Floor(ys), zsb = (int)Math.Floor(zs), wsb = (int)Math.Floor(ws);
            double xsi = xs - xsb, ysi = ys - ysb, zsi = zs - zsb, wsi = ws - wsb;

            // If we're in the lower half, flip so we can repeat the code for the upper half. We'll flip back later.
            double siSum = xsi + ysi + zsi + wsi;
            double ssi = siSum * 0.309016994374947; // Prep for vertex contributions.
            bool inLowerHalf = (siSum < 2);
            if (inLowerHalf)
            {
                xsi = 1 - xsi; ysi = 1 - ysi; zsi = 1 - zsi; wsi = 1 - wsi;
                siSum = 4 - siSum;
            }

            // Consider opposing vertex pairs of the octahedron formed by the central cross-section of the stretched tesseract
            double aabb = xsi + ysi - zsi - wsi, abab = xsi - ysi + zsi - wsi, abba = xsi - ysi - zsi + wsi;
            double aabbScore = Math.Abs(aabb), ababScore = Math.Abs(abab), abbaScore = Math.Abs(abba);

            // Find the closest point on the stretched tesseract as if it were the upper half
            int vertexIndex, via, vib;
            double asi, bsi;
            if (aabbScore > ababScore && aabbScore > abbaScore)
            {
                if (aabb > 0)
                {
                    asi = zsi; bsi = wsi; vertexIndex = 0b0011; via = 0b0111; vib = 0b1011;
                }
                else
                {
                    asi = xsi; bsi = ysi; vertexIndex = 0b1100; via = 0b1101; vib = 0b1110;
                }
            }
            else if (ababScore > abbaScore)
            {
                if (abab > 0)
                {
                    asi = ysi; bsi = wsi; vertexIndex = 0b0101; via = 0b0111; vib = 0b1101;
                }
                else
                {
                    asi = xsi; bsi = zsi; vertexIndex = 0b1010; via = 0b1011; vib = 0b1110;
                }
            }
            else
            {
                if (abba > 0)
                {
                    asi = ysi; bsi = zsi; vertexIndex = 0b1001; via = 0b1011; vib = 0b1101;
                }
                else
                {
                    asi = xsi; bsi = wsi; vertexIndex = 0b0110; via = 0b0111; vib = 0b1110;
                }
            }
            if (bsi > asi)
            {
                via = vib;
                double temp = bsi;
                bsi = asi;
                asi = temp;
            }
            if (siSum + asi > 3)
            {
                vertexIndex = via;
                if (siSum + bsi > 4)
                {
                    vertexIndex = 0b1111;
                }
            }

            // Now flip back if we're actually in the lower half.
            if (inLowerHalf)
            {
                xsi = 1 - xsi; ysi = 1 - ysi; zsi = 1 - zsi; wsi = 1 - wsi;
                vertexIndex ^= 0b1111;
            }

            // Five points to add, total, from five copies of the A4 lattice.
            for (int i = 0; i < 5; i++)
            {

                // Update xsb/etc. and add the lattice point's contribution.
                LatticePoint4D c = VERTICES_4D[vertexIndex];
                xsb += c.xsv; ysb += c.ysv; zsb += c.zsv; wsb += c.wsv;
                double xi = xsi + ssi, yi = ysi + ssi, zi = zsi + ssi, wi = wsi + ssi;
                double dx = xi + c.dx, dy = yi + c.dy, dz = zi + c.dz, dw = wi + c.dw;
                double attn = 0.5 - dx * dx - dy * dy - dz * dz - dw * dw;
                if (attn > 0)
                {
                    var grad = gradientField[xsb, ysb, zsb, wsb];
                    double ramped = grad.X * dx + grad.Y * dy + grad.Z * dz + grad.W * dw;

                    attn *= attn;
                    value += attn * attn * ramped;
                }

                // Maybe this helps the compiler/JVM/LLVM/etc. know we can end the loop here. Maybe not.
                if (i == 4) break;

                // Update the relative skewed coordinates to reference the vertex we just added.
                // Rather, reference its counterpart on the lattice copy that is shifted down by
                // the vector <-0.2, -0.2, -0.2, -0.2>
                xsi += c.xsi; ysi += c.ysi; zsi += c.zsi; wsi += c.wsi;
                ssi += c.ssiDelta;

                // Next point is the closest vertex on the 4-simplex whose base vertex is the aforementioned vertex.
                double score0 = 1.0 + ssi * (-1.0 / 0.309016994374947); // Seems slightly faster than 1.0-xsi-ysi-zsi-wsi
                vertexIndex = 0b0000;
                if (xsi >= ysi && xsi >= zsi && xsi >= wsi && xsi >= score0)
                {
                    vertexIndex = 0b0001;
                }
                else if (ysi > xsi && ysi >= zsi && ysi >= wsi && ysi >= score0)
                {
                    vertexIndex = 0b0010;
                }
                else if (zsi > xsi && zsi > ysi && zsi >= wsi && zsi >= score0)
                {
                    vertexIndex = 0b0100;
                }
                else if (wsi > xsi && wsi > ysi && wsi > zsi && wsi >= score0)
                {
                    vertexIndex = 0b1000;
                }
            }

            return value;
        }
    }
}
