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
        SpriteFont textFont;
        SpriteBatch spriteBatch;


        Character character;
        Camera camera;
        List<Road> roads = new List<Road>();
        List<Box> boxes = new List<Box>();

        //Level
        Level level;

        Random rnd;

        //Debug
        bool collided = false;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferMultiSampling = true,
                PreferredBackBufferHeight = 800,
                PreferredBackBufferWidth = 1000,
                //SynchronizeWithVerticalRetrace = true,
                //PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8

            };
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
            character = new Character { Position = Settings.StartingPlayerPosition };
            character.Initialize(Content);

            //Camera Initialization
            camera = new ThirdPersonCamera(this, character)
            {
                TargetGameObject = character,
            };
            ((ThirdPersonCamera)camera).SetTargetGameObject(character);
            Components.Add(camera);


            //Road Initialization
            CreateRoads();


            //Box

            //Bounding Spheres Renderer Initialize
            BoundingSphereRenderer.Initialize(GraphicsDevice, 50);

            //Bouding Box Renderer Initialize
            BoundingBoxRenderer.Initialize(GraphicsDevice);

            //Set RasterizerState
            GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };

            //Random
            rnd = new Random();

            //Level
            level = new Level();

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void LoadContent()
        {
            textFont = Content.Load<SpriteFont>("TextFont");
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
            level.Update(gameTime);
            SpawnBlocks(gameTime);
            //SetDebugText(level.CurrentLevel + " - " + boxes.Count);
            //SetDebugText(character.Position.ToString());



            base.Update(gameTime);

        }
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawRoads();
            character.DrawModel(camera);
            //character.DrawBoundingSphere(camera);

            for (int i = 0; i < boxes.Count; i++)
            {
                var box = boxes[i];
                if (!box.IsActive)
                    continue;

                box.DrawModel(camera);

                //box.DrawBoundingSphere(camera);
            }

            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.Default,
                RasterizerState.CullNone);
            spriteBatch.DrawString(textFont,
                "Health:" + character.CurrentHealth,
                new Vector2(1, 1),
                Color.Black
                );
            spriteBatch.End();

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
            const int count = 1;
            float roadLength = 3.4f;
            for (int i = 0; i < count; i++)
            {
                Road r = new Road();
                r.Initialize(Vector3.Zero + new Vector3(0, 0, roadLength * i), Content);
                roads.Add(r);

            }
        }

        private void EventsSubscribe()
        {
            GameEvents.ObjectCollided += GameEvents_ObjectCollided;
            GameEvents.CharacterDied += GameEvents_CharacterDied;
            GameEvents.BoxDestroyed += GameEvents_BoxDestroyed;
            GameEvents.CharacterHit += GameEvents_CharacterHit;
        }

        //Debug Methods
        private void SetDebugText(string text)
        {
            Window.Title = text;
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
            Exit();
        }
        private void GameEvents_CharacterHit(Character arg1, IGameObject arg2)
        {
            arg1.CurrentHealth--;
            arg2.IsActive = false;
        }


        //GameLogic
        private void SpawnBlocks(GameTime gameTime)
        {
            //int number = level.CurrentLevel * 3;

            if (!level.BoxSpawned)
            {
                    boxes.Add(GameObjectCreator.CreateBox(
                        GameObjectCreator.GetRandomPosition(gameTime),
                        character.Position));
                level.BoxSpawnedNumber++;
                //level.ElapsedBoxSpawned = new TimeSpan();
            }            

        }



    }





}
