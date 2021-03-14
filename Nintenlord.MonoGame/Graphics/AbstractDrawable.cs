using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Graphics
{
    public abstract class AbstractDrawable : IDrawable
    {
        private bool visible;
        private int drawOrder;

        public AbstractDrawable(bool visible = true, int drawOrder = 0)
        {
            this.visible = visible;
            this.drawOrder = drawOrder;
        }

        public bool Visible
        {
            get => visible;
            set
            {
                if (value != visible)
                {
                    visible = value;
                    OnVisibleChanged();
                }
            }
        }
        public int DrawOrder
        {
            get => drawOrder;
            set
            {
                if (value != drawOrder)
                {
                    drawOrder = value;
                    OnDrawOrderChanged();
                }
            }
        }

        private void OnVisibleChanged()
        {
            if (VisibleChanged != null)
            {
                VisibleChanged(this, EventArgs.Empty);
            }
        }
        private void OnDrawOrderChanged()
        {
            if (DrawOrderChanged != null)
            {
                DrawOrderChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;

        public abstract void Draw(GameTime gameTime);
    }
}
