using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TetrisGame.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisGame.Drawable.Blocks
{

    class Brick : IDrawable
    {
        private readonly int SIZE = 33;
        public int x { get; set; }
        public int y { get; set; }
        public bool isPivot { get; set; }
        public Texture2D texture { get; set; }
        
        public const int WIDTH = 1;
        public const int HEIGHT = 1;

        public Brick(int x, int y, Texture2D texture)
        {
            this.x = x;
            this.y = y;
            this.texture = texture;
            this.isPivot = false;
        }

        public Brick(int x, int y, Texture2D texture, bool isPivot)
        {
            this.x = x;
            this.y = y;
            this.texture = texture;
            this.isPivot = isPivot;
        }

        //copy constructor
        public Brick(Brick brick)
        {
            this.x = brick.x;
            this.y = brick.y;
            this.isPivot = brick.isPivot;
            this.texture = brick.texture;
        }

        public void draw(SpriteBatch sb)
        {
            Rectangle drawRect = new Rectangle(x * SIZE + x * 1 + 1,
                (y) * SIZE + (y) * 1, SIZE, SIZE);
            sb.Draw(this.texture, drawRect, Color.White);
        }

        public void draw(SpriteBatch sb, float opacity)
        {
            Rectangle drawRect = new Rectangle(x * SIZE + x * 1 + 1,
                (y) * SIZE + (y) * 1, SIZE, SIZE);
            sb.Draw(this.texture, drawRect, Color.White * opacity);
        }

        public bool checkIntersection(Brick anotherBrick)
        {
            return (this.x == anotherBrick.x && this.y == anotherBrick.y);
        }

        public bool checkVerticalCollision(Brick anotherBrick)
        {
            return (this.y + 1 == anotherBrick.y && this.x == anotherBrick.x);
        }

        public bool checkLeftSideContact(Brick anotherBrick)
        {
            return (this.x - 1 == anotherBrick.x && this.y == anotherBrick.y);
        }

        public bool checkRightSideContact(Brick anotherBrick)
        {
            return (this.x + 1 == anotherBrick.x && this.y == anotherBrick.y);
        }

        public bool checkLeftSideBound()
        {
            return (this.x - 1 < 0 ); 
        }

        public bool checkRightSideBound()
        {
            return (this.x + 1 > Grid.WIDTH - 1);
        }

        public bool checkVerticalBound()
        {
            return (this.y + 1 > Grid.HEIGHT - 1);
        }

        public void moveDown()
        {
            this.y += 1;
        }

        public void moveLeft()
        {
            this.x -= 1;
        }

        public void moveRight()
        {
            this.x += 1;
        }
    }
}
