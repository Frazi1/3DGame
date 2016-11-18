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
        List<Road> roads = new List<Road>();
        List<Box> boxes = new List<Box>();

        //Debug
        bool collided = false;

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
            GameObjectCreator.Initialize(Content);

            //Subcribe to Events
            EventsSubscribe();

            //Model Initialization
            //character = new Character { Position = new Vector3(1, 0, 10) };
            character = new Character { Position = new Vector3(10, 0, 10) };
            character.Initialize(Content);

            //Camera Initialization
            camera = new ThirdPersonCamera(this, character)
            {
                //Position = new Vector3(-100, 0, 100),
                Position = new Vector3(0, 10, 80),
                //Target = character.Position,
                Target = new Vector3(0f, 0f, 0f)
            };
            Components.Add(camera);


            //Road Initialization
            CreateRoads();


            //Floor Initialization
            //InitializeFloor();


            //Box
            //boxes.Add(gameObjectCreator.CreateBox());
            boxes.Add(GameObjectCreator.CreateBox(new Vector3(10, 1, 40)));

            //Bounding Spheres Renderer Initialize
            BoundingSphereRenderer.Initialize(GraphicsDevice, 50);

            //Bouding Box Renderer Initialize
            BoundingBoxRenderer.Initialize(GraphicsDevice);

            //Set RasterizerState
            GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };
        }

        private void EventsSubscribe()
        {
            GameEvents.ObjectCollided += GameEvents_ObjectCollided;
            GameEvents.CharacterDied += GameEvents_CharacterDied;
            GameEvents.BoxDestroyed += GameEvents_BoxDestroyed;
            GameEvents.CharacterHit += GameEvents_CharacterHit;
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
            for (int i = 0; i < boxes.Count; i++)
            {
                boxes[i].Update(gameTime);
                if (!boxes[i].IsActive)
                {
                    boxes.RemoveAt(i);
                    --i;
                }
            }

            collided = Collider.CollisionDetection(character, boxes);

            Reset();
            //Window.Title = camera.Position.ToString();
            Window.Title = character.CurrentHealth.ToString();




            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //GraphicsDevice.RasterizerState = rasterizerState;

            DrawRoads();

            //Matrix characterTransormsMatrix = character.RotationMatrix 
            //    * Matrix.CreateTranslation(character.Position) 
            //    * Matrix.CreateScale(Character_Scale);
            //character.World = characterTransormsMatrix;



            character.DrawModel(camera);
            character.DrawBoundingSphere(camera);

            for (int i = 0; i < boxes.Count; i++)
            {
                var box = boxes[i];
                if (!box.IsActive)
                    continue;
                //Matrix boxTransformsMatrix = box.RotationMatrix 
                //    * Matrix.CreateTranslation(box.Position) 
                //    * Matrix.CreateScale(/*0.0001f*/Character_Scale);
                //box.World = boxTransformsMatrix;

                box.DrawModel(camera);

                box.DrawBoundingSphere(camera);
                //box.DrawBoundingSphere(camera);
            }

            base.Draw(gameTime);
        }

        private void DrawRoads()
        {
            foreach (Road road in roads)
            {
                //Matrix roadTransformsMatrix = road.RotationMatrix * Matrix.CreateTranslation(road.Position);
                //Drawer.DrawModel(road.Model, roadTransformsMatrix, road.Transforms, camera);
                //road.World = roadTransformsMatrix;
                road.DrawModel(camera);

            }
            //roads[4].CreateBoundingSphere(1);
            //roads[4].DrawBoundingSphere(camera);

            //roads[4].CreateBoundingBox();
            //roads[4].DrawBoundingBox(camera,Color.White);
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

        //Debug Methods
        private void Reset()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                for (int i = 0; i < boxes.Count; i++)
                {
                    boxes[i].IsActive = true;
                }
            if(Keyboard.GetState().IsKeyDown(Keys.K))
                boxes.Add(GameObjectCreator.CreateBox(
                    GameObjectCreator.GetRandomPosition(),
                    character.Position));
        }


        //Events Methods
        private void GameEvents_ObjectCollided(IGameObject arg1, IGameObject arg2)
        {
            //arg2.IsActive = false;
            arg2.IsActive = false;
        }
        private void GameEvents_BoxDestroyed(Box obj)
        {
            //throw new NotImplementedException();
        }

        private void GameEvents_CharacterDied(Character obj)
        {
            //throw new NotImplementedException();
        }

        private void GameEvents_CharacterHit(Character arg1, IGameObject arg2 )
        {
            arg1.CurrentHealth--;
            arg2.IsActive = false;
        }



    }





}
