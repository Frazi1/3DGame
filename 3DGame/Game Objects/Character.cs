using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    public class Character: ICollidable, IGameObject
    {
        public Model Model { get; set; }

        private Vector3 direction;
        private Matrix worldMatrix = Matrix.Identity;

        public void Initialize(ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("sinon");
            direction = Vector3.Forward;
            Vector3 startingPos = Settings.StartingPlayerPosition + Vector3.Up * Settings.YOffset + Vector3.Left * Settings.XOffset;
            worldMatrix = Matrix.CreateWorld(startingPos, Vector3.Forward, Vector3.Up);
            //BoundingBox = Collider.UpdateBoundingBox(Model, WorldMatrix);
            BoundingBox = Collider.CreateBoundingBox(Model);
        }

        public void Update(GameTime gameTime)
        {
            //angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateCharacterPosition();
        }

        private void UpdateCharacterPosition()
        {
            float amount = 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                worldMatrix *= Matrix.CreateTranslation(new Vector3(0, 0, -0.1f) * direction);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                worldMatrix *= Matrix.CreateTranslation(new Vector3(0, 0, 0.1f) * direction);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //worldMatrix *= Matrix.CreateTranslation(Vector3.Cross(direction, new Vector3(-0.1f, 0, 0)));
                worldMatrix *= Matrix.CreateTranslation(Vector3.Cross(Vector3.Up, direction) * new Vector3(-0.1f, 0, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //worldMatrix *= Matrix.CreateTranslation(Vector3.Cross(direction, new Vector3(0.1f, 0, 0)));
                worldMatrix *= Matrix.CreateTranslation(Vector3.Cross(Vector3.Up, direction) * new Vector3(0.1f, 0, 0));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Vector3.Transform(direction, Matrix.CreateRotationX(0.01f));
                worldMatrix *= Matrix.CreateRotationY(0.01f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Vector3.Transform(direction, Matrix.CreateRotationX(0.01f));
            }
            //BoundingBox = Collider.UpdateBoundingBox(Model, worldMatrix);
            //BoundingBox = Collider.CreateBoundingBox(Model);
            Collider.MoveBoundingBox(BoundingBox,direction,amount);


        }

        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect basicEffect in mesh.Effects)
                {
                    basicEffect.EnableDefaultLighting();
                    basicEffect.PreferPerPixelLighting = true;

                    //basicEffect.World = GetWorldMatrix();
                    basicEffect.World = WorldMatrix * Matrix.CreateScale(0.1f, 0.1f, 0.1f);
                    basicEffect.View = camera.ViewMatrix;
                    basicEffect.Projection = camera.ProjectionMatrix;
                }
                mesh.Draw();
            }

        }



        public BoundingBox BoundingBox { get; set; }

        public Matrix WorldMatrix
        {
            get { return worldMatrix; }
            set { worldMatrix = value; }
        }

        public Vector3 Direction => direction;
    }
}
