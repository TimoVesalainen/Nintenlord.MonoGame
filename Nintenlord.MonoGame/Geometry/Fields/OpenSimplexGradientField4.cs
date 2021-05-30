using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    /// <summary>
    /// Adopted from
    /// https://github.com/KdotJPG/OpenSimplex2/blob/3c64be93f7fadf3d67fbe45e4be5595eb83ef427/csharp/OpenSimplex2F.cs
    /// </summary>
    public sealed class OpenSimplexGradientField4 : IVectorField4iTo4v
    {
        private const int PERMUTATION_SIZE = 1 << 11;
        private const int PERMUTATION_MASK = PERMUTATION_SIZE - 1;

        static readonly Vector4[] gradients;

        static OpenSimplexGradientField4()
        {
            const float N4 = 0.009202377986303158f;
            const float c1a = 0.37968289875261624f;
            const float c1b = 0.753341017856078f;

            const float c2a = 0.12128480194602098f;
            const float c2b = 0.4321472685365301f;
            const float c2c = 0.7821684431180708f;

            const float c3a = 0.044802370851755174f;
            const float c3b = 0.508629699630796f;
            const float c3c = 0.8586508742123365f;

            const float c4a = 0.15296486218853164f;
            const float c4b = 0.4004672082940195f;
            const float c4c = 0.5029860367700724f;
            const float c4d = 0.7504883828755602f;

            const float c5a = 0.08164729285680945f;
            const float c5b = 0.4553054119602712f;
            const float c5c = 0.8828161875373585f;

            const float c6a = 0.3239847771997537f;
            const float c6b = 0.6740059517812944f;
            const float c6c = 0.5794684678643381f;

            const float c7a = 0.03381941603233842f;
            const float c7b = 0.9982828964265062f;

            Vector4[] gradientVectors = {
                new Vector4(-c1b,    -c1a,  -c1a,  -c1a),
                new Vector4(-c2c,   -c2b,   -c2b,    c2a),
                new Vector4(-c2c,   -c2b,    c2a,  -c2b),
                new Vector4(-c2c,    c2a,  -c2b,   -c2b),
                new Vector4(-c3c,   -c3b,     c3a,  c3a),
                new Vector4(-c3c,    c3a, -c3b,     c3a),
                new Vector4(-c3c,    c3a,  c3a, -c3b),
                new Vector4(-c7b,   -c7a,  -c7a,  -c7a),
                new Vector4(-c1a,  -c1b,    -c1a,  -c1a),
                new Vector4(-c2b,   -c2c,   -c2b,    c2a),
                new Vector4(-c2b,   -c2c,    c2a,  -c2b),
                new Vector4( c2a,  -c2c,   -c2b,   -c2b),
                new Vector4(-c3b,    -c3c,    c3a,  c3a),
                new Vector4( c3a, -c3c,   -c3b,     c3a),
                new Vector4( c3a, -c3c,    c3a, -c3b),
                new Vector4(-c7a,  -c7b,   -c7a,  -c7a),
                new Vector4(-c1a,  -c1a,  -c1b,    -c1a),
                new Vector4(-c2b,   -c2b,   -c2c,    c2a),
                new Vector4(-c2b,    c2a,  -c2c,   -c2b),
                new Vector4( c2a,  -c2b,   -c2c,   -c2b),
                new Vector4(-c3b,     c3a, -c3c,    c3a),
                new Vector4( c3a, -c3b,    -c3c,    c3a),
                new Vector4( c3a,  c3a, -c3c,   -c3b),
                new Vector4(-c7a,  -c7a,  -c7b,   -c7a),
                new Vector4(-c1a,  -c1a,  -c1a,  -c1b),
                new Vector4(-c2b,   -c2b,    c2a,  -c2c),
                new Vector4(-c2b,    c2a,  -c2b,   -c2c),
                new Vector4( c2a,  -c2b,   -c2b,   -c2c),
                new Vector4(-c3b,     c3a,  c3a, -c3c),
                new Vector4( c3a, -c3b,     c3a, -c3c),
                new Vector4( c3a,  c3a, -c3b,    -c3c),
                new Vector4(-c7a,  -c7a,  -c7a,  -c7b),
                new Vector4(-c6b,   -c6a,   -c6a,    c6c),
                new Vector4(-c4d,   -c4b,    c4a,   c4c),
                new Vector4(-c4d,    c4a,  -c4b,    c4c),
                new Vector4(-c5c,    c5a,   c5a,   c5b),
                new Vector4(-c5b,   -c5a,  -c5a,   c5c),
                new Vector4(-c4c,   -c4a,   c4b,    c4d),
                new Vector4(-c4c,    c4b,   -c4a,   c4d),
                new Vector4(-c6c,    c6a,    c6a,    c6b),
                new Vector4(-c6a,   -c6b,   -c6a,    c6c),
                new Vector4(-c4b,   -c4d,    c4a,   c4c),
                new Vector4( c4a,  -c4d,   -c4b,    c4c),
                new Vector4( c5a,  -c5c,    c5a,   c5b),
                new Vector4(-c5a,  -c5b,   -c5a,   c5c),
                new Vector4(-c4a,  -c4c,    c4b,    c4d),
                new Vector4( c4b,   -c4c,   -c4a,   c4d),
                new Vector4( c6a,   -c6c,    c6a,    c6b),
                new Vector4(-c6a,   -c6a,   -c6b,    c6c),
                new Vector4(-c4b,    c4a,  -c4d,    c4c),
                new Vector4( c4a,  -c4b,   -c4d,    c4c),
                new Vector4( c5a,   c5a,  -c5c,    c5b),
                new Vector4(-c5a,  -c5a,  -c5b,    c5c),
                new Vector4(-c4a,   c4b,   -c4c,    c4d),
                new Vector4( c4b,   -c4a,  -c4c,    c4d),
                new Vector4( c6a,    c6a,   -c6c,    c6b),
                new Vector4(-c6b,   -c6a,    c6c,   -c6a),
                new Vector4(-c4d,   -c4b,    c4c,    c4a),
                new Vector4(-c4d,    c4a,   c4c,   -c4b),
                new Vector4(-c5c,    c5a,   c5b,    c5a),
                new Vector4(-c5b,   -c5a,   c5c,   -c5a),
                new Vector4(-c4c,   -c4a,   c4d,    c4b),
                new Vector4(-c4c,    c4b,    c4d,   -c4a),
                new Vector4(-c6c,    c6a,    c6b,    c6a),
                new Vector4(-c6a,   -c6b,    c6c,   -c6a),
                new Vector4(-c4b,   -c4d,    c4c,    c4a),
                new Vector4( c4a,  -c4d,    c4c,   -c4b),
                new Vector4( c5a,  -c5c,    c5b,    c5a),
                new Vector4(-c5a,  -c5b,    c5c,   -c5a),
                new Vector4(-c4a,  -c4c,    c4d,    c4b),
                new Vector4( c4b,   -c4c,    c4d,   -c4a),
                new Vector4( c6a,   -c6c,    c6b,    c6a),
                new Vector4(-c6a,   -c6a,    c6c,   -c6b),
                new Vector4(-c4b,    c4a,   c4c,   -c4d),
                new Vector4( c4a,  -c4b,    c4c,   -c4d),
                new Vector4( c5a,   c5a,   c5b,   -c5c),
                new Vector4(-c5a,  -c5a,   c5c,   -c5b),
                new Vector4(-c4a,   c4b,    c4d,   -c4c),
                new Vector4( c4b,   -c4a,   c4d,   -c4c),
                new Vector4( c6a,    c6a,    c6b,   -c6c),
                new Vector4(-c6b,    c6c,   -c6a,   -c6a),
                new Vector4(-c4d,    c4c,   -c4b,    c4a),
                new Vector4(-c4d,    c4c,    c4a,  -c4b),
                new Vector4(-c5c,    c5b,    c5a,   c5a),
                new Vector4(-c5b,    c5c,   -c5a,  -c5a),
                new Vector4(-c4c,    c4d,   -c4a,   c4b),
                new Vector4(-c4c,    c4d,    c4b,   -c4a),
                new Vector4(-c6c,    c6b,    c6a,    c6a),
                new Vector4(-c6a,    c6c,   -c6b,   -c6a),
                new Vector4(-c4b,    c4c,   -c4d,    c4a),
                new Vector4( c4a,   c4c,   -c4d,   -c4b),
                new Vector4( c5a,   c5b,   -c5c,    c5a),
                new Vector4(-c5a,   c5c,   -c5b,   -c5a),
                new Vector4(-c4a,   c4d,   -c4c,    c4b),
                new Vector4( c4b,    c4d,   -c4c,   -c4a),
                new Vector4( c6a,    c6b,   -c6c,    c6a),
                new Vector4(-c6a,    c6c,   -c6a,   -c6b),
                new Vector4(-c4b,    c4c,    c4a,  -c4d),
                new Vector4( c4a,   c4c,   -c4b,   -c4d),
                new Vector4( c5a,   c5b,    c5a,  -c5c),
                new Vector4(-c5a,   c5c,   -c5a,  -c5b),
                new Vector4(-c4a,   c4d,    c4b,   -c4c),
                new Vector4( c4b,    c4d,   -c4a,  -c4c),
                new Vector4( c6a,    c6b,    c6a,   -c6c),
                new Vector4( c6c,   -c6b,   -c6a,   -c6a),
                new Vector4( c4c,   -c4d,   -c4b,    c4a),
                new Vector4( c4c,   -c4d,    c4a,  -c4b),
                new Vector4( c5b,   -c5c,    c5a,   c5a),
                new Vector4( c5c,   -c5b,   -c5a,  -c5a),
                new Vector4( c4d,   -c4c,   -c4a,   c4b),
                new Vector4( c4d,   -c4c,    c4b,   -c4a),
                new Vector4( c6b,   -c6c,    c6a,    c6a),
                new Vector4( c6c,   -c6a,   -c6b,   -c6a),
                new Vector4( c4c,   -c4b,   -c4d,    c4a),
                new Vector4( c4c,    c4a,  -c4d,   -c4b),
                new Vector4( c5b,    c5a,  -c5c,    c5a),
                new Vector4( c5c,   -c5a,  -c5b,   -c5a),
                new Vector4( c4d,   -c4a,  -c4c,    c4b),
                new Vector4( c4d,    c4b,   -c4c,   -c4a),
                new Vector4( c6b,    c6a,   -c6c,    c6a),
                new Vector4( c6c,   -c6a,   -c6a,   -c6b),
                new Vector4( c4c,   -c4b,    c4a,  -c4d),
                new Vector4( c4c,    c4a,  -c4b,   -c4d),
                new Vector4( c5b,    c5a,   c5a,  -c5c),
                new Vector4( c5c,   -c5a,  -c5a,  -c5b),
                new Vector4( c4d,   -c4a,   c4b,   -c4c),
                new Vector4( c4d,    c4b,   -c4a,  -c4c),
                new Vector4( c6b,    c6a,    c6a,   -c6c),
                new Vector4( c7a,   c7a,   c7a,   c7b),
                new Vector4(-c3a, -c3a,  c3b,     c3c),
                new Vector4(-c3a,  c3b,    -c3a,  c3c),
                new Vector4(-c2a,   c2b,    c2b,    c2c),
                new Vector4( c3b,    -c3a, -c3a,  c3c),
                new Vector4( c2b,   -c2a,   c2b,    c2c),
                new Vector4( c2b,    c2b,   -c2a,   c2c),
                new Vector4( c1a,   c1a,   c1a,   c1b),
                new Vector4( c7a,   c7a,   c7b,    c7a),
                new Vector4(-c3a,  c3a,  c3c,    c3b),
                new Vector4(-c3a,  c3b,     c3c,   -c3a),
                new Vector4(-c2a,   c2b,    c2c,    c2b),
                new Vector4( c3b,    -c3a,  c3c,   -c3a),
                new Vector4( c2b,   -c2a,   c2c,    c2b),
                new Vector4( c2b,    c2b,    c2c,   -c2a),
                new Vector4( c1a,   c1a,   c1b,     c1a),
                new Vector4( c7a,   c7b,    c7a,   c7a),
                new Vector4(-c3a,  c3c,   -c3a,  c3b),
                new Vector4(-c3a,  c3c,    c3b,    -c3a),
                new Vector4(-c2a,   c2c,    c2b,    c2b),
                new Vector4( c3b,     c3c,   -c3a, -c3a),
                new Vector4( c2b,    c2c,   -c2a,   c2b),
                new Vector4( c2b,    c2c,    c2b,   -c2a),
                new Vector4( c1a,   c1b,     c1a,   c1a),
                new Vector4( c7b,    c7a,   c7a,   c7a),
                new Vector4( c3c,   -c3a, -c3a,  c3b),
                new Vector4( c3c,   -c3a,  c3b,    -c3a),
                new Vector4( c2c,   -c2a,   c2b,    c2b),
                new Vector4( c3c,    c3b,    -c3a, -c3a),
                new Vector4( c2c,    c2b,   -c2a,   c2b),
                new Vector4( c2c,    c2b,    c2b,   -c2a),
                new Vector4( c1b,     c1a,   c1a,   c1a)
            };
            for (int i = 0; i < gradientVectors.Length; i++)
            {
                gradientVectors[i] = gradientVectors[i] / N4;
            }
            gradients = new Vector4[PERMUTATION_SIZE];
            for (int i = 0; i < gradients.Length; i++)
            {
                gradients[i] = gradientVectors[i % gradientVectors.Length];
            }
        }

        private readonly short[] permutation;
        private readonly Vector4[] gradientPermutation;

        public OpenSimplexGradientField4(long seed)
        {
            short[] source = new short[PERMUTATION_SIZE];
            permutation = new short[PERMUTATION_SIZE];
            gradientPermutation = new Vector4[PERMUTATION_SIZE];
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

        public Vector4 this[int x, int y, int z, int w] => GetGradient(x, y, z, w);

        private Vector4 GetGradient(int x, int y, int z, int w)
        {
            x &= PERMUTATION_MASK;
            y &= PERMUTATION_MASK;
            z &= PERMUTATION_MASK;
            w &= PERMUTATION_MASK;
            return gradientPermutation[permutation[permutation[permutation[x] ^ y] ^ z] ^ w];
        }
    }
}
