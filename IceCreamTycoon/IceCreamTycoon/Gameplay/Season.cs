using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IceCreamTycoon
{
    public class Season
    {
        public enum ESeason
        {
            Autumn,
            Spring,
            Summer,
            Winter
        }

        ESeason season;

        Random summerTemp;
        Random springTemp;
        Random autumnTemp;
        Random winterTemp;

        int summer, spring, autumn, winter;

        public Season()
        {
            summerTemp = new Random();
            springTemp = new Random();
            autumnTemp = new Random();
            winterTemp = new Random();
            season = ESeason.Summer;
        }

        public void Update(GameTime gameTime)
        {

            float timer = 0;
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (season)
            {
                case ESeason.Summer:

                    if (timer > 5)
                    {
                        season = ESeason.Autumn;
                    }

                    if (summer < 33)
                    {
                        summer = summerTemp.Next(33, 43);
                        HUD.temperature = summer;
                    }

                    break;
                case ESeason.Spring:
                    Console.WriteLine("Spring");

                    if (spring < 25)
                    {
                        spring = springTemp.Next(25, 35);
                        HUD.temperature = spring;
                    }

                    break;
                case ESeason.Autumn:
                    Console.WriteLine("Autumn");

                    if (autumn < 24)
                    {
                        autumn = autumnTemp.Next(24, 34);
                        HUD.temperature = autumn;
                    }

                    break;
                case ESeason.Winter:
                    Console.WriteLine("Winter");        // never too cold for icecream

                    if (winter < 16)
                    {
                        winter = winterTemp.Next(16, 26);
                        HUD.temperature = winter;
                    }

                    break;
            }
        }
    }
}
