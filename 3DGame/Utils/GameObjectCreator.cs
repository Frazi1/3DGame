using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _3DGame
{
    public class GameObjectCreator
    {
        public ContentManager ContentManager { get; set; }

        public GameObjectCreator(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public Box CreateBox(/*Vector3 position*/)
        {
            //Box box = new Box { Position = new Vector3(Settings.XOffset,0,0)  };
            Box box = new Box();
            box.Position = new Vector3(10,10,10);

            box.Initialize(ContentManager);
            return box;
        }

    }
}