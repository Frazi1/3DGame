using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1: Game
    {
        GraphicsDeviceManager graphics;
        Character character;
        Camera camera;
        Matrix World = Matrix.Identity;

        //floor
        VertexPositionNormalTexture[] floorVertecies;
        Texture2D checkerboardTexture2D;
        BasicEffect floorBasicEffect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.Viewport.Height;
            //graphics.PreferredBackBufferWidth = GraphicsDevice.Viewport.Width;
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            base.Initialize();
            //Camera Initialization
            camera = new ThirdPersonCamera(this)
            {
                CamPosition = new Vector3(0, 130, -150),
                CamTarget = new Vector3(0f, 0f, 0f),
                //WorldMatrix = Matrix.CreateWorld(new Vector3(0, 0, 0), Vector3.Forward, Vector3.Up)
                // {X:0 Y:143,3337 Z:-499,9992}
                //camera.CamPosition = new Vector3(0,0,6);
            };
            Components.Add(camera);

            //Model Initialization
            character = new Character();
            character.Initialize(Content);

            //Floor Initialization
            InitializeFloor();

        }

        private void InitializeFloor()
        {
            floorVertecies = new VertexPositionNormalTexture[6];

            floorVertecies[0].Position = new Vector3(-20, -20, 0);
            floorVertecies[1].Position = new Vector3(-20, 20, 0);
            floorVertecies[2].Position = new Vector3(20, -20, 0);

            floorVertecies[3].Position = floorVertecies[1].Position;
            floorVertecies[4].Position = new Vector3(20, 20, 0);
            floorVertecies[5].Position = floorVertecies[2].Position;

            int repetitions = 20;

            floorVertecies[0].TextureCoordinate = new Vector2(0, 0);
            floorVertecies[1].TextureCoordinate = new Vector2(0, repetitions);
            floorVertecies[2].TextureCoordinate = new Vector2(repetitions, 0);

            floorVertecies[3].TextureCoordinate = floorVertecies[1].TextureCoordinate;
            floorVertecies[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            floorVertecies[5].TextureCoordinate = floorVertecies[2].TextureCoordinate;

            floorBasicEffect = new BasicEffect(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            //model = Content.Load<Model>("sinon");
            checkerboardTexture2D = Content.Load<Texture2D>("checkerboard");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //camera.Update(gameTime);
            character.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            //foreach (ModelMesh mesh in model.Meshes)
            //{
            //    foreach (BasicEffect effect in mesh.Effects)
            //    {
            //        effect.EnableDefaultLighting();
            //        effect.PreferPerPixelLighting = true;
            //        effect.World = Matrix.Identity;
            //        effect.View = camera.ViewMatrix;
            //        effect.Projection = camera.ProjectionMatrix;
            //    }
            //    mesh.Draw();
            //}
            character.Draw(camera);
            DrawFloor();
            base.Draw(gameTime);
        }

        protected void DrawFloor()
        {
            var cameraLookAtVector = Vector3.Zero;
            var cameraUpVector = Vector3.UnitZ;
            //var cameraUpVector = Vector3.Cross(Vector3.Up, Vector3.UnitZ);

            floorBasicEffect.View = Matrix.CreateLookAt(
                camera.CamPosition, cameraLookAtVector, cameraUpVector);

            //float aspectRatio =
            //    graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            //float fieldOfView = MathHelper.PiOver4;
            //float nearClipPlane = 1;
            //float farClipPlane = 1000;

            //floorBasicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
            //fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            floorBasicEffect.Projection = camera.ProjectionMatrix;

            floorBasicEffect.TextureEnabled = true;
            floorBasicEffect.Texture = checkerboardTexture2D;
            //floorBasicEffect.View = camera.ViewMatrix;

            foreach (var pass in floorBasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(
                            PrimitiveType.TriangleList,
                    floorVertecies,
                    0,
                    2);
            }
        }
    }
}
