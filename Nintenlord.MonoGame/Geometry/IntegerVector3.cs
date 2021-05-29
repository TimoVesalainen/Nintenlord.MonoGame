﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nintenlord.MonoGame.Geometry
{
    [DataContract]
    public struct IntegerVector3 : IEquatable<IntegerVector3>
    {
        private static readonly IntegerVector3 zero = new IntegerVector3(0, 0, 0);
        private static readonly IntegerVector3 one = new IntegerVector3(1, 1, 1);
        private static readonly IntegerVector3 unitX = new IntegerVector3(1, 0, 0);
        private static readonly IntegerVector3 unitY = new IntegerVector3(0, 1, 0);
        private static readonly IntegerVector3 unitZ = new IntegerVector3(0, 0, 1);
        private static readonly IntegerVector3 min = new IntegerVector3(int.MinValue, int.MinValue, int.MinValue);
        private static readonly IntegerVector3 max = new IntegerVector3(int.MaxValue, int.MaxValue, int.MaxValue);

        [DataMember]
        public int X;
        [DataMember]
        public int Y;
        [DataMember]
        public int Z;

        public IntegerVector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
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

        public bool IsInSquare(IntegerVector3 start, IntegerVector3 size)
        {
            IntegerVector3 end = start + size;
            return
                X >= start.X && X < end.X &&
                Y >= start.Y && Y < end.Y &&
                Z >= start.Z && Z < end.Z;
        }

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        #region Static operators

        public static IntegerVector3 Sign(IntegerVector3 a)
        {
            Sign(ref a);
            return a;
        }

        public static void Sign(ref IntegerVector3 a)
        {
            a.X = Math.Sign(a.X);
            a.Y = Math.Sign(a.Y);
            a.Z = Math.Sign(a.Z);
        }

        public static IntegerVector3 Abs(IntegerVector3 a)
        {
            Abs(ref a);
            return a;
        }

        public static void Abs(ref IntegerVector3 a)
        {
            a.X = Math.Abs(a.X);
            a.Y = Math.Abs(a.Y);
            a.Z = Math.Abs(a.Z);
        }

        public static IntegerVector3 Max(IntegerVector3 a, IntegerVector3 b)
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

        public static void Plus(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static IEnumerable<IntegerVector3> Plus(IntegerVector3 a, IEnumerable<IntegerVector3> u)
        {
            return u.Select(x => a + x);
        }

        public static IEnumerable<IntegerVector3> Minus(IntegerVector3 a, IEnumerable<IntegerVector3> u)
        {
            return u.Select(x => a - x);
        }

        public static void Subtract(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static void Multiply(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static void Multiply(ref IntegerVector3 a, int b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static void Multiply(int a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a * b.X, a * b.Y, a * b.Z);
        }

        public static void Divide(ref IntegerVector3 a, int b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X / b, a.Y / b, a.Z / b);
        }

        public static void Divide(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static void Modulus(ref IntegerVector3 a, int b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X % b, a.Y % b, a.Z % b);
        }

        public static void Modulus(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
        }

        public static void And(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X & b.X, a.Y & b.Y, a.Z & b.Z);
        }

        public static void And(ref IntegerVector3 a, int b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X & b, a.Y & b, a.Z & b);
        }

        public static void Or(ref IntegerVector3 a, int b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X | b, a.Y | b, a.Z | b);
        }

        public static void Or(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X | b.X, a.Y | b.Y, a.Z | b.Z);
        }

        public static void Xor(ref IntegerVector3 a, int b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X ^ b, a.Y ^ b, a.Z ^ b);
        }

        public static void Xor(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X ^ b.X, a.Y ^ b.Y, a.Z ^ b.Z);
        }

        public static void Minus(ref IntegerVector3 a, out IntegerVector3 result)
        {
            result = new IntegerVector3(-a.X, -a.Y, -a.Z);
        }

        public static void LeftShift(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X << b.X, a.Y << b.Y, a.Z << b.Z);
        }

        public static void LeftShift(ref IntegerVector3 a, int b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X << b, a.Y << b, a.Z << b);
        }

        public static void RightShift(ref IntegerVector3 a, ref IntegerVector3 b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X >> b.X, a.Y >> b.Y, a.Z >> b.Z);
        }

        public static void RightShift(ref IntegerVector3 a, int b, out IntegerVector3 result)
        {
            result = new IntegerVector3(a.X >> b, a.Y >> b, a.Z >> b);
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

        public static IntegerVector3 operator +(IntegerVector3 a, IntegerVector3 b)
        {
            Plus(ref a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IEnumerable<IntegerVector3> operator +(IntegerVector3 a, IEnumerable<IntegerVector3> b)
        {
            return Plus(a, b);
        }

        public static IEnumerable<IntegerVector3> operator +(IEnumerable<IntegerVector3> a, IntegerVector3 b)
        {
            return Plus(b, a);
        }

        public static IntegerVector3 operator -(IntegerVector3 a, IntegerVector3 b)
        {
            Subtract(ref a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IEnumerable<IntegerVector3> operator -(IntegerVector3 a, IEnumerable<IntegerVector3> b)
        {
            return Minus(a, b);
        }

        public static IEnumerable<IntegerVector3> operator -(IEnumerable<IntegerVector3> a, IntegerVector3 b)
        {
            return Minus(b, a);
        }

        public static IntegerVector3 operator -(IntegerVector3 a)
        {
            Minus(ref a, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator *(IntegerVector3 a, IntegerVector3 b)
        {
            Multiply(ref a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator *(int a, IntegerVector3 b)
        {
            Multiply(a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator *(IntegerVector3 a, int b)
        {
            Multiply(ref a, b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator /(IntegerVector3 a, IntegerVector3 b)
        {
            Divide(ref a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator /(IntegerVector3 a, int b)
        {
            Divide(ref a, b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator %(IntegerVector3 a, IntegerVector3 b)
        {
            Modulus(ref a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator %(IntegerVector3 a, int b)
        {
            Modulus(ref a, b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator &(IntegerVector3 a, IntegerVector3 b)
        {
            And(ref a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator &(IntegerVector3 a, int b)
        {
            And(ref a, b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator |(IntegerVector3 a, IntegerVector3 b)
        {
            Or(ref a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator |(IntegerVector3 a, int b)
        {
            Or(ref a, b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator ^(IntegerVector3 a, IntegerVector3 b)
        {
            Xor(ref a, ref b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator ^(IntegerVector3 a, int b)
        {
            Xor(ref a, b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator >>(IntegerVector3 a, int b)
        {
            RightShift(ref a, b, out IntegerVector3 result);
            return result;
        }

        public static IntegerVector3 operator <<(IntegerVector3 a, int b)
        {
            LeftShift(ref a, b, out IntegerVector3 result);
            return result;
        }

        public static bool operator ==(IntegerVector3 a, IntegerVector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(IntegerVector3 a, IntegerVector3 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        #endregion

        public static explicit operator Vector3(IntegerVector3 a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static explicit operator IntegerVector3(Vector3 a)
        {
            return new IntegerVector3((int)a.X, (int)a.Y, (int)a.Z);
        }

        public static explicit operator Point(IntegerVector3 a)
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

        public static implicit operator (int x, int y, int z)(IntegerVector3 vector)
        {
            return (vector.X, vector.Y, vector.Z);
        }

        public static implicit operator Tuple<int, int, int>(IntegerVector3 vector)
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
