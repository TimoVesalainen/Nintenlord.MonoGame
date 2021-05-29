using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public sealed class OpenSimplexGradientField3 : IVectorField3iTo3v
    {
        private const int PERMUTATION_SIZE = 1 << 11;
        private const int PERMUTATION_MASK = PERMUTATION_SIZE - 1;

        static readonly Vector3[] gradients;

        static OpenSimplexGradientField3()
        {
            gradients = new Vector3[PERMUTATION_SIZE];
            const float c1 = 2.22474487139f;
            const float c2a = 3.0862664687972017f;
            const float c2b = 1.1721513422464978f;
            const float N3 = 0.030485933181293584f;
            Vector3[] gradientVectors = {
                new Vector3(-c1, -c1, -1.0f),
                new Vector3(-c1, -c1, 1.0f),
                new Vector3(-c2a, -c2b, 0.0f),
                new Vector3(-c2b, -c2a, 0.0f),
                new Vector3(-c1, -1.0f, -c1),
                new Vector3(-c1, 1.0f, -c1),
                new Vector3(-c2b, 0.0f, -c2a),
                new Vector3(-c2a, 0.0f, -c2b),
                new Vector3(-c1, -1.0f, c1),
                new Vector3(-c1, 1.0f, c1),
                new Vector3(-c2a, 0.0f, c2b),
                new Vector3(-c2b, 0.0f, c2a),
                new Vector3(-c1, c1, -1.0f),
                new Vector3(-c1, c1, 1.0f),
                new Vector3(-c2b, c2a, 0.0f),
                new Vector3(-c2a, c2b, 0.0f),
                new Vector3(-1.0f, -c1, -c1),
                new Vector3( 1.0f, -c1, -c1),
                new Vector3( 0.0f, -c2a, -c2b),
                new Vector3( 0.0f, -c2b, -c2a),
                new Vector3(-1.0f, -c1, c1),
                new Vector3( 1.0f, -c1, c1),
                new Vector3( 0.0f, -c2b, c2a),
                new Vector3( 0.0f, -c2a, c2b),
                new Vector3(-1.0f, c1, -c1),
                new Vector3( 1.0f, c1, -c1),
                new Vector3( 0.0f, c2b, -c2a),
                new Vector3( 0.0f, c2a, -c2b),
                new Vector3(-1.0f, c1, c1),
                new Vector3( 1.0f, c1, c1),
                new Vector3( 0.0f, c2a, c2b),
                new Vector3( 0.0f, c2b, c2a),
                new Vector3( c1, -c1, -1.0f),
                new Vector3( c1, -c1, 1.0f),
                new Vector3( c2b, -c2a, 0.0f),
                new Vector3( c2a, -c2b, 0.0f),
                new Vector3( c1, -1.0f, -c1),
                new Vector3( c1, 1.0f, -c1),
                new Vector3( c2a, 0.0f, -c2b),
                new Vector3( c2b, 0.0f, -c2a),
                new Vector3( c1, -1.0f, c1),
                new Vector3( c1, 1.0f, c1),
                new Vector3( c2b, 0.0f, c2a),
                new Vector3( c2a, 0.0f, c2b),
                new Vector3( c1, c1, -1.0f),
                new Vector3( c1, c1, 1.0f),
                new Vector3( c2a, c2b, 0.0f),
                new Vector3( c2b, c2a, 0.0f)
            };
            for (int i = 0; i < gradientVectors.Length; i++)
            {
                gradientVectors[i] = gradientVectors[i] / N3;
            }
            for (int i = 0; i < gradients.Length; i++)
            {
                gradients[i] = gradientVectors[i % gradientVectors.Length];
            }
        }

        private readonly short[] permutation;
        private readonly Vector3[] gradientPermutation;

        public OpenSimplexGradientField3(long seed)
        {
            permutation = new short[PERMUTATION_SIZE];
            gradientPermutation = new Vector3[PERMUTATION_SIZE];
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
                    r += (i + 1);
                permutation[i] = source[r];
                gradientPermutation[i] = gradients[permutation[i]];
                source[r] = source[i];
            }
        }

        public Vector3 this[int x, int y, int z] => GetGradient(x, y, z);

        private Vector3 GetGradient(int x, int y, int z)
        {
            x &= PERMUTATION_MASK;
            y &= PERMUTATION_MASK;
            z &= PERMUTATION_MASK;

            return gradientPermutation[permutation[permutation[x] ^ y] ^ z];
        }
    }
}
