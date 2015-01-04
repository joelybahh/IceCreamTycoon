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
    class Stand : Sprite
    {
        public Stand(Texture2D texture, Vector2 position, float scale)
            : base(texture, position, scale)
        {
            this.texture = texture;
            this.scale = scale;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            if (position.X > 100)
            {
                scale = 2;
            } 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
