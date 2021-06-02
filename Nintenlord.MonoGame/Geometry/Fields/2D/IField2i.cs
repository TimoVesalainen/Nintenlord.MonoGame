using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Geometry.Fields._2D;
using Nintenlord.MonoGame.Geometry.Fields._3D;
using Nintenlord.MonoGame.Noise;
using System;

namespace Nintenlord.MonoGame.Geometry.Fields
{
    public interface IField2i<out T>
    {
        T this[int x, int y] { get; }
    }

    public static class Field2iHelpers
    {
        public static IField2i<double> NoiseField(this INoise2D noise, Vector2 scale)
        {
            return new NoiseField2(noise, scale);
        }
        public static IField3i<T> StretchX<T>(this IField2i<T> field)
        {
            return new StretchXField3i<T>(field);
        }
        public static IField3i<T> StretchY<T>(this IField2i<T> field)
        {
            return new StretchYField3i<T>(field);
        }
        public static IField3i<T> StretchZ<T>(this IField2i<T> field)
        {
            return new StretchZField3i<T>(field);
        }
        public static IField2i<TOut> Zip<T1, T2, TOut>(this IField2i<T1> field1, IField2i<T2> field2, Func<T1, T2, TOut> zipper)
        {
            return new ZipField2i<T1, T2, TOut>(field1, field2, zipper);
        }
        public static IField2i<TOut> Select<TIn, TOut>(this IField2i<TIn> field, Func<TIn, TOut> selector)
        {
            return new SelectField2i<TIn, TOut>(field, selector);
        }
        public static IField3i<bool> ToHeightField(this IField2i<int> heightField)
        {
            return new HeightField3(heightField);
        }
        public static IField3i<TOut> ToPillarFieldZ<TIn, TOut>(this IField2i<TIn> heightField,
            Func<TIn, int, TOut> selector)
        {
            return new PillarZField3<TIn, TOut>(heightField, selector);
        }
        public static IField2i<T> EnlargeField<T>(this IField2i<T> heightField,
            int chunkX, int chunkY, int posInChunkX, int posInChunkY, T filler = default)
        {
            return new EnlargeField2<T>(heightField, filler, chunkX, chunkY, posInChunkX, posInChunkY);
        }

        //PillarZField3
    }
}
