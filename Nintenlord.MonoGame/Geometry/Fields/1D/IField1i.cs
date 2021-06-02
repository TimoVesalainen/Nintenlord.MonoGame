namespace Nintenlord.MonoGame.Geometry.Fields
{
    public interface IField1i<out T>
    {
        T this[int x] { get; }
    }
    public static class Field1iHelpers
    {
        public static IField3i<T> StretchXY<T>(this IField1i<T> field)
        {
            return new StretchXYField3i<T>(field);
        }
        public static IField3i<T> StretchYZ<T>(this IField1i<T> field)
        {
            return new StretchYZField3i<T>(field);
        }
        public static IField3i<T> StretchXZ<T>(this IField1i<T> field)
        {
            return new StretchXZField3i<T>(field);
        }
    }
}
