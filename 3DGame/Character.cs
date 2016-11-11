using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public class Character
    {
        public Model Model { get; set; }
        float angle;



        public void Initialize(ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("sinon");
        }

        public void Update(GameTime gameTime)
        {
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect basicEffect in mesh.Effects)
                {
                    basicEffect.EnableDefaultLighting();
                    basicEffect.PreferPerPixelLighting = true;

                    basicEffect.World = GetWorldMatrix();
                    basicEffect.View = camera.ViewMatrix;
                    basicEffect.Projection = camera.ProjectionMatrix * Matrix.CreateScale(0.1f, 0.1f, 0.1f);
                }
                mesh.Draw();
            }

        }

        private Matrix GetWorldMatrix()
        {
            const float circleRadius = 8;
            const float heightOffGround = 3;

            // this matrix moves the model "out" from the origin
            Matrix translationMatrix = Matrix.CreateTranslation(
                circleRadius, 0, heightOffGround);

            // this matrix rotates everything around the origin
            Matrix rotationMatrix = Matrix.CreateRotationY(angle);

            // We combine the two to have the model move in a circle:
            Matrix combined = translationMatrix * rotationMatrix;

            return combined;
        }
    }
}
