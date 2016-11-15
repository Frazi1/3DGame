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

        public Box CreateBox(Vector3 position)
        {
            Box box = new Box { Position = position };

            box.Initialize(ContentManager);
            return box;
        }

    }
}