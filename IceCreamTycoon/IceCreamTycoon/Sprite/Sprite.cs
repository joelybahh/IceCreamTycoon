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
    public class Sprite
    {
            #region Variables

        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotation;
        public float scale;
        public Rectangle crop;
        public Color color = Color.White;
        public SpriteEffects spriteEffects = SpriteEffects.None;
        public float alpha = 1f;

        #endregion

        /// <summary>
        /// Static Sprites
        /// </summary>
        /// <param name="texture">sprites texture</param>
        /// <param name="position">the position of the sprite</param>
        /// <param name="scale">the scale of the sprite</param>
        public Sprite(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            crop = texture.Bounds;
            origin = new Vector2(crop.Width / 2, crop.Height / 2);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draw Function
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, crop, color * alpha, rotation, origin, scale, spriteEffects, 1);
        }
    }
}
