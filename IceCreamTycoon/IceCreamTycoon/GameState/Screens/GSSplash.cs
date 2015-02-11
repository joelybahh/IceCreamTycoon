using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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

        bool hasPlayed = false;

        Video video;
        Texture2D videoTexture;
        VideoPlayer videoPlayer;
        float alpha = 1f;

        // Timers
        float timer = 0f;
        float deltaTime;
        float loadingTime = 20;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public GSSplash()
        {
            // White Background
            white = new Sprite(Content.Load<Texture2D>("white"), new Vector2(500, 300), 1f);

            video = Content.Load<Video>("SplashScreen1");
            videoPlayer = new VideoPlayer();

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

            PlayVideo();
            UpdateTransitions();

            DeveloperCommands(gameTime);

            base.Update(gameTime);
        }

        private void UpdateTransitions()
        {
            // Fade out video
            if (timer >= 4 && timer <= 7 && alpha > 0)
                alpha -= 0.005f;
            // Fade in loading screen
            if (timer >= 6 && timer <= 11 && tempLoad.alpha < 1)
                tempLoad.alpha += 0.01f;
            // go to menu
            if (timer >= loadingTime)
                GameStateManager.SwitchToMenu();
        }

        private void PlayVideo()
        {
            if (videoPlayer.State == MediaState.Stopped && hasPlayed == false)
            {
                hasPlayed = true;
                videoPlayer.IsLooped = false;
                videoPlayer.Play(video);
                videoTexture = videoPlayer.GetTexture();
                alpha = 1f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            videoTexture = videoPlayer.GetTexture();
            
            spriteBatch.Begin();           //<-------Bottom Layer------->\\

            white.Draw(spriteBatch);
            mainSplash.Draw(spriteBatch);
            spriteBatch.Draw(videoTexture, new Rectangle(0, 0, Game1.screenWidth, Game1.screenHeight), Color.White * alpha);
            tempLoad.Draw(spriteBatch);

            spriteBatch.End();              //<-------Top Layer------->\\

            base.Draw(spriteBatch);
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
