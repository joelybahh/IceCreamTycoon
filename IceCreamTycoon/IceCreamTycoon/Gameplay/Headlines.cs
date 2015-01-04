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
    public class Headlines
    {
        public string currMOTD;
        public Random random = new Random();

        public string GetMOTD(List<string> strings)
        {
            int motd = random.Next(strings.Count);

            currMOTD = strings[motd];

            return currMOTD;
        }
    }
}
