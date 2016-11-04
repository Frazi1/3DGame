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
            UpdateCameraPosition();

            base.Update(gameTime);
        }

        private void UpdateCameraPosition()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                CamPosition.X += 1f;
                CamTarget.X += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                CamPosition.X -= 1f;
                CamTarget.X -= 1f;
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

            //ViewMatrix = Matrix.CreateLookAt(CamPosition, CamTarget, Vector3.Up);
        }
    }
}
