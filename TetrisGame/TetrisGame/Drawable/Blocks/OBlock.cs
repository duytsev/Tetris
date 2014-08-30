using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisGame.Drawable.Blocks
{
    class OBlock : Block
    {
        public const int WIDTH = 2;
        public const int HEIGHT = 2;
        private Texture2D brickTexture;

        public OBlock(int x, int y, Texture2D brickTexture)
            : base(x, y, WIDTH, HEIGHT)
        {
            this.brickTexture = brickTexture;
            bricks = new List<Brick>();
            bricks.Add(new Brick(x, y, brickTexture));
            bricks.Add(new Brick(x, y + 1, brickTexture));
            bricks.Add(new Brick(x + 1, y, brickTexture));
            bricks.Add(new Brick(x + 1, y + 1, brickTexture));
        }

        public override void rotateClockwise()
        {
            //do nothing
        }

        public override void rotateCounterClockwise()
        {
            //do nothing
        }
    }
}
