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
    class Text
    {
        #region Variables 

        public SpriteFont font;
        public string text;
        public Vector2 position;
        public Vector2 origin;
        public float rotation;
        public float scale;
        public Color color;
        public SpriteEffects spriteEffects = SpriteEffects.None;
        public float alpha;

        #endregion

        /// <summary>
        /// Base Text
        /// </summary>
        /// <param name="font">the font you want to use</param>
        /// <param name="text">the text you want to display</param>
        /// <param name="color">the color you want the text to be</param>
        /// <param name="position">the position you want the text to be at</param>
        /// <param name="scale">the scale of the text</param>
        public Text(SpriteFont font, string text, Color color, Vector2 position, float scale)
        {
            this.font = font;
            this.text = text;
            this.color = color;
            this.position = position;
            this.scale = scale;
            origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
        }

        /// <summary>
        /// Draw Function
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, spriteEffects, 1);
        }
    }
}
