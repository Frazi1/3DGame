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

        public void Initialize(ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("sinon");
        }


        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect basicEffect in mesh.Effects)
                {
                    basicEffect.EnableDefaultLighting();
                    basicEffect.PreferPerPixelLighting = true;

                    basicEffect.World = camera.WorldMatrix;
                    basicEffect.View = camera.ViewMatrix;
                    basicEffect.Projection = camera.ProjectionMatrix * Matrix.CreateScale(0.2f, 0.2f, 0.2f);
                }
                mesh.Draw();
            }

        }
    }
}
