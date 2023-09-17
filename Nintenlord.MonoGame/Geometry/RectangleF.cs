using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.Geometry
{
    public struct RectangleF : IEquatable<RectangleF>
    {
        public float Height;
        public float Width;
        public float X, Y;

        static RectangleF()
        {
            Empty = new RectangleF(0, 0, 0, 0);
        }

        public RectangleF(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Left => X;

        public float Top => Y;

        public float Right => X + Width;

        public float Bottom => Y + Height;

        public bool IsEmpty => Width == 0 && Height == 0;

        public Vector2 Position
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public static RectangleF Empty { get; }

        public bool Equals(RectangleF other)
        {
            return other.X == X && other.Y == Y && other.Width == Width && other.Height == Height;
        }

        public Vector2 GetCenter()
        {
            return Position + Size / 2;
        }

        public bool IntersectsWith(RectangleF rect)
        {
            if (Right < rect.X)
            {
                return false;
            }

            if (rect.Right < X)
            {
                return false;
            }

            if (Bottom < rect.Y)
            {
                return false;
            }

            if (rect.Bottom < Y)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y + " Width: " + Width + " Height: " + Height;
        }

        public bool Contains(Vector2 value)
        {
            return value.X >= X && value.X < X + Width &&
                   value.Y >= Y && value.Y < Y + Height;
        }

        public bool Contains(RectangleF value)
        {
            return value.X >= X && value.X + value.Width <= X + Width &&
                   value.Y >= Y && value.Y + value.Height <= Y + Height;
        }

        public override bool Equals(object obj)
        {
            if (obj is RectangleF)
            {
                return Equals((RectangleF)obj);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }


        public static void And(ref RectangleF a, ref RectangleF b, out RectangleF result)
        {
            if (!Intersect(a, b))
            {
                result = Empty;
            }
            else
            {
                result = new RectangleF();
                if (a.X > b.X)
                {
                    result.X = a.X;
                    result.Width = b.Right - a.X;
                }
                else
                {
                    result.X = b.X;
                    result.Width = a.Right - b.X;
                }

                if (a.Y > b.Y)
                {
                    result.Y = a.Y;
                    result.Height = b.Bottom - a.Y;
                }
                else
                {
                    result.Y = b.Y;
                    result.Height = a.Bottom - b.Y;
                }
            }
        }

        public static void Or(ref RectangleF a, ref RectangleF b, out RectangleF result)
        {
            bool aIsEmpty = a.IsEmpty;
            bool bIsEmpty = b.IsEmpty;

            if (aIsEmpty && bIsEmpty)
            {
                result = Empty;
            }
            else if (aIsEmpty)
            {
                result = b;
            }
            else if (bIsEmpty)
            {
                result = a;
            }
            else
            {

                float bottom = Math.Max(a.Bottom, b.Bottom);
                float right = Math.Max(a.Right, b.Right);

                result = new RectangleF
                {
                    X = Math.Min(a.X, b.X),
                    Y = Math.Min(a.Y, b.Y)
                };
                result.Width = right - result.X;
                result.Height = bottom - result.Y;
            }
        }

        public static bool Intersect(RectangleF rect1, RectangleF rect2)
        {
            if (rect1.Right < rect2.X)
            {
                return false;
            }

            if (rect1.Right < rect2.X)
            {
                return false;
            }

            if (rect1.Bottom < rect2.Y)
            {
                return false;
            }

            if (rect1.Bottom < rect2.Y)
            {
                return false;
            }

            return true;
        }

        public static Rectangle ToRectangle(RectangleF a)
        {
            return new Rectangle((int)a.X, (int)a.Y, (int)a.Width, (int)a.Height);
        }


        public static bool operator ==(RectangleF a, RectangleF b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !a.Equals(b);
        }

        public static RectangleF operator &(RectangleF a, RectangleF b)
        {
            And(ref a, ref b, out RectangleF result);
            return result;
        }

        public static RectangleF operator |(RectangleF a, RectangleF b)
        {
            Or(ref a, ref b, out RectangleF result);
            return result;
        }

        public static explicit operator Rectangle(RectangleF a)
        {
            return ToRectangle(a);
        }

        public static RectangleF And(IEnumerable<RectangleF> list)
        {
            IEnumerator<RectangleF> enuma = list.GetEnumerator();
            if (enuma.MoveNext())
            {
                RectangleF result = enuma.Current;
                while (enuma.MoveNext())
                {
                    result &= enuma.Current;
                }
                return result;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static RectangleF Or(IEnumerable<RectangleF> list)
        {
            return list.Aggregate(Empty, (current, item) => current | item);
        }

        public static IEnumerable<RectangleF> GetQuadrants(RectangleF area)
        {
            var width = area.Width / 2;
            var height = area.Height / 2;

            yield return new RectangleF(area.X + width, area.Y + height, width, height);
            yield return new RectangleF(area.X, area.Y + height, width, height);
            yield return new RectangleF(area.X, area.Y, width, height);
            yield return new RectangleF(area.X + width, area.Y, width, height);
        }

        public RectangleQuadrant GetQuadrant(Vector2 position)
        {
            var isLeft = position.X < X + Width / 2;
            var isTop = position.Y < Y + Height / 2;

            return RectangleQuadrantHelpers.GetQuadrant(isLeft, isTop);
        }
    }
}