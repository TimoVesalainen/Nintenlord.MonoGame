using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nintenlord.MonoGame.Geometry.Cubes
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct HammingCode : IEquatable<HammingCode>
    {
        readonly int buffer;
        readonly int dimensions;

        public HammingCode(int buffer, int dimensions)
        {
            if (dimensions <= 0)
            {
                throw new ArgumentException(nameof(dimensions));
            }
            if (buffer >= (1 << dimensions) || buffer < 0)
            {
                throw new ArgumentException(nameof(buffer));
            }

            this.buffer = buffer;
            this.dimensions = dimensions;
        }

        public IEnumerable<HammingCode> GetNeighbors()
        {
            for (int i = 0; i < dimensions; i++)
            {
                yield return new HammingCode(buffer | 1 << i, dimensions);
            }
        }

        public HammingCode GetOpposite()
        {
            var mask = (1 << dimensions) - 1;
            return new HammingCode(buffer ^ mask, dimensions);
        }

        public int GetDistance(in HammingCode other)
        {
            if (dimensions != other.dimensions)
            {
                throw new ArgumentException("Code from wrong diemnsion", nameof(other));
            }

            return buffer.HammingDistance(other.buffer);
        }

        public bool Equals(HammingCode other)
        {
            return other.dimensions == dimensions &&
                other.buffer == buffer;
        }

        public override bool Equals(object obj)
        {
            return obj is HammingCode code && Equals(code);
        }

        public override int GetHashCode()
        {
            return buffer;
        }

        private string InnerToString() => Convert.ToString(buffer, 2).PadLeft(dimensions, '0');

        public override string ToString()
        {
            return $"[Code {InnerToString()}]";
        }

        private string GetDebuggerDisplay()
        {
            return InnerToString();
        }
    }
}
