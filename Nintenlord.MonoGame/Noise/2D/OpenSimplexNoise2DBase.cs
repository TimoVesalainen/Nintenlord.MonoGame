﻿using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;
using System;

namespace Nintenlord.MonoGame.Noise
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexNoise2DBase : INoise2D
    {
        readonly IVectorField2iTo2v gradients;
        readonly LatticePoint2D[] LOOKUP_2D;

        public OpenSimplexNoise2DBase(IVectorField2iTo2v gradients)
        {
            this.gradients = gradients;
            LOOKUP_2D = new LatticePoint2D[4];

            LOOKUP_2D[0] = new LatticePoint2D(1, 0);
            LOOKUP_2D[1] = new LatticePoint2D(0, 0);
            LOOKUP_2D[2] = new LatticePoint2D(1, 1);
            LOOKUP_2D[3] = new LatticePoint2D(0, 1);
        }

        private struct LatticePoint2D
        {
            public int xsv, ysv;
            public Vector2 d;
            public LatticePoint2D(int xsv, int ysv)
            {
                this.xsv = xsv; this.ysv = ysv;
                float ssv = (xsv + ysv) * -0.211324865405187f;
                d = new Vector2(-xsv - ssv, -ysv - ssv);
            }
        }

        /**
         * 2D Simplex noise base.
         * Lookup table implementation inspired by DigitalShadow.
         */
        public double Noise(Vector2 position)
        {
            // Get base points and offsets
            int xsb = (int)Math.Floor(position.X);
            int ysb = (int)Math.Floor(position.Y);

            Vector2 si = position - new Vector2(xsb, ysb);

            // Index to point list
            int index = (int)((si.Y - si.X) / 2 + 1);

            float ssi = (si.X + si.Y) * -0.211324865405187f;
            Vector2 iVec = si + new Vector2(ssi);

            double value = 0;
            // Point contributions
            for (int i = 0; i < 3; i++)
            {
                LatticePoint2D c = LOOKUP_2D[index + i];

                Vector2 d = iVec + c.d;
                double attn = 0.5 - d.LengthSquared();

                if (attn <= 0) continue;

                Vector2 grad = gradients[xsb + c.xsv, ysb + c.ysv];
                var extrapolation = Vector2.Dot(d, grad);

                attn *= attn;
                value += attn * attn * extrapolation;
            }

            return value;
        }
    }
}
