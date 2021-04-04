﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Nintenlord.MonoGame.Geometry;
using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Tests.Geometry
{
    class CollisionDetectionTests
    {
        [TestCase(0)]
        [TestCase(0.1f)]
        [TestCase(-0.1f)]
        [TestCase(0.9f)]
        [TestCase(1.0f)]
        [TestCase(1.1f)]
        public void ClipLineNegative(float movement)
        {
            float toMove;

            if (movement > 0)
            {
                toMove = 0;
            }
            else
            {
                toMove = movement;
            }

            Assert.AreEqual(toMove, CollisionDetection.ClipLine(0, 1, 1, 2, movement), 0.001);
        }

        [TestCase(0)]
        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(-0.9f)]
        [TestCase(-1.0f)]
        [TestCase(-1.1f)]
        public void ClipLinePositive(float movement)
        {
            float toMove;

            if (movement < 0)
            {
                toMove = 0;
            }
            else
            {
                toMove = movement;
            }

            Assert.AreEqual(toMove, CollisionDetection.ClipLine(1, 2, 0, 1, movement), 0.001);
        }

        [TestCase(0)]
        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(-0.9f)]
        [TestCase(-1.0f)]
        [TestCase(-1.1f)]
        [TestCase(0.9f)]
        [TestCase(1.0f)]
        [TestCase(1.1f)]
        public void ClipLineSame(float transposition)
        {
            float toMove;

            if (transposition <= -1 || transposition >= 1)
            {
                toMove = 0;
            }
            else if(transposition >= 0)
            {
                toMove = 1 - transposition;
            }
            else
            {
                toMove = -1 - transposition;
            }

            Assert.AreEqual(toMove, CollisionDetection.ClipLine(0 + transposition, 1 + transposition, 0, 1, 0), 0.001);
        }

        [TestCase(-0.1f)]
        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedUnitX(float amount)
        {
            TestMovementTo(amount, Vector3.UnitX);
        }

        [TestCase(-0.1f)]
        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedUnitY(float amount)
        {
            TestMovementTo(amount, Vector3.UnitY);
        }

        [TestCase(-0.1f)]
        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedUnitZ(float amount)
        {
            TestMovementTo(amount, Vector3.UnitZ);
        }

        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedXY(float amount)
        {
            TestMovementTo(amount, Vector3.UnitX + Vector3.UnitY);
        }

        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedXZ(float amount)
        {
            TestMovementTo(amount, Vector3.UnitX + Vector3.UnitZ);
        }

        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedYZ(float amount)
        {
            TestMovementTo(amount, Vector3.UnitY + Vector3.UnitZ);
        }

        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedXYZ(float amount)
        {
            TestMovementTo(amount, Vector3.One);
        }

        [TestCase(-0.1f)]
        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedMinusUnitX(float amount)
        {
            TestMovementTo(amount, -Vector3.UnitX);
        }

        [TestCase(-0.1f)]
        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedMinusUnitY(float amount)
        {
            TestMovementTo(amount, -Vector3.UnitY);
        }

        [TestCase(-0.1f)]
        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedMinusUnitZ(float amount)
        {
            TestMovementTo(amount, -Vector3.UnitZ);
        }

        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedMinusXY(float amount)
        {
            TestMovementTo(amount, -Vector3.UnitX - Vector3.UnitY);
        }

        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedMinusXZ(float amount)
        {
            TestMovementTo(amount, -Vector3.UnitX - Vector3.UnitZ);
        }

        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedMinusYZ(float amount)
        {
            TestMovementTo(amount,- Vector3.UnitY - Vector3.UnitZ);
        }

        [TestCase(-0.1f)]
        [TestCase(0.1f)]
        [TestCase(0.5f)]
        [TestCase(0.9f)]
        [TestCase(1f)]
        public void ClipMovementMovedMinusXYZ(float amount)
        {
            TestMovementTo(amount, -Vector3.One);
        }

        private static void TestMovementTo(float amount, Vector3 direction)
        {
            bool shouldChange = amount > 0;

            Vector3 vector = direction * amount;
            BoundingBox original = new BoundingBox(Vector3.Zero, Vector3.One);

            BoundingBox other = new BoundingBox(direction, Vector3.One + direction);

            Vector3 movement = vector;
            var changed = CollisionDetection.ClipMovementAgainstSolid(original, other, ref movement);

            Assert.AreEqual(shouldChange, changed);
            if (shouldChange)
            {
                Assert.AreNotEqual(vector, movement);
                Assert.AreEqual(Vector3.Zero, movement);
            }
            else
            {
                Assert.AreEqual(vector, movement);
                if (amount != 0)
                {
                    Assert.AreNotEqual(Vector3.Zero, movement);
                }
            }
        }

        [Test]
        public void CollisionMiddle()
        {
            BoundingBox original = new BoundingBox(Vector3.Zero, Vector3.One);
            BoundingBox other = new BoundingBox(Vector3.One * 0.5f, Vector3.One * 1.5f);
            Vector3 movement = Vector3.Zero;

            var changed = CollisionDetection.ClipMovementAgainstSolid(original, other, ref movement);

            Assert.False(changed);
        }
    }
}
