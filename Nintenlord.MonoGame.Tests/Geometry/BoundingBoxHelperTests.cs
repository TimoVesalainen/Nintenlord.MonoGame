using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry;
using Nintenlord.MonoGame.Geometry.Vectors;
using NUnit.Framework;
using System.Linq;

namespace Nintenlord.MonoGame.Tests.Geometry
{
    class BoundingBoxHelperTests
    {
        [TestCase(0, 0, 0)]
        [TestCase(-1, 0, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, 0, -1)]
        [TestCase(-1, -1, -1)]
        [TestCase(1, 1, 1)]
        public void TestUnitCubeBottom(int x, int y, int z)
        {
            BoundingBox box = new BoundingBox(new Vector3(x,y,z), new Vector3(x + 1, y + 1, z + 1));

            var bottomBox = box.GetBottomUnitBoxes().ToArray();

            Assert.AreEqual(new[] { new IntegerVector3(x,y,z) }, bottomBox);
        }

        [TestCase(0, 0, 0)]
        [TestCase(-1, 0, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, 0, -1)]
        [TestCase(-1, -1, -1)]
        [TestCase(1, 1, 1)]
        public void TestEightCubeBottom(int x, int y, int z)
        {
            BoundingBox box = new BoundingBox(new Vector3(x, y, z), new Vector3(x + 2, y + 2, z + 2));

            var bottomBox = box.GetBottomUnitBoxes().ToArray();

            Assert.AreEqual(new[] { 
                new IntegerVector3(x, y, z), 
                new IntegerVector3(x+1, y, z),
                new IntegerVector3(x, y+1, z),
                new IntegerVector3(x+1, y+1, z) }, bottomBox);
        }

        [TestCase(0, 0, 0)]
        [TestCase(-1, 0, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, 0, -1)]
        [TestCase(-1, -1, -1)]
        [TestCase(1, 1, 1)]
        public void TestUnitCubeHalfCoordBottom(int x, int y, int z)
        {
            BoundingBox box = new BoundingBox(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), new Vector3(x + 1.5f, y + 1.5f, z + 1.5f));

            var bottomBox = box.GetBottomUnitBoxes().ToArray();

            Assert.AreEqual(new[] {
                new IntegerVector3(x, y, z),
                new IntegerVector3(x+1, y, z),
                new IntegerVector3(x, y+1, z),
                new IntegerVector3(x+1, y+1, z) }, bottomBox);
        }
    }
}
