using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static _3DGame.GameEvents;
using static _3DGame.Settings;
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
        readonly List<Road> roads = new List<Road>();
        readonly List<Box> boxes = new List<Box>();


        GameObjectCreator gameObjectCreator;

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

            //Initialize GameObjectCreator
            gameObjectCreator = new GameObjectCreator(Content);

            //Subcribe to Events
            EventsSubscribe();

            //Model Initialization
            character = new Character { Position = Vector3.Zero };
            character.Initialize(Content);

            //Camera Initialization
            camera = new ThirdPersonCamera(this)
            {
                CamPosition = new Vector3(-100, 0, 100),
                CamTarget = character.Position,
            };
            Components.Add(camera);


            //Road Initialization
            CreateRoads();


            //Floor Initialization
            //InitializeFloor();


            //Box
            boxes.Add(gameObjectCreator.CreateBox(character.Position + character.RotationMatrix.Forward * 2 + character.RotationMatrix.Right * 5));
        }

        private void EventsSubscribe()
        {
            GameEvents.ObjectCollided += GameEvents_ObjectCollided;
        }


        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            character.Update(gameTime);
            Collider.CollisionDetection(character, boxes);
            //if (box != null)
            //    //Collider.CheckCollision(character, box);
            Reset();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            DrawRoads();

            Matrix characterTransormsMatrix = character.RotationMatrix * Matrix.CreateTranslation(character.Position) *
                                              Matrix.CreateScale(CharacterScale);
            Drawer.DrawModel(character.Model, characterTransormsMatrix, character.Transforms, camera);


            for (int i = 0; i < boxes.Count; i++)
            {
                var box = boxes[i];
                if(!box.IsActive)
                    continue;
                Matrix boxTransformsMatrix = box.RotationMatrix * Matrix.CreateTranslation(box.Position) *
                                             Matrix.CreateScale(CharacterScale);
                Drawer.DrawModel(box.Model, boxTransformsMatrix, box.Transforms, camera);
            }

            base.Draw(gameTime);
        }

        private void DrawRoads()
        {
            foreach (Road road in roads)
            {
                Matrix roadTransformsMatrix = road.RotationMatrix * Matrix.CreateTranslation(road.Position);
                Drawer.DrawModel(road.Model, roadTransformsMatrix, road.Transforms, camera);
            }
        }

        private void CreateRoads()
        {
            int count = 5;
            float roadLength = 3.4f;
            for (int i = 0; i < count; i++)
            {
                Road r = new Road();
                r.Initialize(Settings.StartingPlayerPosition + new Vector3(0, 0, roadLength * i), Content);
                roads.Add(r);

            }
        }


        public void Reset()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                for (int i = 0; i < boxes.Count; i++)
                {
                    boxes[i].IsActive = true;
                }
        }

        //Events Methods
        private void GameEvents_ObjectCollided(ICollidable arg1, ICollidable arg2)
        {
            arg2.IsActive = false;
        }


    }





}
