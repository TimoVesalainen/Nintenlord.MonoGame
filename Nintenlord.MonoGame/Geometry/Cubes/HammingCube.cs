using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Graph;

namespace Nintenlord.MonoGame.Geometry.Cubes
{
    public sealed class HammingCube : IGraph<HammingCode>
    {
        public int Dimensions { get; }

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
