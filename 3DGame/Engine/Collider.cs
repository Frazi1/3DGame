using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static _3DGame.Settings;

namespace _3DGame
{
    public static class Collider
    {
        #region Methods

        public static bool CheckBoundingBoxCollision(ICollidable source, ICollidable obstacle)
        {
            bool res = source.BoundingBox.Intersects(obstacle.BoundingBox);
            if (res)
                GameEvents.OnObjectCollided(source, obstacle);
            return res;

        }

        public static bool CheckSphereCollision(ICollidable source, ICollidable obstacle)
        {
            bool res = source.BoundingSphere.Intersects(obstacle.BoundingSphere);
            if (res)
                GameEvents.OnObjectCollided(source, obstacle);

            return res;
        }

        public static bool CollisionDetection(Character character, IList<Box> boxes)
        {
            bool result = false;
            character.CreateBoundingSphere(0.2f);

            for (int i = 0; i < boxes.Count; i++)
            {
                var b = boxes[i];
                //b.CreateBoundingSphere(BoxBoundingSphereScale);
                b.CreateBoundingSphere(0.08f);

                
                bool res = CheckSphereCollision(character, b);
                if (!result && res)
                    result = true;
            }
            return result;
        }

        public static BoundingSphere CreateBoundingSphere(this IGameObject gameObject, float scale)
        {
            gameObject.BoundingSphere =
                gameObject.Model.Meshes[0].BoundingSphere.Transform(gameObject.Transforms[0] * gameObject.World);

            for (int i = 0; i < gameObject.Model.Meshes.Count; i++)
            {
                var currentMeshBoundingSphere = gameObject.Model.Meshes[i].BoundingSphere;
                gameObject.BoundingSphere = BoundingSphere.CreateMerged(gameObject.BoundingSphere,
                    currentMeshBoundingSphere.Transform(Matrix.CreateScale(scale) * gameObject.Transforms[i] * gameObject.World));
            }
            
            return gameObject.BoundingSphere;
        }
        public static BoundingBox CreateBoundingBox(this IGameObject gameObject)
        {
            var model = gameObject.Model;
            var worldTransform = gameObject.World;

            // Initialize minimum and maximum corners of the bounding box to max and min values
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // Vertex buffer parameters
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition =
                            Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]),
                                worldTransform);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }
            var result = new BoundingBox(min, max);

            gameObject.BoundingBox = result;
            // Create and return bounding box
            return result;
        }

        #endregion
    }
}
