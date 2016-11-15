using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public class Box: IGameObject
    {
        private BoundingBox boundingBox;
        private BoundingSphere boundingSphere;

        private Model model;
        private Matrix rotationMatrix = Matrix.Identity;
        private Vector3 position = Vector3.Zero;
        private Matrix[] transforms;
        private bool isActive;


        public void Initialize(ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("sinon");
            Transforms = Drawer.SetupEffectDefaults(Model);
            IsActive = true;
        }
        #region Properties



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

        public Matrix[] Transforms
        {
            get { return transforms; }
            set { transforms = value; }
        }

        #endregion

    }



}