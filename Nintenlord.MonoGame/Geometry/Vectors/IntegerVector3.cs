using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace Nintenlord.MonoGame.Geometry.Vectors
{
    [DataContract]
    [DebuggerDisplay("[{X},{Y},{Z}]")]
    public readonly struct IntegerVector3 : IEquatable<IntegerVector3>
    {
        private static readonly IntegerVector3 zero = new IntegerVector3(0, 0, 0);
        private static readonly IntegerVector3 one = new IntegerVector3(1, 1, 1);
        private static readonly IntegerVector3 unitX = new IntegerVector3(1, 0, 0);
        private static readonly IntegerVector3 unitY = new IntegerVector3(0, 1, 0);
        private static readonly IntegerVector3 unitZ = new IntegerVector3(0, 0, 1);
        private static readonly IntegerVector3 min = new IntegerVector3(int.MinValue, int.MinValue, int.MinValue);
        private static readonly IntegerVector3 max = new IntegerVector3(int.MaxValue, int.MaxValue, int.MaxValue);

        [DataMember]
        public readonly int X;
        [DataMember]
        public readonly int Y;
        [DataMember]
        public readonly int Z;

        public IntegerVector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public IntegerVector3(int r)
        {
            X = r;
            Y = r;
            Z = r;
        }

        public IntegerVector3(Point xy, int z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        public IntegerVector3 ProjectX => new IntegerVector3(X, 0, 0);

        public IntegerVector3 ProjectY => new IntegerVector3(0, Y, 0);

        public IntegerVector3 ProjectZ => new IntegerVector3(0, 0, Z);


        public static IntegerVector3 Zero => zero;

        public static IntegerVector3 One => one;

        public static IntegerVector3 UnitX => unitX;

        public static IntegerVector3 UnitY => unitY;

        public static IntegerVector3 UnitZ => unitZ;

        public static IntegerVector3 MinVector => min;

        public static IntegerVector3 MaxVector => max;

        public bool IsInSquare(in IntegerVector3 start, in IntegerVector3 size)
        {
            IntegerVector3 end = start + size;
            return IsBetween(start, end);
        }
        public bool IsBetween(in IntegerVector3 start, in IntegerVector3 end)
        {
            return
                X >= start.X && X < end.X &&
                Y >= start.Y && Y < end.Y &&
                Z >= start.Z && Z < end.Z;
        }

        public IntegerVector3 With(int? x = null, int? y = null, int? z = null)
        {
            return new IntegerVector3(x ?? X, y ?? Y, z ?? Z);
        }

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        #region Static operators

        public static IntegerVector3 Sign(in IntegerVector3 a)
        {
            var x = Math.Sign(a.X);
            var y = Math.Sign(a.Y);
            var z = Math.Sign(a.Z);
            return new IntegerVector3(x, y, z);
        }

        public static IntegerVector3 Abs(in IntegerVector3 a)
        {
            var x = Math.Abs(a.X);
            var y = Math.Abs(a.Y);
            var z = Math.Abs(a.Z);
            return new IntegerVector3(x, y, z);
        }

        public static IntegerVector3 Max(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(
                Math.Max(a.X, b.X),
                Math.Max(a.Y, b.Y),
                Math.Max(a.Z, b.Z));
        }

        public static IntegerVector3 Min(IntegerVector3 a, IntegerVector3 b)
        {
            return new IntegerVector3(
                Math.Min(a.X, b.X),
                Math.Min(a.Y, b.Y),
                Math.Min(a.Z, b.Z));
        }

        public static int Volume(IntegerVector3 a)
        {
            return a.X * a.Y * a.Z;
        }

        public static int DotProduct(IntegerVector3 a, IntegerVector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static int LengthSquared(IntegerVector3 a)
        {
            return DotProduct(a, a);
        }

        public static int DistanceSquared(IntegerVector3 a, IntegerVector3 b)
        {
            return LengthSquared(a - b);
        }

        public static double Distance(IntegerVector3 a, IntegerVector3 b)
        {
            return Length(a - b);
        }

        public static double Length(IntegerVector3 a)
        {
            return Math.Sqrt(DotProduct(a, a));
        }

        public static IntegerVector3 Plus(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static IEnumerable<IntegerVector3> Plus(IntegerVector3 a, IEnumerable<IntegerVector3> u)
        {
            return u.Select(x => a + x);
        }

        public static IEnumerable<IntegerVector3> Minus(IntegerVector3 a, IEnumerable<IntegerVector3> u)
        {
            return u.Select(x => a - x);
        }

        public static IntegerVector3 Subtract(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static IntegerVector3 Multiply(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static IntegerVector3 Multiply(in IntegerVector3 a, int b)
        {
            return new IntegerVector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static IntegerVector3 Divide(in IntegerVector3 a, int b)
        {
            return new IntegerVector3(a.X / b, a.Y / b, a.Z / b);
        }

        public static IntegerVector3 Divide(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static IntegerVector3 Modulus(in IntegerVector3 a, int b)
        {
            return new IntegerVector3(a.X % b, a.Y % b, a.Z % b);
        }

        public static IntegerVector3 Modulus(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        }

        public static IntegerVector3 And(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X & b.X, a.Y & b.Y, a.Z & b.Z);
        }

        public static IntegerVector3 And(in IntegerVector3 a, int b)
        {
            return new IntegerVector3(a.X & b, a.Y & b, a.Z & b);
        }

        public static IntegerVector3 Or(in IntegerVector3 a, int b)
        {
            return new IntegerVector3(a.X | b, a.Y | b, a.Z | b);
        }

        public static IntegerVector3 Or(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X | b.X, a.Y | b.Y, a.Z | b.Z);
        }

        public static IntegerVector3 Xor(in IntegerVector3 a, int b)
        {
            return new IntegerVector3(a.X ^ b, a.Y ^ b, a.Z ^ b);
        }

        public static IntegerVector3 Xor(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X ^ b.X, a.Y ^ b.Y, a.Z ^ b.Z);
        }

        public static IntegerVector3 Minus(in IntegerVector3 a)
        {
            return new IntegerVector3(-a.X, -a.Y, -a.Z);
        }

        public static IntegerVector3 LeftShift(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X << b.X, a.Y << b.Y, a.Z << b.Z);
        }

        public static IntegerVector3 LeftShift(in IntegerVector3 a, int b)
        {
            return new IntegerVector3(a.X << b, a.Y << b, a.Z << b);
        }

        public static IntegerVector3 RightShift(in IntegerVector3 a, in IntegerVector3 b)
        {
            return new IntegerVector3(a.X >> b.X, a.Y >> b.Y, a.Z >> b.Z);
        }

        public static IntegerVector3 RightShift(in IntegerVector3 a, int b)
        {
            return new IntegerVector3(a.X >> b, a.Y >> b, a.Z >> b);
        }

        public static IntegerVector3 Floor(Vector3 v)
        {
            return new IntegerVector3((int)Math.Floor(v.X), (int)Math.Floor(v.Y), (int)Math.Floor(v.Z));
        }

        public static IntegerVector3 Ceiling(Vector3 v)
        {
            return new IntegerVector3((int)Math.Ceiling(v.X), (int)Math.Ceiling(v.Y), (int)Math.Ceiling(v.Z));
        }

        public static IntegerVector3 Round(Vector3 v)
        {
            return new IntegerVector3((int)Math.Round(v.X), (int)Math.Round(v.Y), (int)Math.Round(v.Z));
        }

        public static IntegerVector3 Round(Vector3 v, MidpointRounding r)
        {
            return new IntegerVector3((int)Math.Round(v.X, r), (int)Math.Round(v.Y, r), (int)Math.Round(v.Z, r));
        }

        #endregion

        #region Operators

        public static IntegerVector3 operator +(in IntegerVector3 a, in IntegerVector3 b)
        {
            return Plus(a, b);
        }

        public static IEnumerable<IntegerVector3> operator +(in IntegerVector3 a, IEnumerable<IntegerVector3> b)
        {
            return Plus(a, b);
        }

        public static IEnumerable<IntegerVector3> operator +(IEnumerable<IntegerVector3> a, in IntegerVector3 b)
        {
            return Plus(b, a);
        }

        public static IntegerVector3 operator -(in IntegerVector3 a, in IntegerVector3 b)
        {
            return Subtract(a, b);
        }

        public static IEnumerable<IntegerVector3> operator -(in IntegerVector3 a, IEnumerable<IntegerVector3> b)
        {
            return Minus(a, b);
        }

        public static IEnumerable<IntegerVector3> operator -(IEnumerable<IntegerVector3> a, in IntegerVector3 b)
        {
            return Minus(b, a);
        }

        public static IntegerVector3 operator -(in IntegerVector3 a)
        {
            return Minus(a);
        }

        public static IntegerVector3 operator *(in IntegerVector3 a, in IntegerVector3 b)
        {
            return Multiply(a, b);
        }

        public static IntegerVector3 operator *(int a, in IntegerVector3 b)
        {
            return Multiply(b, a);
        }

        public static IntegerVector3 operator *(in IntegerVector3 a, int b)
        {
            return Multiply(a, b);
        }

        public static IntegerVector3 operator /(in IntegerVector3 a, in IntegerVector3 b)
        {
            return Divide(a, b);
        }

        public static IntegerVector3 operator /(in IntegerVector3 a, int b)
        {
            return Divide(a, b);
        }

        public static IntegerVector3 operator %(in IntegerVector3 a, in IntegerVector3 b)
        {
            return Modulus(a, b);
        }

        public static IntegerVector3 operator %(in IntegerVector3 a, int b)
        {
            return Modulus(a, b);
        }

        public static IntegerVector3 operator &(in IntegerVector3 a, in IntegerVector3 b)
        {
            return And(a, b);
        }

        public static IntegerVector3 operator &(in IntegerVector3 a, int b)
        {
            return And(a, b);
        }

        public static IntegerVector3 operator |(in IntegerVector3 a, in IntegerVector3 b)
        {
            return Or(a, b);
        }

        public static IntegerVector3 operator |(in IntegerVector3 a, int b)
        {
            return Or(a, b);
        }

        public static IntegerVector3 operator ^(in IntegerVector3 a, in IntegerVector3 b)
        {
            return Xor(a, b);
        }

        public static IntegerVector3 operator ^(in IntegerVector3 a, int b)
        {
            return Xor(a, b);
        }

        public static IntegerVector3 operator >>(in IntegerVector3 a, int b)
        {
            return RightShift(a, b);
        }

        public static IntegerVector3 operator <<(in IntegerVector3 a, int b)
        {
            return LeftShift(a, b);
        }

        public static bool operator ==(in IntegerVector3 a, in IntegerVector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(in IntegerVector3 a, in IntegerVector3 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        #endregion

        public static explicit operator Vector3(in IntegerVector3 a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static explicit operator IntegerVector3(Vector3 a)
        {
            return new IntegerVector3((int)a.X, (int)a.Y, (int)a.Z);
        }

        public static explicit operator Point(in IntegerVector3 a)
        {
            return new Point(a.X, a.Y);
        }

        public static implicit operator IntegerVector3(Point a)
        {
            return new IntegerVector3(a.X, a.Y, 0);
        }

        public static implicit operator IntegerVector3((int x, int y, int z) tuple)
        {
            return new IntegerVector3(tuple.x, tuple.y, tuple.z);
        }

        public static implicit operator IntegerVector3(Tuple<int, int, int> tuple)
        {
            return new IntegerVector3(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static implicit operator (int x, int y, int z)(in IntegerVector3 vector)
        {
            return (vector.X, vector.Y, vector.Z);
        }

        public static implicit operator Tuple<int, int, int>(in IntegerVector3 vector)
        {
            return Tuple.Create(vector.X, vector.Y, vector.Z);
        }

        public override int GetHashCode()
        {
            const int mask = 0x3FF;
            const int shift = 10;

            return X & mask | (Y & mask) << shift | (Z & mask) << shift * 2;
        }

        public override string ToString()
        {
            return string.Format("{{{0},{1},{2}}}", X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            return obj is IntegerVector3 vector && this == vector;
        }

        #region IEquatable<IntegerVector3> Members

        public bool Equals(IntegerVector3 other)
        {
            return this == other;
        }

        #endregion
    }
}
