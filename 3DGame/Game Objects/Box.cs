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

        private Vector3 position;
        public Matrix WorldMatrix { get; set; }

        public Box(Vector3 position, ContentManager contentManager)
        {
            //this.position = position;
            this.position = Settings.StartingPlayerPosition + Vector3.Up * Settings.YOffset + Vector3.Left * Settings.XOffset + Vector3.Forward * 10;
            Initialize(contentManager);
            //WorldMatrix = Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up);
        }

        public Box(ContentManager contentManager)
        {
            //WorldMatrix = Matrix.Identity;
            position = Vector3.Zero;
            Initialize(contentManager);
        }

        public void Initialize(ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("sinon");
            UpdateWorld();
        }

        public void AddToPosition(Vector3 offset)
        {
            position += offset;
            UpdateWorld();
        }

        private void UpdateWorld()
        {
            WorldMatrix = Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up);
            //BoundingBox = Collider.UpdateBoundingBox(Model, WorldMatrix);
            BoundingBox = Collider.CreateBoundingBox(Model);
        }
    }



}