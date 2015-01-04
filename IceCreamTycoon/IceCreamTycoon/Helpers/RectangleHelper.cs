using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IceCreamTycoon.Helpers
{
    static class RectangleHelper
    {
        /// <summary>
        /// Checks to see if you are touching the Top Of something
        /// </summary>
        /// <param name="r1">The Main Rectangle</param>
        /// <param name="r2">The Rectangle you're colliding with</param>
        /// <returns>Returns a calculation that checks if it is touching the top of the rectangle</returns>
        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 1 &&
                    r1.Bottom <= r2.Top + (r2.Height / 2) &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5));
        }

        /// <summary>
        /// Checks to see if you are touching the Bottom Of something
        /// </summary>
        /// <param name="r1">The Main Rectangle</param>
        /// <param name="r2">The Rectangle you're colliding with</param>
        /// <returns>Returns a calculation that checks if it is touching the top of the rectangle</returns>
        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 5) &&
                    r1.Top >= r2.Bottom + 1 &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5));
        }

        /// <summary>
        /// Checks to see if you are touching the Left Of something
        /// </summary>
        /// <param name="r1">The Main Rectangle</param>
        /// <param name="r2">The Rectangle you're colliding with</param>
        /// <returns>Returns a calculation that checks if it is touching the top of the rectangle</returns>
        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                    r1.Right >= r2.Left - 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        /// <summary>
        /// Checks to see if you are touching the Right Of something
        /// </summary>
        /// <param name="r1">The Main Rectangle</param>
        /// <param name="r2">The Rectangle you're colliding with</param>
        /// <returns>Returns a calculation that checks if it is touching the top of the rectangle</returns>
        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left &&
                    r1.Left <= r2.Right + 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }
    }
}
