using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static _3DGame.GameEvents;
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
        //Matrix World = Matrix.Identity;
        List<Road> roads = new List<Road>();
        Box box;

        //floor
        VertexPositionNormalTexture[] floorVertecies;
        //Texture2D checkerboardTexture2D;
        BasicEffect floorBasicEffect;
        BasicEffect lineEffect;
        BasicEffect lineEffect1;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferHeight = /*1080*/ 600;
            graphics.PreferredBackBufferWidth = /*1920*/ 800;
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


            //Subcribe to Events
            EventsSubscribe();

            //Model Initialization
            character = new Character();
            character.Initialize(Content);

            //Road Initialization
            CreateRoads();


            //Floor Initialization
            //InitializeFloor();


            //Box
            box = new Box(Settings.StartingPlayerPosition, Content);
            //box.AddToPosition(Vector3.Forward * 10 + Vector3.Left*10);

            //CharBoxEffect
            lineEffect = new BasicEffect(GraphicsDevice);
            lineEffect.LightingEnabled = false;
            lineEffect.TextureEnabled = false;
            lineEffect.VertexColorEnabled = true;
            //BoxBoxEffect
            lineEffect1 = new BasicEffect(GraphicsDevice);
            lineEffect1.LightingEnabled = false;
            lineEffect1.TextureEnabled = false;
            lineEffect1.VertexColorEnabled = true;
        }

        private void EventsSubscribe()
        {
            GameEvents.ObjectCollided += GameEvents_ObjectCollided;
        }


        protected override void LoadContent()
        {
            //model = Content.Load<Model>("sinon");
            //checkerboardTexture2D = Content.Load<Texture2D>("checkerboard");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //camera.Update(gameTime);
            character.Update(gameTime);
            if (box != null)
                //Collider.CheckCollision(character, box);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            //DrawModel(road, camera.ProjectionMatrix,camera.ViewMatrix,character.GetWorldMatrix() * Matrix.CreateTranslation(new Vector3(-.5f,-0.35f,0)));

            character.Draw(camera);
            DrawRoads();

            if (box != null)
                Drawer.DrawModel(box.Model, camera, box.WorldMatrix, 0.05f);

            //DrawFloor();

            //Draw BB's
            Drawer.DrawBoundingBox(BoundingBoxBuffers.CreateBoundingBoxBuffers(character.BoundingBox, GraphicsDevice), lineEffect, GraphicsDevice, camera.ViewMatrix, camera.ProjectionMatrix);
            Drawer.DrawBoundingBox(BoundingBoxBuffers.CreateBoundingBoxBuffers(box.BoundingBox, GraphicsDevice), lineEffect1, GraphicsDevice, camera.ViewMatrix, camera.ProjectionMatrix);
            base.Draw(gameTime);
        }

        private void DrawRoads()
        {
            foreach (Road road in roads)
            {
                road.Draw(camera);
            }
        }
        private void DrawFloor()
        {
            var cameraLookAtVector = Vector3.Zero;
            //var cameraUpVector = Vector3.UnitZ;
            var cameraUpVector = Vector3.Up;
            //var cameraUpVector = Vector3.Cross(Vector3.Up, Vector3.UnitZ);

            //floorBasicEffect.View = Matrix.CreateLookAt(
            //    camera.CamPosition, cameraLookAtVector, cameraUpVector);
            floorBasicEffect.View = camera.ViewMatrix;


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
        private void CreateRoads()
        {
            int count = 5;
            float roadLength = 3.4f;
            for (int i = 0; i < count; i++)
            {
                Road r = new Road(Settings.StartingPlayerPosition + new Vector3(0, 0, roadLength * i));
                roads.Add(r);
                r.Initialize(Content);
            }
        }


        //Events Methods
        private void GameEvents_ObjectCollided(ICollidable arg1, ICollidable arg2)
        {
            box = null;
        }


    }





}
