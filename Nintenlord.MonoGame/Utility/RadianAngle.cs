using Microsoft.Xna.Framework;
using System;

namespace Nintenlord.MonoGame.Utility
{
    public readonly struct RadianAngle
    {
        public static readonly RadianAngle Zero = new RadianAngle(0);
        public static readonly RadianAngle Pi = new RadianAngle(MathHelper.Pi);
        public static readonly RadianAngle PiOver2 = new RadianAngle(MathHelper.PiOver2);
        public static readonly RadianAngle PiOver4 = new RadianAngle(MathHelper.PiOver4);
        private readonly float angle;

        public RadianAngle(float angle)
        {
            this.angle = WrapAngle(angle);
        }

        public float Angle => angle;

        public static RadianAngle operator +(RadianAngle angle1, float angle2)
        {
            return new RadianAngle(angle1.angle + angle2);
        }

        public static RadianAngle operator +(RadianAngle angle1, RadianAngle angle2)
        {
            return new RadianAngle(angle1.angle + angle2.angle);
        }

        public static RadianAngle operator -(RadianAngle angle1, float angle2)
        {
            return new RadianAngle(angle1.angle - angle2);
        }

        public static RadianAngle operator -(RadianAngle angle1, RadianAngle angle2)
        {
            return new RadianAngle(angle1.angle - angle2.angle);
        }

        public Matrix CreateRotationX()
        {
            return Matrix.CreateRotationX(angle);
        }

        public Matrix CreateRotationY()
        {
            return Matrix.CreateRotationY(angle);
        }

        public Matrix CreateRotationZ()
        {
            return Matrix.CreateRotationZ(angle);
        }

        public Matrix CreateRotationFromAxis(Vector3 axis)
        {
            return Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(axis, angle));
        }

        public static float WrapAngle(float angle)
        {
            //From http://code.google.com/p/monoxna/source/browse/trunk/src/Microsoft.Xna.Framework/MathHelper.cs?r=348
            angle = (float)Math.IEEERemainder(angle, 6.2831854820251465); //2xPi precission is double
            if (angle <= -3.141593f)
            {
                angle += 6.283185f;
                return angle;
            }
            if (angle > 3.141593f)
            {
                angle -= 6.283185f;
            }
            return angle;

        }

        public static float ClampAngle(float angle)
        {
            if (angle <= -MathHelper.PiOver2 + 0.0001f)
            {
                return -MathHelper.PiOver2 + 0.0001f;
            }
            else if (angle >= MathHelper.PiOver2 - 0.0001f)
            {
                return MathHelper.PiOver2 - 0.0001f;
            }
            return angle;
        }
    }
}