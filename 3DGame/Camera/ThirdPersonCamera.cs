using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    public class ThirdPersonCamera : Camera
    {
        public ThirdPersonCamera(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            UpdateCameraPosition1();

            base.Update(gameTime);
        }

        private void UpdateCameraPosition()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                CamPosition.X += 1f;
                //CamTarget.X += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                CamPosition.X -= 1f;
                //CamTarget.X -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                CamPosition.Y += 1f;
                CamTarget.Y += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CamPosition.Y -= 1f;
                CamTarget.Y -= 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                CamPosition.Z += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                CamPosition.Z -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Orbit = !Orbit;
            }
            if (Orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(
                    MathHelper.ToRadians(1f));
                CamPosition = Vector3.Transform(CamPosition, rotationMatrix);
            }

            ViewMatrix = Matrix.CreateLookAt(CamPosition, CamTarget, Vector3.Up);
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
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    Orbit = !Orbit;
            //}
            //if (Orbit)
            //{
            //    rotationMatrix = Matrix.CreateRotationY(
            //        MathHelper.ToRadians(1f));
            //}
            CamPosition = Vector3.Transform(CamPosition, rotationMatrix);

        }
    }
}
