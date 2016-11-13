using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public class Box: ICollidable, IGameObject
    {
        public Model Model { get; set; }
        public BoundingBox BoundingBox { get; set; }

        public Matrix WorldMatrix { get; }

        public Box(Vector3 position, ContentManager contentManager)
        {
            WorldMatrix = Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up);
            Initialize(contentManager);
        }
        public Box(ContentManager contentManager)
        {
            WorldMatrix = Matrix.Identity;
            Initialize(contentManager);
        }

        public void Initialize(ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("sinon");
            BoundingBox = Collider.UpdateBoundingBox(Model, WorldMatrix);

        }
        
    }

    

}