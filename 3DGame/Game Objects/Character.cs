using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static _3DGame.Settings;

namespace _3DGame
{
    public class Character: IGameObject
    {
        private Model model;
        private bool isActive;

        private BoundingBox boundingBox;
        private BoundingSphere boundingSphere;

        private Vector3 direction;
        private Vector3 position = Vector3.Zero;

        private float rotation = 0;

        private Matrix rotationMatrix = Matrix.Identity;
        private Matrix world = Matrix.Identity;

        private Matrix[] transforms;

        private int currentHealth;

        #region Methods

        public void Initialize(ContentManager contentManager)
        {
            Model = contentManager.Load<Model>("sinon");
            direction = Vector3.Forward;
            Transforms = Drawer.SetupEffectDefaults(Model);
            IsActive = true;

            SetWorld();

            CurrentHealth = Settings.Character_Max_Heath;

        }

        public void Update(GameTime gameTime)
        {
            //angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateCharacterPosition(gameTime);
            SetWorld();

        }

        private void UpdateCharacterPosition(GameTime gameTime)
        {
            float amount = 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Vector3 moveVector = RotationMatrix.Forward * -amount;
                if ((Position + moveVector).CheckPositon())
                    Position += moveVector;

                //Position += RotationMatrix.Forward * - amount;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Vector3 moveVector = RotationMatrix.Forward * amount;
                if ((Position + moveVector).CheckPositon())
                    Position += moveVector;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Vector3 moveVector =  Vector3.Cross(RotationMatrix.Up, RotationMatrix.Forward) * -amount;
                if ((Position + moveVector).CheckPositon())
                    Position += moveVector;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Vector3 moveVector = Vector3.Cross(RotationMatrix.Up, RotationMatrix.Forward) * amount;
                if ((Position + moveVector).CheckPositon())
                    Position += moveVector;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Rotation += 1f / 500f * gameTime.ElapsedGameTime.Milliseconds;
                RotationMatrix = Matrix.CreateRotationY(Rotation);

                Direction = Vector3.Transform(direction, RotationMatrix);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Rotation -= 1f / 500f * gameTime.ElapsedGameTime.Milliseconds;
                RotationMatrix = Matrix.CreateRotationY(Rotation);

                Direction = Vector3.Transform(direction, RotationMatrix);
            }

        }

        public void SetWorld()
        {
            World = RotationMatrix
    * Matrix.CreateTranslation(Position)
    * Matrix.CreateScale(Character_Scale);
        }

        #endregion


        #region Properties


        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
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



        public float Rotation
        {
            get { return rotation; }
            set
            {
                float newVal = value;
                while (newVal >= MathHelper.TwoPi)
                {
                    newVal -= MathHelper.TwoPi;
                }
                while (newVal < 0)
                {
                    newVal += MathHelper.TwoPi;
                }

                if (rotation != newVal)
                {
                    rotation = newVal;
                    RotationMatrix = Matrix.CreateRotationY(rotation);
                }
            }

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

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = value;
                if (currentHealth <= 0)
                    GameEvents.OnCharacterDied(this);
            }
        }

        #endregion
    }
}
