using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nintenlord.MonoGame.Graphics.Effects
{
    public interface IWorldlyTexturingEffect
    {
        Texture2D CurrentTexture { get; set; }

        Matrix World { get; set; }

        EffectTechnique Technique { get; }
    }
}
