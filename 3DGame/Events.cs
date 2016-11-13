using System;

namespace _3DGame
{
    public static class GameEvents
    {
        public static event Action<ICollidable, ICollidable> ObjectCollided;


        public static void OnObjectCollided(ICollidable arg1, ICollidable arg2)
        {
            ObjectCollided?.Invoke(arg1, arg2);
        }
    }
}