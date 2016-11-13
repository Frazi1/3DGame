using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public static class Collider
    {
        public static bool CheckCollision(ICollidable source, ICollidable obstacle)
        {
            bool res = source.BoundingBox.Intersects(obstacle.BoundingBox);
            if(res)
                GameEvents.OnObjectCollided(source,obstacle);
            return res;

        }

        //public static BoundingBox CreateBoundingBox(Vector3 min, Vector3 max)
        //{
        //    List<Vector3> points = new List<Vector3> {min, max};
        //    return BoundingBox.CreateFromPoints(points);
        //}
        //public static BoundingBox CreateBoundingBox(List<Vector3> points)
        //{
        //    return BoundingBox.CreateFromPoints(points);
        //}
        //public static BoundingBox CreateBoundingBox(Model model)
        //{
        //    return BoundingBox.CreateFromPoints(GetBoundingBoxMinMaxPoints(model));
        //}

        //public static List<Vector3> GetBoundingBoxMinMaxPoints(Model model)
        //{
        //    List<Vector3> points = new List<Vector3>();

        //    float minX = model.Bones[0].ModelTransform.Right.X;
        //    float maxX = model.Bones[0].ModelTransform.Right.X;

        //    float minY = model.Bones[0].ModelTransform.Up.Y;
        //    float maxY = model.Bones[0].ModelTransform.Up.Y;

        //    float minZ = model.Bones[0].ModelTransform.Forward.Z;
        //    float maxZ = model.Bones[0].ModelTransform.Forward.Z;

        //    foreach (ModelMesh modelMesh in model.Meshes)
        //    {

        //        float newMinZ = model.Bones.Min(i => i.ModelTransform.Forward.Z);
        //        float newMaxZ = model.Bones.Max(i => i.ModelTransform.Forward.Z);
        //        float newMinX = model.Bones.Min(i => i.ModelTransform.Right.X);
        //        float newMaxX = model.Bones.Max(i => i.ModelTransform.Right.X);
        //        float newMinY = model.Bones.Min(i => i.ModelTransform.Up.Y);
        //        float newMaxY = model.Bones.Max(i => i.ModelTransform.Up.Y);

        //        minX = SelectMin(minX, newMinX);
        //        maxX = SelectMax(maxX, newMaxX);

        //        minZ = SelectMin(minZ, newMinZ);
        //        maxZ = SelectMax(maxZ, newMaxZ);

        //        minY = SelectMin(minY, newMinY);
        //        maxY = SelectMax(maxY, newMaxY);
        //    }
        //    points.Add(new Vector3(minX, minY, minZ));
        //    points.Add(new Vector3(maxX, maxY, maxZ));
        //    return points;
        //}

        //public static List<Vector3> GetBoundingBoxPoints(Model model)
        //{
        //    List<Vector3> points = new List<Vector3>();

        //    model.Meshes[0].Effects[0]

        //    return points;
        //}
        public static BoundingBox UpdateBoundingBox(Model model, Matrix worldTransform)
        {
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
                    int vertexBufferSize = meshPart.NumVertices*vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize/sizeof (float)];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize/sizeof (float); i += vertexStride/sizeof (float))
                    {
                        Vector3 transformedPosition =
                            Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]),
                                worldTransform);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }

            // Create and return bounding box
            return new BoundingBox(min, max);
        }

        private static float SelectMin(float a, float b) => a < b ? a : b;

        private static float SelectMax(float a, float b) => a > b ? a : b;

    }
}
