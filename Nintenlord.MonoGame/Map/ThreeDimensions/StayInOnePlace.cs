using Microsoft.Xna.Framework;

namespace Nintenlord.MonoGame.Map.ThreeDimensions
{
    public sealed class StayInOnePlace : IPositionable3D
    {
        private readonly Vector3 position;

        public StayInOnePlace(Vector3 position)
        {
            this.position = position;
        }

        #region IPositionable3D Members

        public Vector3 Position => position;

        #endregion
    }
}