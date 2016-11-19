using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static _3DGame.Settings;

namespace _3DGame
{
    public class Box: IGameObject
    {
        private Model model;

        private BoundingBox boundingBox;
        private BoundingSphere boundingSphere;

        private float speed;
        private float velocity = Box_Velocity;

        private float livingTime;
        private TimeSpan livingTimeSpan;

        private bool isActive;

        private Vector3 position;
        private Vector3 target;
        private Vector3 direction;

        private Matrix world = Matrix.Identity;
        private Matrix rotationMatrix = Matrix.Identity;

        private Matrix[] transforms;


        public void Initialize(ContentManager contentManager)
        {

            Model = contentManager.Load<Model>("QB");
            Transforms = Drawer.SetupEffectDefaults(Model);
            IsActive = true;

            livingTimeSpan = new TimeSpan();
            SetWorld();

            //World = RotationMatrix * Matrix.CreateTranslation(Position);
            //World = Matrix.CreateWorld(position,Vector3.Forward, Vector3.Up);
        }

        public void Update(GameTime gameTime)
        {
            if (Direction == Vector3.Zero)
            {
                direction = target - position;
                //
                direction.Y = 0;
                //
                direction.Normalize();
            }

            Speed += Velocity;
            Position += Direction * Speed;

            SetWorld();

            //LivingTime += gameTime.ElapsedGameTime.;
            livingTimeSpan = livingTimeSpan.Add(gameTime.ElapsedGameTime);
            if (livingTimeSpan.Seconds>= Box_Living_Time)
                IsActive = false;
        }

        public void SetWorld()
        {
            World = RotationMatrix
                    * Matrix.CreateTranslation(Position)
                    * Matrix.CreateScale(/*0.0001f*/Character_Scale);
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
            set
            {
                isActive = value;
                if(value == false)
                    GameEvents.OnBoxDestroyed(this);
            }
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

        public Vector3 Target
        {
            get { return target; }
            set { target = value; }
        }

        public float Speed
        {
            get { return speed; }
            set
            {
                if (speed + value < Box_Max_Speed)
                    speed = value;
            }
        }

        public float Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public float LivingTime
        {
            get { return livingTime; }
            set { livingTime = value; }
        }



        #endregion

    }



}