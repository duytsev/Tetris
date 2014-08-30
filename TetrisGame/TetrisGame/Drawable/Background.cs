using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisGame.Drawable
{
    class Background : IDrawable
    {
        private Rectangle rect = new Rectangle(0, 0, 340, 748);
        public Texture2D texture { get; set; }

        public Background() { }

        public Background(Texture2D texture)
        {
            this.texture = texture;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }

        public void draw(SpriteBatch spriteBatch, float opacity)
        {
            spriteBatch.Draw(texture, rect, Color.White * opacity);
        }
    }
}
