using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Graph;

namespace Nintenlord.MonoGame.Geometry.Cubes
{
    public sealed class HammingCube : IGraph<HammingCode>
    {
        readonly static ConcurrentDictionary<int, HammingCube> values = new ConcurrentDictionary<int, HammingCube>();

        public static HammingCube ForDimension(int dimension)
        {
            return values.GetOrAdd(dimension, d => new HammingCube(d));
        }

        public int Dimensions { get; }

        private HammingCube(int dimensions)
        {
            Dimensions = dimensions;
        }

        public IEnumerable<HammingCode> Nodes => Enumerable.Range(0, 1 << Dimensions).Select(i => new HammingCode(i, Dimensions));

        public IEnumerable<HammingCode> GetNeighbours(HammingCode node)
        {
            return node.GetNeighbors();
        }

        public bool IsEdge(HammingCode from, HammingCode to)
        {
            return from.GetDistance(to) == 1;
        }
    }
}
