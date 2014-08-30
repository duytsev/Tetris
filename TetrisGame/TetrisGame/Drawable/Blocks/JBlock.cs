using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisGame.Drawable.Blocks
{
    class JBlock : Block
    {
        public const int WIDTH = 2;
        public const int HEIGHT = 3;
        private Texture2D brickTexture;

        public JBlock(int x, int y, Texture2D brickTexture)
            : base(x, y, WIDTH, HEIGHT)
        {
            this.brickTexture = brickTexture;
            bricks = new List<Brick>();
            bricks.Add(new Brick(x, y, brickTexture));
            bricks.Add(new Brick(x, y + 1, brickTexture, true));
            bricks.Add(new Brick(x, y + 2, brickTexture));
            bricks.Add(new Brick(x - 1, y + 2, brickTexture));
        }
    }
}
