using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Games;
using System;

namespace Nintenlord.MonoGame.Utility
{
    public sealed class Timer : AbstractUpdateable
    {
        private readonly TimeSpan timeToWait;
        private TimeSpan currentTime;
        private bool alarmRaised;

        public bool Timing
        {
            get;
            set;
        }

        public Timer(TimeSpan timeToWait)
        {
            this.timeToWait = timeToWait;
            ResetTimer();
        }

        public void ResetTimer()
        {
            alarmRaised = false;
            currentTime = TimeSpan.Zero;
        }

        public event EventHandler TimeDone;

        private void OnTimeDone()
        {
            TimeDone?.Invoke(this, EventArgs.Empty);
        }

        public override void Update(GameTime gameTime)
        {
            if (Timing)
            {
                currentTime += gameTime.ElapsedGameTime;
                if (currentTime >= timeToWait && !alarmRaised)
                {
                    alarmRaised = true;
                    OnTimeDone();
                }
            }
        }
    }
}
