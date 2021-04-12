using Microsoft.Xna.Framework;
using Nintenlord.Collections;
using Nintenlord.Collections.Comparers;
using Nintenlord.Utility;
using System;
using System.Linq;

namespace Nintenlord.MonoGame.Geometry
{
    public static class RectangleExtensions
    {
        public static void Clamp(this Rectangle i, ref Vector2 toClamp)
        {
            if (toClamp.X < i.X)
            {
                toClamp.X = i.X;
            }
            else if (toClamp.X >= i.Right - 1)
            {
                toClamp.X = i.Right - 1;
            }

            if (toClamp.Y < i.Y)
            {
                toClamp.Y = i.Y;
            }
            else if (toClamp.Y >= i.Bottom - 1)
            {
                toClamp.Y = i.Bottom - 1;
            }
        }


        public static int Area(this Rectangle i)
        {
            return i.Width * i.Height;
        }

        public static T[] ForEachPoint<T>(this Rectangle r, Func<int, int, T> func)
        {
            var result = new T[r.Area()];
            for (int j = 0; j < r.Height; j++)
            {
                for (int i = 0; i < r.Width; i++)
                {
                    result[i + j * r.Width] = func(i, j);
                }
            }
            return result;
        }

        public static void ForEachPoint<T>(this Rectangle r, T[,] grid, Func<int, int, T> func)
        {
            int widht = grid.GetLength(0);

            for (int j = r.Top; j < r.Bottom; j++)
            {
                for (int i = r.Left; i < r.Top; i++)
                {
                    grid[i, j] = func(i, j);
                }
            }
        }

        public static Rectangle GetRectangle(Point corner1, Point corner2)
        {
            return new Rectangle(
                Math.Min(corner1.X, corner2.X),
                Math.Min(corner1.Y, corner2.Y),
                Math.Abs(corner1.X - corner2.X) + 1,
                Math.Abs(corner1.Y - corner2.Y) + 1
                );
        }


        public static Rectangle[] GetRects(bool[] bools, int width, int amount)
        {
            return GetRects(bools, width, new Rectangle(0, 0, width, bools.Length / width), amount);
        }

        public static Rectangle[] GetRects(bool[] bools, int width, Rectangle area, int amount)
        {
            IPriorityQueue<int, Rectangle> seeds
                = new SkipListPriorityQueue<int, Rectangle>(
                    100,
                    new FunctionComparer<int>(x => -x)
                    );
            Minimize(ref area, bools, width);
            seeds.Enqueue(area, Math.Max(area.Width, area.Height));

            while (seeds.Count < amount)
            {
                Rectangle toDivide = seeds.Dequeue();
                if (toDivide.Area() == 0)
                {
                    break;
                }

                var rect1 = new Rectangle(toDivide.X, toDivide.Y, toDivide.Width, toDivide.Height);
                var rect2 = new Rectangle(toDivide.X, toDivide.Y, toDivide.Width, toDivide.Height);
                if (toDivide.Width > toDivide.Height)
                {
                    int w1 = toDivide.Width / 2;
                    int w2 = toDivide.Width - w1;
                    rect1.Width = w1;
                    rect2.Width = w2;
                    rect2.X += w1;
                }
                else
                {
                    int h1 = toDivide.Height / 2;
                    int h2 = toDivide.Height - h1;
                    rect1.Height = h1;
                    rect2.Height = h2;
                    rect2.Y += h1;
                }
                Minimize(ref rect1, bools, width);
                Minimize(ref rect2, bools, width);
                seeds.Enqueue(rect1, Math.Max(rect1.Width, rect1.Height));
                seeds.Enqueue(rect2, Math.Max(rect2.Width, rect2.Height));
            }

            seeds.Clear();
            return seeds.Select(item => item.Value).ToArray();
        }

        private static void Minimize(ref Rectangle rect, bool[] bools, int width)
        {
            while (NoTruesOnLeft(ref rect, bools, width) && rect.Area() > 0)
            {
                rect.X++;
                rect.Width--;
            }
            while (NoTruesOnRight(ref rect, bools, width) && rect.Area() > 0)
            {
                rect.Width--;
            }
            while (NoTruesOnBottom(ref rect, bools, width) && rect.Area() > 0)
            {
                rect.Height--;
            }
            while (NoTruesOnTop(ref rect, bools, width) && rect.Area() > 0)
            {
                rect.Y++;
                rect.Height--;
            }
            if (rect.Width == 0)
            {
                rect.Height = 0;
            }
            if (rect.Height == 0)
            {
                rect.Width = 0;
            }
        }

        private static bool NoTruesOnTop(ref Rectangle rect, bool[] bools, int width)
        {
            for (int i = rect.Left; i < rect.Right; i++)
            {
                if (bools[rect.Top * width + i])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool NoTruesOnBottom(ref Rectangle rect, bool[] bools, int width)
        {
            for (int i = rect.Left; i < rect.Right; i++)
            {
                if (bools[(rect.Bottom - 1) * width + i])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool NoTruesOnRight(ref Rectangle rect, bool[] bools, int width)
        {
            for (int i = rect.Top; i < rect.Bottom; i++)
            {
                if (bools[i * width + rect.Right - 1])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool NoTruesOnLeft(ref Rectangle rect, bool[] bools, int width)
        {
            for (int i = rect.Top; i < rect.Bottom; i++)
            {
                if (bools[i * width + rect.Left])
                {
                    return false;
                }
            }
            return true;
        }
    }
}