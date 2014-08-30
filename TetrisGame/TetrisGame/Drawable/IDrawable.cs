using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisGame.Drawable
{
    interface IDrawable
    {
        void draw(SpriteBatch spriteBatch);

        void draw(SpriteBatch spriteBatch, float opacity);
    }
}
