using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace IceCreamTycoon
{
    class GSSplash : GameState
    {
        #region Variables 
        // Loading In Sprites
        Sprite mainSplash;
        Sprite tempLoad;
        Sprite black;
        Sprite white;

        // Timers
        float timer = 0f;
        float deltaTime;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public GSSplash()
        {
            // White Background
            white = new Sprite(Content.Load<Texture2D>("white"), new Vector2(500, 300), 1f);

            // Panda Logo
            mainSplash = new Sprite(Content.Load<Texture2D>("Splash"), new Vector2(500, 300), 0.6f);
            mainSplash.alpha = 0f;

            tempLoad = new Sprite(Content.Load<Texture2D>("splashtest"), new Vector2(500, 300), 1f);
            tempLoad.alpha = 0f;
        }

        /// <summary>
        /// Update Function
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Updateing Timer
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += deltaTime;

            // mainSplash fade
            if (timer >= 2 && timer <= 6 && mainSplash.alpha < 1)
                mainSplash.alpha += 0.005f;
            if (timer >= 8 && timer <= 12 && mainSplash.alpha > 0)
                mainSplash.alpha -= 0.01f;

            // loading screen fade in
            if (timer >= 12 && timer <= 16 && tempLoad.alpha < 1)
                tempLoad.alpha += 0.005f;
         
            // go to menu
            if (timer >= 24)
                GameStateManager.SwitchToMenu();

            DeveloperCommands();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw Function
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();           //<-------Bottom Layer------->\\

            white.Draw(spriteBatch);
            mainSplash.Draw(spriteBatch);
            tempLoad.Draw(spriteBatch);

            spriteBatch.End();              //<-------Top Layer------->\\

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
