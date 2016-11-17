using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public class Box: IGameObject
    {
        private Model model;

        private BoundingBox boundingBox;
        private BoundingSphere boundingSphere;

        private bool isActive;

        private Vector3 position;
        
        private Matrix world = Matrix.Identity;
        private Matrix rotationMatrix = Matrix.Identity;

        private Matrix[] transforms;


        public void Initialize(ContentManager contentManager)
        {

            Model = contentManager.Load<Model>("QB");
            Transforms = Drawer.SetupEffectDefaults(Model);
            IsActive = true;

            World = RotationMatrix * Matrix.CreateTranslation(Position);
            //World = Matrix.CreateWorld(position,Vector3.Forward, Vector3.Up);
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

        #endregion

    }



}