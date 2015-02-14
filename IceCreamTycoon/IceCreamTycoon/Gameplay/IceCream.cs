using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IceCreamTycoon
{
    public class IceCream
    {
        public enum IceCreamType
        {
            Vanilla,
            Chocolate,
            Strawberry,
            Caramel,
            Banana,
            Mango,
            Coffee
        };

        public IceCreamType iceCreamType { get; set; }

        public Texture2D texture { get; set; }

        public int numAvailable { get; set; }
        public string itemName { get; set; }

        public Color color = Color.White;

        private int maxQuantity = 99;

        public IceCream()
        {

        }

        public IceCream(string name, int numAvailable, Texture2D texture, IceCreamType type)
        {
            this.numAvailable = numAvailable;
            this.texture = texture;
            this.iceCreamType = type;
            this.itemName = name;
        }

        public void AddItems()
        {
            HUD.ChocolateIceCream++;
            CheckMaxQuantity();
        }

        public void AddItems(int numToAdd)
        {
            HUD.ChocolateIceCream += numToAdd;
            CheckMaxQuantity();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, color);
        }

        private void CheckMaxQuantity()
        {
            if (HUD.ChocolateIceCream > maxQuantity)
            {
                HUD.ChocolateIceCream = maxQuantity;
            }
        }
    }
}
