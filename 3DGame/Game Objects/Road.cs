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
    public class Road
    {
        public Model Model { get; set; }
        public Matrix World = Matrix.Identity;
        public Vector3 Position;

        public Road(Vector3 position)
        {
            Position = position;
            
        }

        public void Initialize(ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("roadV2");
            World = Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up);
        }

        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect basicEffect in mesh.Effects)
                {
                    basicEffect.EnableDefaultLighting();
                    basicEffect.PreferPerPixelLighting = true;

                    basicEffect.World = World;
                    basicEffect.View = camera.ViewMatrix;
                    basicEffect.Projection = camera.ProjectionMatrix;
                }
                mesh.Draw();
            }

        }
    }
}
