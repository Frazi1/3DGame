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
        public IGameObject TargetGameObject;
        public Vector3 Distance;

        public ThirdPersonCamera(Game game, IGameObject targetGameObject) : base(game)
        {
            //SetTargetGameObject(targetGameObject);
            //Distance = new Vector3(0, -25, 150);
            //Position.X = 0;
        }



        public override void Update(GameTime gameTime)
        {
            UpdateCameraPosition1();

            //Target = TargetGameObject.Position;
            //Position = TargetGameObject.Position - Distance;

            base.Update(gameTime);
        }

        public void SetTargetGameObject(IGameObject targetGameObject)
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
            Matrix rotationMatrix = Matrix.Identity;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotationMatrix = Matrix.CreateRotationY(0.01f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotationMatrix = Matrix.CreateRotationY(-0.01f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                rotationMatrix = Matrix.CreateRotationX(0.01f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                rotationMatrix = Matrix.CreateRotationX(-0.01f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                rotationMatrix = Matrix.CreateTranslation(Vector3.Backward);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                rotationMatrix = Matrix.CreateTranslation(Vector3.Forward);
            }

            Position = Vector3.Transform(Position, rotationMatrix);

        }
    }
}
