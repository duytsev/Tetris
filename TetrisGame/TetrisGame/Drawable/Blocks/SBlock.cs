using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisGame.Drawable.Blocks
{
    class SBlock : Block
    {
        public const int WIDTH = 3;
        public const int HEIGHT = 2;
        private Texture2D brickTexture;

        public SBlock(int x, int y, Texture2D brickTexture)
            : base(x, y, WIDTH, HEIGHT)
        {
            this.brickTexture = brickTexture;
            bricks = new List<Brick>();
            bricks.Add(new Brick(x, y, brickTexture));
            bricks.Add(new Brick(x + 1, y, brickTexture, true));
            bricks.Add(new Brick(x, y + 1, brickTexture));
            bricks.Add(new Brick(x - 1, y + 1, brickTexture));
        }
    }
}
