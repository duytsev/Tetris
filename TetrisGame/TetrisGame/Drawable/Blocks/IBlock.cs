using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisGame.Main;

namespace TetrisGame.Drawable.Blocks
{
    class IBlock : Block
    {
        public const int WIDTH = 1;
        public const int HEIGHT = 4;
        private Texture2D brickTexture;

        public IBlock() { bricks = new List<Brick>(); }

        public IBlock(int x, int y, Texture2D brickTexture)
            : base(x, y, WIDTH, HEIGHT)
        {
            this.brickTexture = brickTexture;
            bricks = new List<Brick>();
            bricks.Add(new Brick(x, y, brickTexture));
            bricks.Add(new Brick(x, y + 1, brickTexture));
            bricks.Add(new Brick(x, y + 2, brickTexture, true));
            bricks.Add(new Brick(x, y + 3, brickTexture));
        }

    }
}
