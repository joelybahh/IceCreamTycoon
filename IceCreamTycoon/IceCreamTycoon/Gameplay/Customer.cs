using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IceCreamTycoon
{
    public class Customer : Sprite
    {
        int customerSpeed;
        string preference;
        protected int moneyOnHand;
        protected int maxWillSpend;
        int chance;
        public bool desperateForIceCream;
        bool isMoving;
        bool willStop;
        bool atStand;
        bool wasAtStand;
        public bool payed;
        public bool isAlive = true;

        int random1;
        int random2;
        int random3;

        float timer, max = 2000f;

        public int value;

        Random rand = new Random();

        public Customer(Texture2D texture, Vector2 position, float scale, int chance, int moneyOnHand, int maxWillSpend, IceCream.IceCreamType preference)
            : base(texture, position, scale)
        {
            this.preference = preference.ToString();
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            this.chance = chance;
            this.moneyOnHand = moneyOnHand;
            this.maxWillSpend = maxWillSpend;
            customerSpeed = 2;

            wasAtStand = false;
            isMoving = true;

            
        }

        public override void Update(GameTime gameTime)
        {
            if (isAlive)
            {
                if (chance >= 2)
                    desperateForIceCream = true;
                else
                    desperateForIceCream = false;

                #region checkingDesperateConditions

                if (desperateForIceCream)
                {
                    customerSpeed = 5;
                    willStop = true;
                }
                else if (!desperateForIceCream && HUD.temperature >= 30)
                {
                    random1 = rand.Next(0, 1);

                    if (random1 == 1)
                    {
                        willStop = true;
                    }
                }
                else if (!desperateForIceCream && HUD.temperature > 23 && HUD.temperature < 27)
                {
                    random2 = rand.Next(0, 3);

                    if (random2 == 1)
                    {
                        willStop = true;
                    }
                }
                else
                {
                    random3 = rand.Next(0, 10);

                    if (random3 == 1)
                    {
                        willStop = true;
                    }
                }

                #endregion

                if (!atStand && position.X >= 500 && position.X <= 700 && !wasAtStand && HUD.NumberOfIceCream > 0)
                {
                    atStand = true;
                }

                if (isMoving)
                    position.X += customerSpeed;

                if (atStand)
                {
                    isMoving = false;

                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timer >= max)
                    {
                        if (preference.Equals(IceCream.IceCreamType.Chocolate.ToString()) && HUD.ChocolateIceCream > 0)
                        {
                            atStand = false;
                            wasAtStand = true;
                            HUD.money += HUD.price;
                            HUD.NumberOfIceCream--;
                            HUD.ChocolateIceCream--;
                        }
                        else
                        {
                            atStand = false;
                            wasAtStand = true;
                            customerSpeed = 5;
                            HUD.popularity--;
                        }
                    }
                }

                if (atStand && HUD.NumberOfIceCream == 0)
                {
                    isMoving = true;
                    atStand = false;
                }

                if (wasAtStand)
                {
                    isMoving = true;
                    atStand = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
