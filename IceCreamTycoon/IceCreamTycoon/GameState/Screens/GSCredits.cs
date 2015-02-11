﻿using System;
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
    class GSCredits : GameState
    {

        public GSCredits()
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
        }

        public override void UnloadContent(ContentManager Content)
        {
            base.UnloadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            DeveloperCommands(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void DeveloperCommands(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                GameStateManager.SwitchToPlay(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.M))
                GameStateManager.SwitchToMenu();
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                GameStateManager.SwitchToSplash();
        }
    }
}