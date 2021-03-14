using Microsoft.Xna.Framework.Graphics;

namespace Nintenlord.MonoGame.Graphics.Effects
{
    public interface ISkyboxEffect
    {
        TextureCube SkyboxTexture { get; set; }

        EffectTechnique Technique { get; }
    }
}
