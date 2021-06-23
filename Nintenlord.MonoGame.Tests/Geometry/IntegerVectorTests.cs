using Nintenlord.MonoGame.Geometry.Vectors;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.MonoGame.Tests.Geometry
{
    class IntegerVectorTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(11)]
        public void CubeSizeTest(int radius)
        {
            var calculatedAmount = (2 * radius - 1) * (2 * radius - 1) * (2 * radius - 1);

            var cubeVectorAmount = IntegerVectorHelpers.GetCube(
                new IntegerVector3(-radius + 1), new IntegerVector3(radius - 1)).Count();

            var cubeLayerSumAmount = Enumerable.Range(1, radius)
                .Select(radius => IntegerVectorHelpers.GetHollowCubeAmount(radius))
                .Sum();

            Assert.AreEqual(calculatedAmount, cubeVectorAmount);
            Assert.AreEqual(calculatedAmount, cubeLayerSumAmount);
        }
    }
}
