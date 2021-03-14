using Microsoft.Xna.Framework;
using Nintenlord.MonoGame.Map.ThreeDimensions;

namespace Nintenlord.MonoGame.Camera
{
    public interface ICamera : IPositionable3D
    {
        Vector3 ViewDirection { get; }
        Vector3 UpDirection { get; }
    }

    public static class CameraHelpers
    {
        public static Matrix GetViewMatrix(this ICamera camera)
        {
            Vector3 cameraPosition = camera.Position;
            Vector3 cameraUpVector = camera.UpDirection;
            Vector3 cameraTarget = camera.ViewDirection + cameraPosition;

            return Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
        }

        public static Ray GetLookingDirection(this ICamera camera)
        {
            return new Ray(camera.Position, camera.ViewDirection);
        }
    }
}