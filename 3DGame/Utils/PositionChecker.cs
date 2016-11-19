using Microsoft.Xna.Framework;

namespace _3DGame
{
    public static class PositionChecker
    {
        public static bool CheckPositon(this Vector3 positon)
        {

            if (positon.X > Settings.BorderLeft || positon.X < Settings.BorderRight)
                return false;
            if (positon.Z > Settings.BorderForward || positon.Z < Settings.BorderBackward)
                return false;

            return true;
        }
    }
}