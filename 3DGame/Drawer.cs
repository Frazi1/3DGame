using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public static class Drawer
    {
        public static void DrawModel(Model model, Camera camera, Matrix world)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = world;
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }
                mesh.Draw();
            }
        }

        public static void DrawModel(Model model, Camera camera, Matrix world, float scale)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    //effect.PreferPerPixelLighting = true;
                    effect.World = world * Matrix.CreateScale(new Vector3(scale, scale, scale));
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix ;
                }
                mesh.Draw();
            }
        }
    }
}