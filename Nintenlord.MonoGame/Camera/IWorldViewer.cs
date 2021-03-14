using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Camera
{
    public interface IWorldViewer
    {
        float FarPlane { get; }
        float FieldOfView { get; }
        float NearPlane { get; }
    }

    public static class WorldViewerExtensions
    {
        public static Matrix GetProjectionMatrix(this IWorldViewer viewer, float aspectRatio)
        {
            return Matrix.CreatePerspectiveFieldOfView(
                viewer.FieldOfView,
                aspectRatio,
                viewer.NearPlane,
                viewer.FarPlane);
        }
    }
}