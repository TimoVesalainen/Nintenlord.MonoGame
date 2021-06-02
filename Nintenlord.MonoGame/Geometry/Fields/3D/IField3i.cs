using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields._3D;
using Nintenlord.MonoGame.Noise;
using System;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public interface IField3i<out T>
    {
        T this[int x, int y, int z] { get; }
    }

    public static class Field3iHelpers
    {
        public static IField3i<TOut> Select<TIn, TOut>(this IField3i<TIn> field, Func<TIn, TOut> selector)
        {
            return new SelectField3i<TIn, TOut>(field, selector);
        }

        public static IField3i<(T1, T2)> PairWith<T1, T2>(this IField3i<T1> field1, IField3i<T2> field2)
        {
            return new PairField3i<T1, T2>(field1, field2);
        }

        public static IField3i<TOut> Zip<T1, T2, TOut>(this IField3i<T1> field1, IField3i<T2> field2, Func<T1, T2, TOut> selector)
        {
            return new ZipField3i<T1, T2, TOut>(field1, field2, selector);
        }

        public static IField3i<double> FromNoise(this INoise3D noise, Vector3 scale)
        {
            return new NoiseField3(noise, scale);
        }

        public static IField3i<T> Translate<T>(this IField3i<T> field, int x = 0, int y = 0, int z = 0)
        {
            if (x == 0 && y == 0 && z == 0)
            {
                return field;
            }
            else
            {
                return new TranslateField<T>(field, x, y, z);
            }
        }
    }
}
