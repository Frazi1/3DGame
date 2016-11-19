using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    public class Menu
    {
        public List<MenuItem> Items { get; set; }
        public string Header { get; set; }

        private SpriteFont font;

        private int currentItem;
        KeyboardState oldState;

        public Menu()
        {
            Items = new List<MenuItem>();
            Header = String.Empty;
            
        }

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("TextFont");
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Enter))
                Items[currentItem].OnClick();

            int delta = 0;
            if (state.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                delta = -1;
            if (state.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                delta = 1;

            CurrentItem += delta;

            oldState = state;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.Default,
                RasterizerState.CullNone);
            int y = 100;
            foreach (MenuItem menuItem in Items)
            {
                Color color = Color.White;
                if (!menuItem.Active)
                    color = Color.Gray;
                if (menuItem == Items[currentItem])
                    color = Color.Red;

                spriteBatch.DrawString(font,
                    menuItem.Name,
                    new Vector2(10, y),
                    color);
                y += 20;
            }

            spriteBatch.DrawString(font,
                Header,
                new Vector2(x: spriteBatch.GraphicsDevice.Viewport.Width /2,
                y: spriteBatch.GraphicsDevice.Viewport.Height/2),
                Color.Red);

            spriteBatch.End();
        }

        public void SetActive(string itemName)
        {
            Items.Find(g => g.Name == itemName).Active = true;
        }
        public void SetUnActive(string itemName)
        {
            Items.Find(g => g.Name == itemName).Active = false;
        }

        #endregion


        #region Properties

        public int CurrentItem
        {
            get { return currentItem; }
            set
            {
                if (value < 0)
                    currentItem = Items.Count - 1;
                else if (value >= Items.Count)
                    currentItem = 0;
                else if (!Items[value].Active)
                    CurrentItem = value + 1;
                else
                    currentItem = value;
            }
        }

        #endregion

    }
}