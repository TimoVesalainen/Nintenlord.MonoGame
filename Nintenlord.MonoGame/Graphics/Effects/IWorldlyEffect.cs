using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nintenlord.MonoGame.Graphics.Effects
{
    public interface IWorldlyEffect
    {
        Matrix World { get; set; }

        EffectTechnique Technique { get; }
    }
}
