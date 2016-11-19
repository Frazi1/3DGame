using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    public class ThirdPersonCamera: Camera
    {
        public Character TargetGameObject;
        public float Distance;

        private Vector3 direction = Vector3.Forward;

        float angle = 0;
        Matrix rotationMatrix = Matrix.Identity;

        public ThirdPersonCamera(Game game, IGameObject targetGameObject) : base(game)
        {
            //SetTargetGameObject(targetGameObject);
            //Distance = new Vector3(0, -25, 150);
            //Position.X = 0;
            //Direction = Target;
            //Direction.Normalize();
        }



        public override void Update(GameTime gameTime)
        {
            //UpdateCameraPosition1();
            FollowCharacter();



            base.Update(gameTime);
        }

        public void SetTargetGameObject(Character targetGameObject)
        {
            TargetGameObject = targetGameObject;
        }

        private void UpdateCameraPosition()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position.X += 1f;
                //Target.X += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position.X -= 1f;
                //Target.X -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Position.Y += 1f;
                Target.Y += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Position.Y -= 1f;
                Target.Y -= 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                Position.Z += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                Position.Z -= 1f;
            }


            ViewMatrix = Matrix.CreateLookAt(Position, Target, Vector3.Up);
        }

        private void UpdateCameraPosition1()
        {
            //Matrix rotationMatrix = Matrix.Identity;
            float amount = 0.05f;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                RotationMatrix = Matrix.CreateRotationY(0.01f);
                Direction = Vector3.Transform(Direction, RotationMatrix);
                Direction.Normalize();
                Position = Vector3.Transform(Position, RotationMatrix);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                RotationMatrix = Matrix.CreateRotationY(-0.01f);
                Direction = Vector3.Transform(Direction, RotationMatrix);
                Direction.Normalize();
                Position = Vector3.Transform(Position, RotationMatrix);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //rotationMatrix = Matrix.CreateRotationX(0.01f);
                angle += 0.01f;
                RotationMatrix = Matrix.CreateRotationX(angle);
                Direction = Vector3.Transform(Direction, RotationMatrix);
                Direction.Normalize();
                Position = Vector3.Transform(Position, RotationMatrix);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //rotationMatrix = Matrix.CreateRotationX(-0.01f);
                angle -= 0.01f;

                RotationMatrix = Matrix.CreateRotationX(angle);
                Position = Vector3.Transform(Position, RotationMatrix);
                Direction = Vector3.Transform(Direction, RotationMatrix);
                Direction.Normalize();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                rotationMatrix = Matrix.CreateTranslation(Vector3.Backward);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                rotationMatrix = Matrix.CreateTranslation(Vector3.Forward);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                Position += Direction * amount;
                Target += Direction * amount;
                //rotationMatrix = Matrix.CreateTranslation(Position);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                Position -= Direction * amount;
                Target -= Direction * amount;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                Position += Vector3.Cross(Direction,Vector3.Up) * amount;
                Target += Vector3.Cross(Direction, Vector3.Up) * amount;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                Position -= Vector3.Cross(Direction, Vector3.Up) * amount;
                Target -= Vector3.Cross(Direction, Vector3.Up) * amount;
            }

            //Position = Vector3.Transform(Position,RotationMatrix);
            angle = 0;
        }

        private void FollowCharacter()
        {
            //RotationMatrix = Matrix.CreateRotationY(TargetGameObject.Rotation);

            //Vector3 thirdPersonReference = new Vector3(0,Distance,-Distance);
            //Vector3 transformedReference = Vector3.Transform(thirdPersonReference,
            //    RotationMatrix);
            //Position = transformedReference + TargetGameObject.Position;

            //ViewMatrix = Matrix.CreateLookAt(Position,TargetGameObject.Position, Vector3.Up);
            //ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(ViewAngle,
            //    AspectRatio,
            //    NearClip,
            //    FarClip);

            Position = TargetGameObject.World.Translation + new Vector3(0,Settings.Camera_Heigth,-Settings.Camera_Distance);
            Target = TargetGameObject.World.Translation + (TargetGameObject.World.Up * 3);
            ViewMatrix  =Matrix.CreateLookAt(Position,TargetGameObject.Direction,Vector3.Up);



        }
        #region Properties
        public Matrix RotationMatrix
        {
            get { return rotationMatrix; }
            set { rotationMatrix = value; }
        }

        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        #endregion
    }
}
