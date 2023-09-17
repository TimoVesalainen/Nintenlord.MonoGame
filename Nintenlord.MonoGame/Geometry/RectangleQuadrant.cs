namespace Nintenlord.MonoGame.Geometry
{
    public enum RectangleQuadrant
    {
        TopRight = 1,
        TopLeft = 2,
        BottomLeft = 3,
        BottomRight = 4,
    }

    public static class RectangleQuadrantHelpers
    {
        public static RectangleQuadrant GetQuadrant(bool left, bool top) 
        {
            if (left)
            {
                if (top)
                {
                    return RectangleQuadrant.TopLeft;
                }
                else
                {
                    return RectangleQuadrant.BottomLeft;
                }
            }
            else
            {
                if (top)
                {
                    return RectangleQuadrant.TopRight;
                }
                else
                {
                    return RectangleQuadrant.BottomRight;
                }
            }
        }
    }
}