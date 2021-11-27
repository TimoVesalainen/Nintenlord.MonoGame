using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Games
{
    public abstract class AbstractUpdateable : IUpdateable
    {
        private int updateOrder;
        private bool enabled;

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (value != enabled)
                {
                    enabled = value;
                    OnEnabledChanged();
                }
            }
        }

        public int UpdateOrder
        {
            get => updateOrder;
            set
            {
                if (value != updateOrder)
                {
                    updateOrder = value;
                    OnUpdateOrderChanged();
                }
            }
        }

        public AbstractUpdateable(bool enabled = true, int updateOrder = 0)
        {
            this.updateOrder = updateOrder;
            this.enabled = enabled;
        }

        private void OnUpdateOrderChanged()
        {
            UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnEnabledChanged()
        {
            EnabledChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public event EventHandler<EventArgs> EnabledChanged;

        public abstract void Update(GameTime gameTime);
    }
}
