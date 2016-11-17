using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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

            Box box = new Box {Position = position};

            box.Initialize(ContentManager);
            return box;
        }

    }
}