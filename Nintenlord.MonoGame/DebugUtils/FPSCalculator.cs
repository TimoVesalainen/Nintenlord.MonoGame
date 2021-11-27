using Microsoft.Xna.Framework;
using Nintenlord.Collections.Lists;
using System.Linq;

namespace Nintenlord.MonoGame.Utility
{
    public sealed class FPSCalculator : GameComponent
    {
        private readonly LinkedArrayList<double> drawSeconds;
        private readonly int framesToUse;

        public FPSCalculator(Game game, int framesToUse)
            : base(game)
        {
            this.framesToUse = framesToUse;
            drawSeconds = new LinkedArrayList<double>(framesToUse);
        }

        public double FPS => 1 / drawSeconds.Average();

        public override void Update(GameTime gameTime)
        {
            if (drawSeconds.Count > framesToUse)
            {
                drawSeconds.RemoveFirst();
            }
            drawSeconds.AddLast(gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}