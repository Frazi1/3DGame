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

        public static void CollisionDetection(Character character, IList<Box> boxes)
        {
            character.CreateBoundingSphere(CharacterBoundingSphereScale);

            for (int i = 0; i < boxes.Count; i++)
            {
                var b = boxes[i];
                b.CreateBoundingSphere(BoxBoundingSphereScale);
                CheckSphereCollision(character, b);
            }

        }

        public static BoundingSphere CreateBoundingSphere(this IGameObject gameObject, float scale)
        {
            gameObject.BoundingSphere =
                gameObject.Model.Meshes[0].BoundingSphere.Transform(gameObject.Transforms[0] * gameObject.World);

            for (int i = 0; i < gameObject.Model.Meshes.Count; i++)
            {
                var currentMeshBoundingSphere = gameObject.Model.Meshes[i].BoundingSphere;
                gameObject.BoundingSphere = BoundingSphere.CreateMerged(gameObject.BoundingSphere,
                    currentMeshBoundingSphere.Transform(gameObject.Transforms[i] * gameObject.World));
            }
            
            return gameObject.BoundingSphere;
        }

        #endregion
    }
}
