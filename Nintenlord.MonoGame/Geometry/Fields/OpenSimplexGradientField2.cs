using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class OpenSimplexGradientField2 : IVectorField2iTo2v
    {
        private const int PSIZE = 1 << 11;
        private const int PMASK = PSIZE - 1;

        static readonly Vector2[] GRADIENTS_2D;

        static OpenSimplexGradientField2()
        {
            GRADIENTS_2D = new Vector2[PSIZE];
            const float N2 = 0.01001634121365712f;
            const float c1a = 0.130526192220052f;
            const float c1b = 0.99144486137381f;
            const float c2a = 0.38268343236509f;
            const float c2b = 0.923879532511287f;
            const float c3a = 0.608761429008721f;
            const float c3b = 0.793353340291235f;
            Vector2[] Vector2 = {
                new Vector2( c1a,  c1b),
                new Vector2( c2a,   c2b),
                new Vector2( c3a,  c3b),
                new Vector2( c3b,  c3a),
                new Vector2( c2b,  c2a),
                new Vector2( c1b,   c1a),
                new Vector2( c1b,  -c1a),
                new Vector2( c2b, -c2a),
                new Vector2( c3b, -c3a),
                new Vector2( c3a, -c3b),
                new Vector2( c2a,  -c2b),
                new Vector2( c1a, -c1b),
                new Vector2(-c1a, -c1b),
                new Vector2(-c2a,  -c2b),
                new Vector2(-c3a, -c3b),
                new Vector2(-c3b, -c3a),
                new Vector2(-c2b, -c2a),
                new Vector2(-c1b,  -c1a),
                new Vector2(-c1b,   c1a),
                new Vector2(-c2b,  c2a),
                new Vector2(-c3b,  c3a),
                new Vector2(-c3a,  c3b),
                new Vector2(-c2a,   c2b),
                new Vector2(-c1a,  c1b)
            };
            for (int i = 0; i < Vector2.Length; i++)
            {
                Vector2[i] = Vector2[i] / N2;
            }
            for (int i = 0; i < PSIZE; i++)
            {
                GRADIENTS_2D[i] = Vector2[i % Vector2.Length];
            }
        }

        private readonly short[] perm;
        private readonly Vector2[] permVector2;

        public OpenSimplexGradientField2(long seed)
        {
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
                permVector2[i] = GRADIENTS_2D[perm[i]];
                source[r] = source[i];
            }
        }

        public Vector2 this[int x, int y] => GetGradient(x, y);

        private Vector2 GetGradient(int x, int y)
        {
            x &= PMASK;
            y &= PMASK;
            return permVector2[perm[x] ^ y];
        }
    }
}
