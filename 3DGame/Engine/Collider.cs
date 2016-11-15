using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static _3DGame.Settings;

namespace _3DGame
{
    public static class Collider
    {

        #region Methods

        public static bool CheckCollision(ICollidable source, ICollidable obstacle)
        {
            bool res = source.BoundingBox.Intersects(obstacle.BoundingBox);
            if (res)
                GameEvents.OnObjectCollided(source, obstacle);
            return res;

        }

        public static bool CheckSphereCollision(ICollidable source, ICollidable obstacle)
        {
            bool res = source.BoundingSphere.Intersects(obstacle.BoundingSphere);
            if(res)
                GameEvents.OnObjectCollided(source,obstacle);
            return res;
        }

        public static void CollisionDetection(Character character, IList<Box> boxes)
        {
            character.CreateBoundingSphere();

            for (int i = 0; i < boxes.Count; i++)
            {
                var b = boxes[i];
                b.CreateBoundingSphere();
                CheckSphereCollision(character, b);
            }

        }

        public static BoundingSphere CreateBoundingSphere(this IGameObject gameObject)
        {
            gameObject.BoundingSphere = new BoundingSphere(gameObject.Position,
                gameObject.Model.Meshes[0].BoundingSphere.Radius * CharacterBoundingSphereScale);

            //gameObject.BoundingSphere = gameObject.Model.Meshes[0].BoundingSphere;
            //for (int i = 1; i < gameObject.Model.Meshes.Count; i++)
            //{
            //    var currentMeshBoundingSphere = gameObject.Model.Meshes[i].BoundingSphere;
            //    gameObject.BoundingSphere = BoundingSphere.CreateMerged(gameObject.BoundingSphere,
            //        currentMeshBoundingSphere);
            //}
            //gameObject.BoundingSphere.Radius *= Settings.CharacterScale;


            return gameObject.BoundingSphere;
        }

        #endregion
    }
}
