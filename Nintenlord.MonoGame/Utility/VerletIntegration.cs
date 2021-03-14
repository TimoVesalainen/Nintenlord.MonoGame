using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Utility
{
    public struct VerletIntegration
    {
        private Vector3 currentPosition;
        private Vector3 previousPosition;

        public VerletIntegration(Vector3 startPosition)
        {
            previousPosition = currentPosition = startPosition;
        }

        public Vector3 PreviousPosition
        {
            get => previousPosition;
            set => previousPosition = value;
        }

        public Vector3 CurrentPosition
        {
            get => currentPosition;
            set => currentPosition = value;
        }

        public Vector3 Velocity(float timeStep)
        {
            return (currentPosition - previousPosition) / timeStep;
        }

        public void Move(float timeStep, Vector3 acceleration)
        {
            Vector3 newPosition = 2 * currentPosition - previousPosition + acceleration * timeStep * timeStep;
            previousPosition = currentPosition;
            currentPosition = newPosition;
        }
    }
}