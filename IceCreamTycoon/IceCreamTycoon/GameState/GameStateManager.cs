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
    class GameStateManager
    {
        static GameState currentGameState;
        public static IServiceProvider Services;

        /// <summary>
        /// Load Content Function
        /// </summary>
        /// <param name="Content"></param>
        public static void LoadContent(ContentManager Content)
        {
            if (currentGameState != null)
                currentGameState.LoadContent(Content);
        }

        /// <summary>
        /// Unload Content Function
        /// </summary>
        /// <param name="Content"></param>
        public static void UnloadContent(ContentManager Content)
        {
            if (currentGameState != null)
                currentGameState.UnloadContent(Content);
        }

        /// <summary>
        /// Update Function
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            if (currentGameState != null)
                currentGameState.Update(gameTime);
        }

        /// <summary>
        /// Draw Function
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (currentGameState != null)
                currentGameState.Draw(spriteBatch);
        }

        // ------------------------------------------------------------------------
        public static void SwitchToSplash()         // Static functions for       |
        {
            currentGameState = new GSSplash();
        } //
        public static void SwitchToMenu()           // switching between          |
        {
            currentGameState = new GSMenu();
        } //
        public static void SwitchToPlay()           // all the screens            |
        {
            currentGameState = new GSPlay();
        } //
        public static void SwitchToOptions()        // simply add a static        |
        {
            currentGameState = new GSOptions();
        } //
        public static void SwitchToControls()       // function underneath the    |
        {
            currentGameState = new GSControls();
        } //
        public static void SwitchToSound()          // bottom one and set its     |
        {
            currentGameState = new GSSound();
        } //
        public static void SwitchToPause()          // currentState so the new    |
        {
            currentGameState = new GSPause();
        } //
        public static void SwitchToCredits()        // screen you want to add.    |
        {
            currentGameState = new GSControls();
        } //
        // ------------------------------------------------------------------------
    }
}
