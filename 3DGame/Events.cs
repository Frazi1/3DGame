using System;

namespace _3DGame
{
    public static class GameEvents
    {
        public static event Action<IGameObject, IGameObject> ObjectCollided;
        public static event Action<Character> CharacterDied;
        public static event Action<Character, IGameObject> CharacterHit;
        public static event Action<Box> BoxDestroyed;

        public static void OnObjectCollided(IGameObject arg1, IGameObject arg2)
        {
            ObjectCollided?.Invoke(arg1, arg2);
        }

        public static void OnCharacterDied(Character arg)
        {
            CharacterDied?.Invoke(arg);
        }

        public static void OnBoxDestroyed(Box arg)
        {
            BoxDestroyed?.Invoke(arg);
        }

        public static void OnCharacterHit(Character arg1, IGameObject arg2)
        {
            CharacterHit?.Invoke(arg1,arg2);
        }
    }
}