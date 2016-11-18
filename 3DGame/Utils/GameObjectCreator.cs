using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static _3DGame.Settings;

namespace _3DGame
{
    public static class GameObjectCreator
    {
        public static ContentManager ContentManager { get; set; }


        public static void Initialize(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public static Box CreateBox(Vector3 position)
        {
            //Box box = new Box { Position = new Vector3(Settings.XOffset, 0, 0) };

            Box box = new Box
            {
                Position = position
            };

            box.Initialize(ContentManager);
            return box;
        }public static Box CreateBox(Vector3 position, Vector3 target)
        {
            //Box box = new Box { Position = new Vector3(Settings.XOffset, 0, 0) };

            Box box = new Box
            {
                Position = position,
                Target = target
            };

            box.Initialize(ContentManager);
            return box;
        }

        public static Vector3 GetRandomPosition()
        {
            Vector3 boxPosition;
            Random rnd = new Random();

            float x = rnd.Next((int) BoxSpawning_Min_XOffset, (int) BoxSpawning_Max_XOffset);
            float y = rnd.Next((int)BoxSpawning_Min_YOffset, (int)BoxSpawning_Max_YOffset);
            float z = rnd.Next((int) BoxSpawning_Min_ZOffset, (int) BoxSpawning_Max_ZOffset);

            boxPosition = new Vector3(x,y,z);

            return boxPosition;
        }

    }
}