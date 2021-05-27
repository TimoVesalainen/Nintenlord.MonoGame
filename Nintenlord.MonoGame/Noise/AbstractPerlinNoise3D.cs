﻿using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Noise
{
    public abstract class AbstractPerlinNoise3D : INoise3D
    {
        protected abstract Vector3 GradientVector(int x, int y, int z);

        protected abstract float Fade(float value);

        public double Noise(Vector3 position)
        {
            var flooredX = Math.Floor(position.X);
            var flooredY = Math.Floor(position.Y);
            var flooredZ = Math.Floor(position.Z);

            int xMin = (int)flooredX;
            int yMin = (int)flooredY;
            int zMin = (int)flooredZ;

            var inCellPosition = position - new Vector3(xMin, yMin, zMin);

            var gradLeftBottomNear = GradientVector(xMin, yMin, zMin);
            var gradLeftTopNear = GradientVector(xMin, yMin + 1, zMin);
            var gradRightBottomNear = GradientVector(xMin + 1, yMin, zMin);
            var gradRightTopNear = GradientVector(xMin + 1, yMin + 1, zMin);
            var gradLeftBottomFar = GradientVector(xMin, yMin, zMin + 1);
            var gradLeftTopFar = GradientVector(xMin, yMin + 1, zMin + 1);
            var gradRightBottomFar = GradientVector(xMin + 1, yMin, zMin + 1);
            var gradRightTopFar = GradientVector(xMin + 1, yMin + 1, zMin + 1);

            Vector3 cornerOffsetPositionLeftBottomNear = inCellPosition;
            Vector3 cornerOffsetPositionLeftTopNear = new Vector3(
                inCellPosition.X,
                1 - inCellPosition.Y,
                inCellPosition.Z);
            Vector3 cornerOffsetPositionRightBottomNear = new Vector3(
                1 - inCellPosition.X,
                inCellPosition.Y,
                inCellPosition.Z);
            Vector3 cornerOffsetPositionRightTopNear = new Vector3(
                1 - inCellPosition.X,
                1 - inCellPosition.Y,
                inCellPosition.Z);

            Vector3 cornerOffsetPositionLeftBottomFar = new Vector3(
                inCellPosition.X,
                inCellPosition.Y,
                1 - inCellPosition.Z);
            Vector3 cornerOffsetPositionLeftTopFar = new Vector3(
                inCellPosition.X,
                1 - inCellPosition.Y,
                inCellPosition.Z);
            Vector3 cornerOffsetPositionRightBottomFar = new Vector3(
                1 - inCellPosition.X,
                inCellPosition.Y,
                1 - inCellPosition.Z);
            Vector3 cornerOffsetPositionRightTopFar = new Vector3(
                1 - inCellPosition.X,
                1 - inCellPosition.Y,
                1 - inCellPosition.Z);

            var u = Fade(inCellPosition.X);
            var v = Fade(inCellPosition.Y);
            var w = Fade(inCellPosition.Z);

            var leftBottomNearDot = Vector3.Dot(cornerOffsetPositionLeftBottomNear, gradLeftBottomNear);
            var leftTopNearDot = Vector3.Dot(cornerOffsetPositionLeftTopNear, gradLeftTopNear);
            var rightBottomNearDot = Vector3.Dot(cornerOffsetPositionRightBottomNear, gradRightBottomNear);
            var rightTopNearDot = Vector3.Dot(cornerOffsetPositionRightTopNear, gradRightTopNear);

            var leftBottomFarDot = Vector3.Dot(cornerOffsetPositionLeftBottomFar, gradLeftBottomFar);
            var leftTopFarDot = Vector3.Dot(cornerOffsetPositionLeftTopFar, gradLeftTopFar);
            var rightBottomFarDot = Vector3.Dot(cornerOffsetPositionRightBottomFar, gradRightBottomFar);
            var rightTopFarDot = Vector3.Dot(cornerOffsetPositionRightTopFar, gradRightTopFar);

            var topNear = MathHelper.Lerp(leftTopNearDot, rightTopNearDot, u);
            var bottomNear = MathHelper.Lerp(leftBottomNearDot, rightBottomNearDot, u);
            var topFar = MathHelper.Lerp(leftTopFarDot, rightTopFarDot, u);
            var bottomFar = MathHelper.Lerp(leftBottomFarDot, rightBottomFarDot, u);

            var near = MathHelper.Lerp(bottomNear, topNear, v);
            var far = MathHelper.Lerp(bottomFar, topFar, v);

            return MathHelper.Lerp(near, far, w);
        }
    }
}
