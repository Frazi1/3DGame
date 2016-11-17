using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public static class BoundingBoxRenderer
    {
        #region Fields

        private static VertexPositionColor[] verts = new VertexPositionColor[8];
        private static short[] indices = new short[]
        {
        0, 1,
        1, 2,
        2, 3,
        3, 0,
        0, 4,
        1, 5,
        2, 6,
        3, 7,
        4, 5,
        5, 6,
        6, 7,
        7, 4,
        };

        private static BasicEffect effect;
        private static VertexDeclaration vertDecl;

        #endregion


        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            if (effect == null)
            {
                effect = new BasicEffect(graphicsDevice)
                {
                    VertexColorEnabled = true,
                    LightingEnabled = false
                };
            }
        }


        /// <summary>
        /// Renders the bounding box for debugging purposes.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="camera"></param>
        /// <param name="color">The color to use drawing the lines of the box.</param>
        public static void DrawBoundingBox(this IGameObject gameObject, Camera camera, Color color)
        {
            var box = gameObject.BoundingBox;
            
            Vector3[] corners = box.GetCorners();
            for (int i = 0; i < 8; i++)
            {
                verts[i].Position = corners[i];
                verts[i].Color = color;
            }

            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                effect.GraphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.LineList,
                    verts,
                    0,
                    8,
                    indices,
                    0,
                    indices.Length / 2);
            }
        }
    }
}