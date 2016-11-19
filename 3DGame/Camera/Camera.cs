using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    public class Camera: GameComponent
    {
        public Vector3 Target;
        public Vector3 Position;
        
        public Matrix ProjectionMatrix { get; protected set; }
        public Matrix ViewMatrix { get; protected set; }
        public float AspectRatio => Game.GraphicsDevice.Viewport.AspectRatio;

        public const float NearClip = 1.0f;
        public const float FarClip = 1000.0f;
        public const float ViewAngle = MathHelper.PiOver4;


        public Camera(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            //ViewMatrix = Matrix.CreateLookAt(Position, Target, Vector3.Up);
            //ViewMatrix = Matrix.CreateLookAt(Position, Target, Vector3.Up);
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(ViewAngle), AspectRatio, NearClip, FarClip);
            //WorldMatrix = /*Matrix.CreateWorld(Target, Vector3.Forward, Vector3.Up);*/
                //Matrix.Identity;

            //ViewMatrix=Matrix.Identity;
            //ProjectionMatrix=Matrix.Identity;;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            ViewMatrix = Matrix.CreateLookAt(Position, Target, Vector3.Up);
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(ViewAngle), AspectRatio, NearClip, FarClip);

            base.Update(gameTime);
        }
    }
}
