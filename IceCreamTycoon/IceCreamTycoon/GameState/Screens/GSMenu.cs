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
    class GSMenu : GameState
    {
        #region Variables

        // Static Sprites
        Sprite tempMenuBG;

        //fonts
        SpriteFont font;

        // Vectors
        Vector2 playPos;
        Vector2 optionsPos;
        Vector2 creditsPos;
        Vector2 quitPos;

        // strings
        string playText = "Play";
        string optionsText = "Options";
        string creditsText = "Credits";
        string quitText = "Quit";

        // Colours
        Color playColor = Color.Black;
        Color optionsColor = Color.Black;
        Color creditsColor = Color.Black;
        Color quitColor = Color.Black;

        // Timers
        float timer = 0f;
        float deltaTime;
        float rotation;
        float playScale;
        float optionsScale;
        float creditsScale;
        float quitScale;

        //Integers
        int origin;

        // Booleans
        bool isHoveringPlay;
        bool isHoveringOptions;
        bool isHoveringCredits;
        bool isHoveringQuit;
        bool transitioning;

        // Controllers
        MouseState mouse;
        KeyboardState keyboard;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public GSMenu()
        {
            tempMenuBG = new Sprite(Content.Load<Texture2D>("menutest"), new Vector2(500, 300), 1f);
            tempMenuBG.alpha = 1f;

            font = Content.Load<SpriteFont>("MenuFont");

            playScale = 1f;
            optionsScale = 1f;
            creditsScale = 1f;
            quitScale = 1f;

            playPos = new Vector2(500 - (Origin(playText)), 100);
            optionsPos = new Vector2(500 - (Origin(optionsText)), 200);
            creditsPos = new Vector2(500 - (Origin(creditsText)), 300);
            quitPos = new Vector2(500 - (Origin(quitText)), 400);
        }

        /// <summary>
        /// Update Function
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            playPos = new Vector2(500 - (Origin(playText)), 100);
            optionsPos = new Vector2(500 - (Origin(optionsText)), 200);
            creditsPos = new Vector2(500 - (Origin(creditsText)), 300);
            quitPos = new Vector2(500 - (Origin(quitText)), 400);

            Console.WriteLine("X: " + mouse.X + " Y: " + mouse.Y);

            CheckIfHovering();
            UpdateSelection(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Updating Selection
        /// </summary>
        private void UpdateSelection(GameTime gameTime)
        {
            DeveloperCommands(gameTime);

            #region PlayButton

            // ----------------------------Play hover
            if (isHoveringPlay)
            {
                playColor = Color.Red;                              //-------------------------------
                if (playScale < 1.1)                                // If The scale of the text is  |
                    playScale += 0.004f;                            // less than 1.1, increase it.  |
                //                              |                                |
                if (mouse.LeftButton == ButtonState.Pressed)        // Checking to see if           |
                {                                                   // you have left clicked,       |
                    GameStateManager.SwitchToPlay(gameTime);                // if so, switch to playScreen. |                                              
                }                                                   //-------------------------------

            }
            else
            {
                playColor = Color.Black;
                if (playScale > 1)                                  // If scale of the text is
                    playScale -= 0.004f;                            // greater than 1, decrease it.
            }

            #endregion

            #region OptionsButton

            // -----------------------------Options Hover
            if (isHoveringOptions)
            {
                optionsColor = Color.Red;
                if (optionsScale < 1.1)
                    optionsScale += 0.004f;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    GameStateManager.SwitchToOptions();
                } 
            }
            else
            {
                optionsColor = Color.Black;
                if (optionsScale > 1)
                    optionsScale -= 0.004f;
            }

            #endregion

            #region CreditsButton

            // -----------------------------Credits Hover
            if (isHoveringCredits)
            {
                creditsColor = Color.Red;
                if (creditsScale < 1.1)
                    creditsScale += 0.004f;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    GameStateManager.SwitchToCredits();
                }
            }
            else
            {
                creditsColor = Color.Black;
                if (creditsScale > 1)
                    creditsScale -= 0.004f;
            }

            #endregion

            #region QuitButton

            // -----------------------------Quit Hover
            if (isHoveringQuit)
            {
                quitColor = Color.Red;
                if (quitScale < 1.1)
                    quitScale += 0.004f;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Game1.exitGame = true;
                }
            }
            else
            {
                quitColor = Color.Black;
                if (quitScale > 1)
                    quitScale -= 0.004f;
            }

            #endregion
        }

        /// <summary>
        /// Checking to see whether or not your hovering over a selection
        /// </summary>
        private void CheckIfHovering()
        {
            #region HoveringPlay

            if (mouse.X >= 443 && mouse.X <= 560 && mouse.Y >= 107 && mouse.Y <= 170)
            {
                isHoveringPlay = true;
            }
            else
            {
                isHoveringPlay = false;
            }

            #endregion

            #region HoveringOptions

            if (mouse.X >= 397 && mouse.X <= 600 && mouse.Y >= 207 && mouse.Y <= 270)
            {
                isHoveringOptions = true;
            }
            else
            {
                isHoveringOptions = false;
            }

            #endregion

            #region HoveringCredits

            if (mouse.X >= 408 && mouse.X <= 592 && mouse.Y >= 307 && mouse.Y <= 370)
            {
                isHoveringCredits = true;
            }
            else
            {
                isHoveringCredits = false;
            }

            #endregion

            #region HoveringQuit

            if (mouse.X >= 442 && mouse.X <= 557 && mouse.Y >= 407 && mouse.Y <= 470)
            {
                isHoveringQuit = true;
            }
            else
            {
                isHoveringQuit = false;
            }

            #endregion
        }

        /// <summary>
        /// Draw Function
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();           //<-------Bottom Layer------->\\

            tempMenuBG.Draw(spriteBatch);
            spriteBatch.DrawString(font, playText, playPos, playColor, rotation, Vector2.Zero, playScale, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, optionsText, optionsPos, optionsColor, rotation, Vector2.Zero, optionsScale, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, creditsText, creditsPos, creditsColor, rotation, Vector2.Zero, creditsScale, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, quitText, quitPos, quitColor, rotation, Vector2.Zero, quitScale, SpriteEffects.None, 1);

            spriteBatch.End();              //<-------Top Layer------->\\
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Gets the origin of a string
        /// </summary>
        /// <param name="text">string you want to get the origin for</param>
        /// <returns>returns the origin</returns>
        private int Origin(string text)
        {
            origin = (int)font.MeasureString(text).X / 2;
            return origin;
        }

        private static void DeveloperCommands(GameTime gameTime)
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
