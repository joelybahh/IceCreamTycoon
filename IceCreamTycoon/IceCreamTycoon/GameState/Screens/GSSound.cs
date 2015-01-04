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
    class GSSound : GameState 
    {

        public GSSound()
        {

        }

        public override void Update(GameTime gameTime)
        {
            DeveloperCommands();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private static void DeveloperCommands()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                GameStateManager.SwitchToPlay();
            if (Keyboard.GetState().IsKeyDown(Keys.M))
                GameStateManager.SwitchToMenu();
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                GameStateManager.SwitchToSplash();
        }
    }
}
