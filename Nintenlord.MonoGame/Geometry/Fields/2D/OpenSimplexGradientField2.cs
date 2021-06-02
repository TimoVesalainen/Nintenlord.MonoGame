using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexGradientField2 : IVectorField2iTo2v
    {
        private const int PERMUTATION_SIZE = 1 << 11;
        private const int PERMUTATION_MASK = PERMUTATION_SIZE - 1;
        private static readonly Vector2[] gradients;

        static OpenSimplexGradientField2()
        {
            gradients = new Vector2[PERMUTATION_SIZE];
            const float N2 = 0.01001634121365712f;
            const float c1a = 0.130526192220052f;
            const float c1b = 0.99144486137381f;
            const float c2a = 0.38268343236509f;
            const float c2b = 0.923879532511287f;
            const float c3a = 0.608761429008721f;
            const float c3b = 0.793353340291235f;
            Vector2[] gradientVectors = {
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
            for (int i = 0; i < gradientVectors.Length; i++)
            {
                gradientVectors[i] = gradientVectors[i] / N2;
            }
            for (int i = 0; i < PERMUTATION_SIZE; i++)
            {
                gradients[i] = gradientVectors[i % gradientVectors.Length];
            }
        }

        private readonly short[] permutation;
        private readonly Vector2[] gradientPermutation;

        public OpenSimplexGradientField2(long seed)
        {
            permutation = new short[PERMUTATION_SIZE];
            gradientPermutation = new Vector2[PERMUTATION_SIZE];
            short[] source = new short[PERMUTATION_SIZE];
            for (short i = 0; i < source.Length; i++)
            {
                source[i] = i;
            }

            for (int i = PERMUTATION_SIZE - 1; i >= 0; i--)
            {
                seed = seed * 6364136223846793005L + 1442695040888963407L;
                int r = (int)((seed + 31) % (i + 1));
                if (r < 0)
                {
                    r += (i + 1);
                }

                permutation[i] = source[r];
                gradientPermutation[i] = gradients[permutation[i]];
                source[r] = source[i];
            }
        }

        public Vector2 this[int x, int y] => GetGradient(x, y);

        private Vector2 GetGradient(int x, int y)
        {
            x &= PERMUTATION_MASK;
            y &= PERMUTATION_MASK;
            return gradientPermutation[permutation[x] ^ y];
        }
    }
}
