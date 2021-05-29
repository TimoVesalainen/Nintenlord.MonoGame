using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class OpenSimplexGradientField3 : IVectorField3iTo3v
    {
        private const int PSIZE = 1 << 11;
        private const int PMASK = PSIZE - 1;
        private const float N3 = 0.030485933181293584f;

        static readonly Vector3[] GRADIENTS_3D;

        static OpenSimplexGradientField3()
        {
            var GRADIENTS_3D = new Vector3[PSIZE];
            const float c1 = 2.22474487139f;
            const float c2 = 3.0862664687972017f;
            const float c3 = 1.1721513422464978f;
            Vector3[] gradients = {
                new Vector3(-c1, -c1, -1.0f),
                new Vector3(-c1, -c1, 1.0f),
                new Vector3(-c2, -c3, 0.0f),
                new Vector3(-c3, -c2, 0.0f),
                new Vector3(-c1, -1.0f, -c1),
                new Vector3(-c1, 1.0f, -c1),
                new Vector3(-c3, 0.0f, -c2),
                new Vector3(-c2, 0.0f, -c3),
                new Vector3(-c1, -1.0f, c1),
                new Vector3(-c1, 1.0f, c1),
                new Vector3(-c2, 0.0f, c3),
                new Vector3(-c3, 0.0f, c2),
                new Vector3(-c1, c1, -1.0f),
                new Vector3(-c1, c1, 1.0f),
                new Vector3(-c3, c2, 0.0f),
                new Vector3(-c2, c3, 0.0f),
                new Vector3(-1.0f, -c1, -c1),
                new Vector3( 1.0f, -c1, -c1),
                new Vector3( 0.0f, -c2, -c3),
                new Vector3( 0.0f, -c3, -c2),
                new Vector3(-1.0f, -c1, c1),
                new Vector3( 1.0f, -c1, c1),
                new Vector3( 0.0f, -c3, c2),
                new Vector3( 0.0f, -c2, c3),
                new Vector3(-1.0f, c1, -c1),
                new Vector3( 1.0f, c1, -c1),
                new Vector3( 0.0f, c3, -c2),
                new Vector3( 0.0f, c2, -c3),
                new Vector3(-1.0f, c1, c1),
                new Vector3( 1.0f, c1, c1),
                new Vector3( 0.0f, c2, c3),
                new Vector3( 0.0f, c3, c2),
                new Vector3( c1, -c1, -1.0f),
                new Vector3( c1, -c1, 1.0f),
                new Vector3( c3, -c2, 0.0f),
                new Vector3( c2, -c3, 0.0f),
                new Vector3( c1, -1.0f, -c1),
                new Vector3( c1, 1.0f, -c1),
                new Vector3( c2, 0.0f, -c3),
                new Vector3( c3, 0.0f, -c2),
                new Vector3( c1, -1.0f, c1),
                new Vector3( c1, 1.0f, c1),
                new Vector3( c3, 0.0f, c2),
                new Vector3( c2, 0.0f, c3),
                new Vector3( c1, c1, -1.0f),
                new Vector3( c1, c1, 1.0f),
                new Vector3( c2, c3, 0.0f),
                new Vector3( c3, c2, 0.0f)
            };
            for (int i = 0; i < gradients.Length; i++)
            {
                gradients[i] = gradients[i] / N3;
            }
            for (int i = 0; i < GRADIENTS_3D.Length; i++)
            {
                GRADIENTS_3D[i] = gradients[i % gradients.Length];
            }
        }

        private readonly short[] perm;
        private readonly Vector3[] permVector3;

        public OpenSimplexGradientField3(long seed)
        {
            perm = new short[PSIZE];
            permVector3 = new Vector3[PSIZE];
            short[] source = new short[PSIZE];

            for (short i = 0; i < source.Length; i++)
            {
                source[i] = i;
            }

            for (int i = PSIZE - 1; i >= 0; i--)
            {
                seed = seed * 6364136223846793005L + 1442695040888963407L;
                int r = (int)((seed + 31) % (i + 1));
                if (r < 0)
                    r += (i + 1);
                perm[i] = source[r];
                permVector3[i] = GRADIENTS_3D[perm[i]];
                source[r] = source[i];
            }
        }

        public Vector3 this[int x, int y, int z] => GetGradient(x, y, z);

        private Vector3 GetGradient(int x, int y, int z)
        {
            x &= PMASK;
            y &= PMASK;
            z &= PMASK;

            return permVector3[perm[perm[x] ^ y] ^ z];
        }
    }
}
