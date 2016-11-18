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
    public class Road: IGameObject
    {
        private Model model;

        private Vector3 position = Vector3.Zero;

        private BoundingBox boundingBox;
        private BoundingSphere boundingSphere;

        private bool isActive;

        private Matrix rotationMatrix = Matrix.Identity;
        private Matrix world = Matrix.Identity;

        private Matrix[] transforms;

        #region Methods
        public void Initialize(Vector3 position, ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("roadV2");
            Position = position;
            Transforms = Drawer.SetupEffectDefaults(Model);
            IsActive = true;
            SetWorld();
            //World = Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up);
        }

        public void SetWorld()
        {
            World = RotationMatrix * Matrix.CreateTranslation(Position);
        }

        #endregion

        #region Properties

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public Matrix RotationMatrix
        {
            get { return rotationMatrix; }
            set { rotationMatrix = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        public Matrix[] Transforms
        {
            get { return transforms; }
            set { transforms = value; }
        }


        public BoundingBox BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public BoundingSphere BoundingSphere
        {
            get { return boundingSphere; }
            set { boundingSphere = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        #endregion
    }

}
