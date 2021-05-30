using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;
using System;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise4DBase : INoise4D
    {
        readonly IVectorField4iTo4v gradientField;
        private static LatticePoint4D[] VERTICES_4D;

        public OpenSimplexNoise4DBase(IVectorField4iTo4v gradientField)
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
            public Vector4 d;
            public Vector4 si;
            public float ssiDelta;
            public LatticePoint4D(int xsv, int ysv, int zsv, int wsv)
            {
                this.xsv = xsv + 409;
                this.ysv = ysv + 409;
                this.zsv = zsv + 409;
                this.wsv = wsv + 409;
                float ssv = (xsv + ysv + zsv + wsv) * 0.309016994374947f;

                this.d = new Vector4(-xsv - ssv, -ysv - ssv, -zsv - ssv, -wsv - ssv);
                this.si = new Vector4(0.2f - xsv, 0.2f - ysv, 0.2f - zsv, 0.2f - wsv);
                this.ssiDelta = (0.8f - xsv - ysv - zsv - wsv) * 0.309016994374947f;
            }
        }

        public double Noise(Vector4 position)
        {
            // Get points for A4 lattice
            float s = -0.138196601125011f * (Vector4.Dot(position, Vector4.One));

            return noise4_Base(position + new Vector4(s));
        }

        /**
         * 4D OpenSimplex2F noise base.
         * Current implementation not fully optimized by lookup tables.
         * But still comes out slightly ahead of Gustavson's Simplex in tests.
         */
        private double noise4_Base(Vector4 s)
        {
            // Get base points and offsets
            int xsb = (int)Math.Floor(s.X);
            int ysb = (int)Math.Floor(s.Y);
            int zsb = (int)Math.Floor(s.Z);
            int wsb = (int)Math.Floor(s.W);
            Vector4 si = s - new Vector4(xsb, ysb, zsb, wsb);

            // If we're in the lower half, flip so we can repeat the code for the upper half. We'll flip back later.
            float siSum = Vector4.Dot(si, Vector4.One);
            float ssi = siSum * 0.309016994374947f; // Prep for vertex contributions.
            bool inLowerHalf = (siSum < 2);
            if (inLowerHalf)
            {
                si = Vector4.One - si;
                siSum = 4 - siSum;
            }

            // Consider opposing vertex pairs of the octahedron formed by the central cross-section of the stretched tesseract
            double aabb = Vector4.Dot(si, new Vector4(1, 1, -1, -1));
            double abab = Vector4.Dot(si, new Vector4(1, -1, 1, -1));
            double abba = Vector4.Dot(si, new Vector4(1, -1, -1, 1));
            double aabbScore = Math.Abs(aabb);
            double ababScore = Math.Abs(abab);
            double abbaScore = Math.Abs(abba);

            // Find the closest point on the stretched tesseract as if it were the upper half
            int vertexIndex, via, vib;
            double asi, bsi;
            if (aabbScore > ababScore && aabbScore > abbaScore)
            {
                if (aabb > 0)
                {
                    asi = si.Z;
                    bsi = si.W;
                    vertexIndex = 0b0011;
                    via = 0b0111;
                    vib = 0b1011;
                }
                else
                {
                    asi = si.X;
                    bsi = si.Y;
                    vertexIndex = 0b1100;
                    via = 0b1101;
                    vib = 0b1110;
                }
            }
            else if (ababScore > abbaScore)
            {
                if (abab > 0)
                {
                    asi = si.Y;
                    bsi = si.W;
                    vertexIndex = 0b0101;
                    via = 0b0111;
                    vib = 0b1101;
                }
                else
                {
                    asi = si.X;
                    bsi = si.Z;
                    vertexIndex = 0b1010;
                    via = 0b1011;
                    vib = 0b1110;
                }
            }
            else
            {
                if (abba > 0)
                {
                    asi = si.Y;
                    bsi = si.Z;
                    vertexIndex = 0b1001;
                    via = 0b1011;
                    vib = 0b1101;
                }
                else
                {
                    asi = si.X;
                    bsi = si.W;
                    vertexIndex = 0b0110;
                    via = 0b0111;
                    vib = 0b1110;
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
                si = Vector4.One - si;
                vertexIndex ^= 0b1111;
            }

            double value = 0;
            // Five points to add, total, from five copies of the A4 lattice.
            for (int i = 0; i < 5; i++)
            {

                // Update xsb/etc. and add the lattice point's contribution.
                LatticePoint4D c = VERTICES_4D[vertexIndex];
                xsb += c.xsv;
                ysb += c.ysv;
                zsb += c.zsv;
                wsb += c.wsv;
                Vector4 iVec = si + new Vector4(ssi);
                Vector4 d = iVec + c.d;

                double attn = 0.5 - d.LengthSquared();
                if (attn > 0)
                {
                    var grad = gradientField[xsb, ysb, zsb, wsb];
                    double ramped = Vector4.Dot(grad, d);

                    attn *= attn;
                    value += attn * attn * ramped;
                }

                // Maybe this helps the compiler/JVM/LLVM/etc. know we can end the loop here. Maybe not.
                if (i == 4) break;

                // Update the relative skewed coordinates to reference the vertex we just added.
                // Rather, reference its counterpart on the lattice copy that is shifted down by
                // the vector <-0.2, -0.2, -0.2, -0.2>
                si += c.si;
                ssi += c.ssiDelta;

                // Next point is the closest vertex on the 4-simplex whose base vertex is the aforementioned vertex.
                double score0 = 1.0 + ssi * (-1.0 / 0.309016994374947); // Seems slightly faster than 1.0-xsi-ysi-zsi-wsi
                vertexIndex = 0b0000;
                if (si.X >= si.Y && si.X >= si.Z && si.X >= si.W && si.X >= score0)
                {
                    vertexIndex = 0b0001;
                }
                else if (si.Y > si.X && si.Y >= si.Z && si.Y >= si.W && si.Y >= score0)
                {
                    vertexIndex = 0b0010;
                }
                else if (si.Z > si.X && si.Z > si.Y && si.Z >= si.W && si.Z >= score0)
                {
                    vertexIndex = 0b0100;
                }
                else if (si.W > si.X && si.W > si.Y && si.W > si.Z && si.W >= score0)
                {
                    vertexIndex = 0b1000;
                }
            }

            return value;
        }
    }
}
