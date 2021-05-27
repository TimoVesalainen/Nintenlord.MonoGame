using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Noise
{
    public abstract class AbstractPerlinNoise2D : INoise2D
    {
        protected abstract Vector2 GradientVector(int x, int y);

        protected abstract float Fade(float value);

        public double Noise(Vector2 position)
        {
            var flooredX = Math.Floor(position.X);
            var flooredY = Math.Floor(position.Y);

            int xMin = (int)flooredX;
            int yMin = (int)flooredY;

            Vector2 inCellPosition = position - new Vector2(xMin, yMin);

            xMin = xMin & 0xFF;
            yMin = yMin & 0xFF;

            var gradLeftBottom = GradientVector(xMin, yMin);
            var gradLeftTop = GradientVector(xMin, yMin + 1);
            var gradRightBottom = GradientVector(xMin + 1, yMin);
            var gradRightTop = GradientVector(xMin + 1, yMin + 1);

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
