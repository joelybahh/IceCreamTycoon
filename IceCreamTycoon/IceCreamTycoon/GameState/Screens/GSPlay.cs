using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace IceCreamTycoon
{
    class GSPlay : GameState
    {
        #region Variables 

        // Game Sprites
        Stand mainStand;
        Sprite floor;
        Sprite up, down, shop_btn, shop;
        CustomerFlow customers;
        Season season;

        //Headlines
        Headlines headlines;

        // Keyboard / Mouse
        MouseState currentMouseState;
        MouseState lastMouseState;

        Song bgMusic;

        // Fonts
        SpriteFont font;

        // Vector2's
        Vector2 pos;

        // Strings
        string text;

        // Integers
        int cost;
        int dailyCustomers;
        int purchases;

        // Floats and Timers
        float timer;
        float timer2;
        float max;

        // Booleans
        bool isHovering;
        bool isHovering2;
        bool gainedCash;
        bool shopOpened;

        #endregion

        // Constructor
        public GSPlay()
        {
            mainStand = new Stand(Content.Load<Texture2D>("cart2"), new Vector2(500, 405), 0.5f);
            bgMusic = Content.Load<Song>("music");
            floor = new Sprite(Content.Load<Texture2D>("scenary"), new Vector2(500, 300), 1);
            up = new Sprite(Content.Load<Texture2D>("up"), new Vector2(165, 57), .8f);
            down = new Sprite(Content.Load<Texture2D>("down"), new Vector2(165, 66), .8f); 
            shop_btn = new Sprite(Content.Load<Texture2D>("shop_btn"), new Vector2(945, 580), 0.5f);
            shop = new Sprite(Content.Load<Texture2D>("shop"), new Vector2(850, 238), 1f); 
            font = Content.Load<SpriteFont>("SpriteFont1");
            customers = new CustomerFlow();
            headlines = new Headlines();
            season = new Season();

            up.color = Color.Black;
            down.color = Color.Black;

            MediaPlayer.Play(bgMusic);
            MediaPlayer.IsRepeating = true;

            HUD.money = 100;
            max = 5;
            cost = 3;

            pos = new Vector2(10, 10);

            gainedCash = false;
        }

        public override void Update(GameTime gameTime)
        {
            season.Update(gameTime);
            DeveloperCommands();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            Console.WriteLine("X: " + Mouse.GetState().X + " Y: " + Mouse.GetState().Y);

            customers.CalculateCustomerFlow();
            PurchaseIceCream(gameTime, customers.CalculateCustomerFlow());

            //Console.WriteLine("Estimated Customers: " + customers.noOfCustomers.ToString());

            CreateButton();
            TestHoveringStand();

            if (shopOpened)
            {
                UpdateShop(gameTime);
            }

            if (Mouse.GetState().X > 896 && Mouse.GetState().X < 993 &&
                Mouse.GetState().Y > 568 && Mouse.GetState().Y < 592)
            {
                if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                    shopOpened = true;
            }

            if (Mouse.GetState().X > 160 && Mouse.GetState().X < 168 &&
                Mouse.GetState().Y > 54 && Mouse.GetState().Y < 60)
            {
                if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                    HUD.temperature += 1;
            }

            if (Mouse.GetState().X > 160 && Mouse.GetState().X < 168 &&
                Mouse.GetState().Y > 63 && Mouse.GetState().Y < 70)
            {
                if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                    HUD.temperature -= 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                HUD.popularity += 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                HUD.popularity -= 1;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Private Function for testing if hovering over the stand
        /// </summary>
        private void TestHoveringStand()
        {
            if (isHovering)
            {
                if (mainStand.scale < 0.51f)
                    mainStand.scale += 0.004f;
                if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                    HUD.money += 3;
            }
            else
            {
                if (mainStand.scale > 0.5f)
                    mainStand.scale -= 0.004f;
            }

            if (isHovering2)
            {
                if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed
                    && HUD.money >= cost)
                    HUD.money -= cost;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            floor.Draw(spriteBatch);
            mainStand.Draw(spriteBatch);

            up.Draw(spriteBatch);
            down.Draw(spriteBatch);
            shop_btn.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Money: $" + HUD.money.ToString(), pos, Color.Black);
            spriteBatch.DrawString(font, "Popularity: " + HUD.popularity.ToString(), new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(font, "Temperature: " + HUD.temperature.ToString() + " C", new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(font, "Estimated Daily Customers: " + customers.noOfCustomers.ToString(), new Vector2(10, 70), Color.Black);

            if (shopOpened)
            {
                shop.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Private Function for creating a button
        /// </summary>
        private void CreateButton()
        {
            if (Mouse.GetState().X > 393 && Mouse.GetState().X < 568 &&
                Mouse.GetState().Y > 400 && Mouse.GetState().Y < 506 ||
                Mouse.GetState().X > 476 && Mouse.GetState().X < 486 &&
                Mouse.GetState().Y > 317 && Mouse.GetState().Y < 405 ||
                Mouse.GetState().X > 567 && Mouse.GetState().X < 616 &&
                Mouse.GetState().Y > 407 && Mouse.GetState().Y < 425)
            {
                isHovering = true;
            }
            else
            {
                isHovering = false;
            }
        }

        /// <summary>
        /// Private Function for ressetting stats
        /// </summary>
        private void ResetStats()
        {
            HUD.money = 0;
        }

        /// <summary>
        /// private function for developer commands
        /// </summary>
        private void DeveloperCommands()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                GameStateManager.SwitchToPlay();
                ResetStats();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                GameStateManager.SwitchToMenu();
                ResetStats();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                GameStateManager.SwitchToSplash();
                ResetStats();
            }
        }

        private void PurchaseIceCream(GameTime gameTime, int dailyCustomers)
        {
            timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer2 >= 1)
            {
                if (purchases < dailyCustomers)
                {
                    HUD.money += cost;
                    purchases++;
                    Console.WriteLine(purchases.ToString());
                    timer2 = 0f;
                }
            }
        }

        private void UpdateShop(GameTime gameTime)
        {
            if (Mouse.GetState().X > 966 && Mouse.GetState().X < 994 &&
                Mouse.GetState().Y > 39 && Mouse.GetState().Y < 67)
            {
                if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                    shopOpened = false;
            }
        }
    }
}
