using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields;
using System;

namespace Nintenlord.MonoGame.Noise
{
    public sealed class PerlinNoise2D : INoise2D
    {
        readonly IVectorField2iTo2v gradients;

        public PerlinNoise2D(IVectorField2iTo2v gradients)
        {
            this.gradients = gradients;
        }

        private float Fade(float value)
        {
            return value * value * value * (value * (value * 6 - 15) + 10);
        }

        public double Noise(Vector2 position)
        {
            var flooredX = Math.Floor(position.X);
            var flooredY = Math.Floor(position.Y);

            int xMin = (int)flooredX;
            int yMin = (int)flooredY;

            Vector2 inCellPosition = position - new Vector2(xMin, yMin);

            var gradLeftBottom = gradients[xMin, yMin];
            var gradLeftTop = gradients[xMin, yMin + 1];
            var gradRightBottom = gradients[xMin + 1, yMin];
            var gradRightTop = gradients[xMin + 1, yMin + 1];

            Vector2 cornerOffsetPositionLeftBottom = inCellPosition;
            Vector2 cornerOffsetPositionLeftTop = new Vector2(
                inCellPosition.X,
                1 - inCellPosition.Y);
            Vector2 cornerOffsetPositionRightBottom = new Vector2(
                1 - inCellPosition.X,
                inCellPosition.Y);
            Vector2 cornerOffsetPositionRightTop = new Vector2(
                1 - inCellPosition.X,
                1 - inCellPosition.Y);

            var u = Fade(inCellPosition.X);
            var v = Fade(inCellPosition.Y);

            var leftBottomDot = Vector2.Dot(cornerOffsetPositionLeftBottom, gradLeftBottom);
            var leftTopDot = Vector2.Dot(cornerOffsetPositionLeftTop, gradLeftTop);
            var rightBottomDot = Vector2.Dot(cornerOffsetPositionRightBottom, gradRightBottom);
            var rightTopDot = Vector2.Dot(cornerOffsetPositionRightTop, gradRightTop);

            var top = MathHelper.Lerp(leftTopDot, rightTopDot, u);
            var bottom = MathHelper.Lerp(leftBottomDot, rightBottomDot, u);

            return MathHelper.Lerp(bottom, top, v);
        }
    }
}
