using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public static class Drawer
    {
        public static void DrawModel(Model model, Matrix modelTransform, Matrix[] absoluteBoneTransforms, Camera camera)
        {
            if (model != null)
            {
                //Draw the model, a model can have multiple meshes, so loop
                foreach (ModelMesh mesh in model.Meshes)
                {
                    //This is where the mesh orientation is set
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World =
                            absoluteBoneTransforms[mesh.ParentBone.Index] *
                            modelTransform;
                        effect.Projection = camera.ProjectionMatrix;
                        effect.View = camera.ViewMatrix;
                    }

                    //Draw the mesh, will use the effects set above.
                    mesh.Draw();
                }
            }
        }

        public static void DrawModel(this IGameObject gameObject, Camera camera)
        {
            //var model = gameObject.Model;
            //var absoluteBoneTransforms = gameObject.Transforms;
            //var modelTransform = gameObject.World;

            if (gameObject.Model!= null)
            {
                //Draw the model, a model can have multiple meshes, so loop
                foreach (ModelMesh mesh in gameObject.Model.Meshes)
                {
                    //This is where the mesh orientation is set
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = gameObject.Transforms[mesh.ParentBone.Index] * gameObject.World;
                        effect.Projection = camera.ProjectionMatrix;
                        effect.View = camera.ViewMatrix;
                    }

                    //Draw the mesh, will use the effects set above.
                    mesh.Draw();
                }
            }
        }

        public static Matrix[] SetupEffectDefaults(Model model)
        {
            Matrix[] absoluteTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(absoluteTransforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                }
            }
            return absoluteTransforms;
        }
    }
}