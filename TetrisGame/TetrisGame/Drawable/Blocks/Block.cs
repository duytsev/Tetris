using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisGame.Main;

namespace TetrisGame.Drawable.Blocks
{
    class Block : IDrawable
    {
        //all values are in grid coords, not in absolute
        public int x { get; set; }
        public int y { get; set; }
        public int pivotX { get; set; }
        public int pivotY { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public List<Brick> bricks { get; set; }

        public Block() { bricks = new List<Brick>(); }

        public Block(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void draw(SpriteBatch sb)
        {
            foreach (Brick brick in bricks)
            {
                brick.draw(sb);
            }
        }

        public void draw(SpriteBatch sb, float opacity)
        {
            foreach (Brick brick in bricks)
            {
                brick.draw(sb, opacity);
            }
        }

        public void copy(Block anotherBlock)
        {
            anotherBlock.y = this.y;
            anotherBlock.x = this.x;
            bricks.ForEach((item) => 
                anotherBlock.bricks.Add(new Brick(item)));
        }

        public virtual void rotateClockwise()
        {
            int[] coords = getPivotCoords();
            int pX = coords[0];
            int pY = coords[1];
            foreach (Brick brick in bricks)
            {
                if (!brick.isPivot)
                {
                    int vRotX = brick.x - pX;
                    int vRotY = brick.y - pY;

                    brick.x = -vRotY + pX;
                    brick.y = vRotX + pY;
                    if (brick.y < this.y)
                    {
                        this.y = brick.y;
                    }
                }
            }
        }

        public virtual void rotateCounterClockwise()
        {
            int[] coords = getPivotCoords();
            int pX = coords[0];
            int pY = coords[1];
            foreach (Brick brick in bricks)
            {
                if (!brick.isPivot)
                {
                    int vRotX = brick.x - pX;
                    int vRotY = brick.y - pY;

                    brick.x = vRotY + pX;
                    brick.y = -vRotX + pY;
                    this.y = brick.y;
                    if (brick.y < this.y)
                    {
                        this.y = brick.y;
                    }
                }
            }
        }

        private int[] getPivotCoords()
        {
            int[] coords = new int[2];
            foreach (Brick brick in bricks)
            {
                if (brick.isPivot)
                {
                    coords[0] = brick.x;
                    coords[1] = brick.y;
                }
            }
            return coords;
        }

        public bool checkIntersection(Brick[] brickRow)
        {
            foreach (Brick brick in bricks)
            {
                if (brick.x < 0 || brick.x > 9 || brick.y > Grid.HEIGHT - 1)
                {
                    return true;
                }
                Brick anotherBrick = brickRow[brick.x];
                if (anotherBrick != null && brick.checkIntersection(anotherBrick))
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkVerticalCollision(Brick[] brickRow)
        {
            foreach (Brick brick in bricks)
            {
                Brick anotherBrick = brickRow[brick.x];
                if (anotherBrick != null && brick.checkVerticalCollision(anotherBrick))
                {
                    return true;
                }

            }
            return false;
        }

        public bool checkLeftSideContact(Brick[] brickRow)
        {
            foreach (Brick brick in bricks)
            {
                foreach (Brick anotherBrick in brickRow)
                {
                    if (anotherBrick != null && brick.checkLeftSideContact(anotherBrick))
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public bool checkRightSideContact(Brick[] brickRow)
        {
            foreach (Brick brick in bricks)
            {
                foreach (Brick anotherBrick in brickRow)
                {
                    if (anotherBrick != null && brick.checkRightSideContact(anotherBrick)) 
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public bool checkLeftSideBound()
        {
            foreach (Brick brick in bricks)
            {
                if (brick.checkLeftSideBound())
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkRightSideBound()
        {
            foreach (Brick brick in bricks)
            {
                if (brick.checkRightSideBound())
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkVerticalBound()
        {
            foreach (Brick brick in bricks)
            {
                if (brick.checkVerticalBound())
                {
                    return true;
                }
            }
            return false;
        }

        public void moveDown()
        {
            this.y++;
            foreach (Brick brick in bricks)
            {
                brick.moveDown();
            }
        }

        public void moveLeft()
        {
            this.x--;
            foreach (Brick brick in bricks)
            {
                brick.moveLeft();
            }
        }

        public void moveRight()
        {
            this.x++;
            foreach (Brick brick in bricks)
            {
                brick.moveRight();
            }
        }

    }
}
