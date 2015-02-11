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

        Texture2D cust;

        // Game Sprites
        Stand mainStand;
        Sprite floor;
        Sprite up, down, shop_btn, shop;

        //
        CustomerFlow customers;
        Season season;

        List<Customer> customer;

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

        float timerC;
        float maxTimerC = 1500f;

        // Booleans
        bool isHovering;
        bool isHovering2;
        bool gainedCash;
        bool shopOpened;

        bool b1 = true;

        int totalC = 0;
        int randomTexture;

        Random rand = new Random();

        Random random = new Random();

        #endregion

        #region Constructors

        // Constructor
        public GSPlay(GameTime gameTime)
        {
            mainStand = new Stand(Content.Load<Texture2D>("cart2"), new Vector2(500, 405), 0.5f);
            bgMusic = Content.Load<Song>("music");
            floor = new Sprite(Content.Load<Texture2D>("scenary"), new Vector2(500, 300), 1);
            up = new Sprite(Content.Load<Texture2D>("up"), new Vector2(165, 57), .8f);
            down = new Sprite(Content.Load<Texture2D>("down"), new Vector2(165, 66), .8f); 
            shop_btn = new Sprite(Content.Load<Texture2D>("shop_btn"), new Vector2(945, 580), 0.5f);
            shop = new Sprite(Content.Load<Texture2D>("shop"), new Vector2(850, 238), 1f); 
            font = Content.Load<SpriteFont>("SpriteFont1");
            cust = Content.Load<Texture2D>("cart2"); 
            customers = new CustomerFlow();
            customer = new List<Customer>();
            headlines = new Headlines();
            season = new Season();
            LoadCustomers(gameTime);

            

            up.color = Color.Black;
            down.color = Color.Black;

            MediaPlayer.Play(bgMusic);
            MediaPlayer.IsRepeating = true;

            HUD.money = 10;
            max = 5;
            cost = 3;

            pos = new Vector2(10, 10);

            gainedCash = false;
        }

        #endregion

        #region Main Methods

        public override void Update(GameTime gameTime)
        {
            randomTexture = random.Next(0, 8);

            //Console.WriteLine("X: " + Mouse.GetState().X + " Y: " + Mouse.GetState().Y);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timerC += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            Console.WriteLine(customer.Count.ToString());

            season.Update(gameTime);
            DeveloperCommands(gameTime);
            customers.CalculateCustomerFlow();
            PurchaseIceCream(gameTime, customers.CalculateCustomerFlow());
            CreateButton();
            TestHoveringStand();
            UpdateShop(gameTime);
            LoadCustomers(gameTime);

            if (b1 == true)
            {
                totalC = customers.noOfCustomers;
                b1 = false;
            }

            foreach (Customer c in customer)
            {
                c.Update(gameTime);
            }

            #region Testing Code

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

            #endregion

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            floor.Draw(spriteBatch);
            mainStand.Draw(spriteBatch);

            foreach (Customer c in customer)
            {
                c.Draw(spriteBatch);
            }

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

        #endregion

        #region Private Methods

        private void TestHoveringStand()                // Checking if you are hovering over the stand
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

        private void CreateButton()                     // Creates a button
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

        private void ResetStats()                       // Resets Stats
        {
            HUD.money = 0;
        }

        private void DeveloperCommands(GameTime gameTime)                // Developer Commands
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                GameStateManager.SwitchToPlay(gameTime);
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

        private void UpdateShop(GameTime gameTime)      // Updates Shop
        {
            if (shopOpened)
            {
                if (Mouse.GetState().X > 966 && Mouse.GetState().X < 994 &&
                    Mouse.GetState().Y > 39 && Mouse.GetState().Y < 67)
                {
                    if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                        shopOpened = false;
                }

            }

            if (Mouse.GetState().X > 896 && Mouse.GetState().X < 993 &&
                Mouse.GetState().Y > 568 && Mouse.GetState().Y < 592)
            {
                if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                    shopOpened = true;
            }
        }

        /// <summary>
        /// Function for purchasing ice cream
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="dailyCustomers"></param>
        private void PurchaseIceCream(GameTime gameTime, int dailyCustomers)
        {
            timer2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer2 >= 500f)
            {
                if (purchases < dailyCustomers)
                {
                    //HUD.money += cost;
                    purchases++;
                    //Console.WriteLine(purchases.ToString());
                    timer2 = 0f;
                }
            }
        }

        public void LoadCustomers(GameTime gameTime)
        {
            int randX = rand.Next(-200, -50);

            int newValue = rand.Next(0, 4);

            bool spawnCustomer = false;

            if (timerC > maxTimerC)
            {
                timerC = 0f;
                spawnCustomer = true;
            }
            else
            {
                spawnCustomer = false;
            }

            Console.WriteLine(timerC);

            if (customer.Count() < totalC && spawnCustomer)
            {
                if (randomTexture == 1)
                {
                    customer.Add(new Customer(Content.Load<Texture2D>("Atheist_Male"), new Vector2(randX, 500), 0.15f, newValue,6,2));
                }
                else if (randomTexture == 2)
                {
                    customer.Add(new Customer(Content.Load<Texture2D>("Cat_Burglar"), new Vector2(randX, 500), 0.17f, newValue,12,1));
                }
                else if (randomTexture == 3)
                {
                    customer.Add(new Customer(Content.Load<Texture2D>("Santa"), new Vector2(randX, 500), 0.17f, newValue,100, 50));
                }
                else if (randomTexture == 4)
                {
                    customer.Add(new Customer(Content.Load<Texture2D>("Serial_Killer_Male"), new Vector2(randX, 500), 0.15f, newValue, 10, 0));
                }
                else if (randomTexture == 5)
                {
                    customer.Add(new Customer(Content.Load<Texture2D>("Singer_Female"), new Vector2(randX, 500), 0.17f, newValue, 32, 2));
                }
                else if (randomTexture == 6)
                {
                    customer.Add(new Customer(Content.Load<Texture2D>("Superhero"), new Vector2(randX, 500), 0.17f, newValue, 50, 3));
                }
                else if (randomTexture == 7)
                {
                    customer.Add(new Customer(Content.Load<Texture2D>("Time_Travler"), new Vector2(randX, 500), 0.17f, newValue,90,9));
                }
                else if (randomTexture == 8)
                {
                    customer.Add(new Customer(Content.Load<Texture2D>("Wizard_Male"), new Vector2(randX, 500), 0.17f, newValue,67, 5));
                }
                
            }


            for (int i = 0; i < customer.Count; i++)
            {
                if (!customer[i].isAlive)
                {
                    customer.RemoveAt(i);
                }

                customer[i].spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        #endregion
    }
}
