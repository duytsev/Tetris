using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TetrisGame.Drawable.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TetrisGame.Main;
using TetrisGame.Blocks;

namespace TetrisGame.Main
{
    class Grid
    {
        private Brick[][] bricks;
        private Block activeBlock;
        private Block traceBlock;
        private BlockGenerator generator;
        private GameState gameState;
        private SongPlayer player;
        private float fallTimer = 0;
        private float sideMoveTimer = 0;
        private float accTimer = 0;
        private int maxY;
        private KeyboardState oldState;
        public const int WIDTH = 10;
        public const int HEIGHT = 22;
        public float fallUpdateInterval { get; set; }
        public float sideMoveUpdateInterval { get; set; }
        public int score { get; set; }
        public int scoreCounter;
        public int level { get; set; }

        public Grid(BlockGenerator generator, GameState gameState, SongPlayer player) 
        {
            this.generator = generator;
            this.gameState = gameState;
            this.player = player;
            this.oldState = Keyboard.GetState();
            fallUpdateInterval = 0.7f;
            sideMoveUpdateInterval = 0.053f;
            reset();
        }

        public void reset()
        {
            addNewBlock();
            bricks = new Brick[HEIGHT ][];
            for (int i = 0; i < HEIGHT; i++)
            {
                bricks[i] = new Brick[10];
            }
            maxY = HEIGHT - 1;
            score = 0;
            scoreCounter = 0;
            level = 1;
            calculateTrace();
        }



        public void update(GameTime gameTime)
        {
            if (!GameState.PAUSED.Equals(gameState.state)
                && !GameState.GAME_OVER.Equals(gameState.state))
            {
                if (checkHeight())
                {
                    gameState.state = GameState.GAME_OVER;
                    player.gameOver();
                }
                if (checkCollisions())
                {
                    player.land();
                    foreach (Brick brick in activeBlock.bricks)
                    {
                        bricks[brick.y][brick.x] = new Brick(brick.x, brick.y, brick.texture);
                    }
                    if (activeBlock.y < maxY)
                    {
                        maxY = activeBlock.y;
                    }
                    addNewBlock();
                    deleteFullRows();
                }
                fallTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (fallTimer >= fallUpdateInterval)
                {
                    moveDownBlock();
                    fallTimer = 0;
                }
                calculateTrace();
            }
        }

        public void handleInput(KeyboardState newState, GameTime gameTime)
        {
            if (GameState.PLAYING.Equals(gameState.state))
            {
                sideMoveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (newState.IsKeyDown(Keys.Left)
                    && sideMoveTimer >= sideMoveUpdateInterval)
                {
                    if (!checkLeftSideCollision())
                    {
                        this.moveLeftBlock();
                        sideMoveTimer = 0;
                    }
                }

                if (newState.IsKeyDown(Keys.Right)
                    && sideMoveTimer >= sideMoveUpdateInterval)
                {
                    if (!checkRightSideCollision())
                    {
                        this.moveRightBlock();
                        sideMoveTimer = 0;
                    }
                }

                accTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (newState.IsKeyDown(Keys.Down)
                    && accTimer >= 0.005f
                    && activeBlock.y > 0)
                {
                    this.moveDownBlock();
                    fallTimer = 0;
                }
            }

            if (newState.IsKeyDown(Keys.P) && !oldState.IsKeyDown(Keys.P))
            {
                if (GameState.PLAYING.Equals(gameState.state))
                {
                    gameState.state = GameState.PAUSED;
                }
                else if (GameState.PAUSED.Equals(gameState.state))
                {
                    gameState.state = GameState.PLAYING;
                }
            }
            if (newState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up))
            {
                if (GameState.PLAYING.Equals(gameState.state))
                {
                    bool isRotated = true;
                    activeBlock.rotateClockwise();
                    foreach (Brick[] brickRow in bricks)
                    {
                        if (activeBlock.checkIntersection(brickRow))
                        {
                            activeBlock.rotateCounterClockwise();
                            isRotated = false;
                            break;
                        }
                    }
                    if (isRotated)
                    {
                        player.rotate();
                    }
                }
            }
            oldState = newState;
        }

        public void draw(SpriteBatch sb)
        {
            activeBlock.draw(sb);
            traceBlock.draw(sb, 0.2f);
            foreach (Brick[] brickRow in bricks)
            {
                foreach (Brick brick in brickRow)
                {
                    if (brick != null)
                    {
                        brick.draw(sb);
                    }
                }
            }

        }

        public void addNewBlock()
        {
            activeBlock = generator.generateBlock();
        }

        private void calculateTrace()
        {
            traceBlock = new Block();
            activeBlock.copy(traceBlock);
            while (!checkTraceCollisions())
            {
                traceBlock.moveDown();
            }
        }

        //algorithm can be improved
        private void deleteFullRows()
        {
            List<int> fullRows = getFullRows();
            if (fullRows.Count != 0)
            {
                incScoreAndLevel(fullRows.Count);
                foreach (int i in fullRows)
                {
                    bricks[i] = new Brick[10];
                }

                for (int i = 0; i < fullRows.Count; i++)
                {
                    for (int j = fullRows[i] - 1; j >= maxY; j--)
                    {
                        moveDownRowBricks(bricks[j]);
                    }
                }
                Brick[][] tmp = new Brick[HEIGHT][];
                Array.Copy(bricks, 0, tmp, fullRows.Count, fullRows.First());
                int offset = fullRows.First() + fullRows.Count();
                Array.Copy(bricks, offset, tmp, offset, HEIGHT - offset);
                for (int i = 0; i < fullRows.Count(); i++)
                {
                    tmp[i] = new Brick[10];
                }
                bricks = tmp;
                maxY += fullRows.Count();
            }
        }

        private void moveDownRowBricks(Brick[] brickRow)
        {
            foreach (Brick brick in brickRow)
            {
                if (brick != null)
                {
                    brick.moveDown();
                }
            }
        }

        //returns the list with full row numbers
        private List<int> getFullRows()
        {
            List<int> rows = new List<int>();
            for (int i = 0; i < bricks.Length; i++)
            {
                int count = 0;
                foreach (Brick brick in bricks[i])
                {
                    if (brick != null)
                    {
                        count++;
                    }
                }
                if (count == 10)
                {
                    rows.Add(i);
                }
            }
            return rows;
        }

        private void incScoreAndLevel(int rowCount)
        {
            int addScore = 100 * rowCount;
            scoreCounter += addScore;
            score += addScore;
            if (scoreCounter >= 300)
            {
                level++;
                fallUpdateInterval -= 0.05f;
                player.levelUp();
                scoreCounter = 0;
            }
            else
            {
                player.fullRow();
            }
        }

        private bool checkHeight()
        {
            return maxY <= 0;
        }

        private bool checkCollisions()
        {
            return (activeBlock.checkVerticalBound() || checkSleepingBlocksCollision(activeBlock));
        }

        private bool checkTraceCollisions()
        {
            return (traceBlock.checkVerticalBound() || checkSleepingBlocksCollision(traceBlock));
        }


        private bool checkSleepingBlocksCollision(Block block)
        {
            foreach (Brick[] brickRow in bricks)
            {
                if (block.checkVerticalCollision(brickRow))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkLeftSideCollision()
        {
            if (activeBlock.checkLeftSideBound())
            {
                return true;
            }

            foreach (Brick[] brickRow in bricks)
            {
                if (activeBlock.checkLeftSideContact(brickRow))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkRightSideCollision()
        {
            if (activeBlock.checkRightSideBound())
            {
                return true;
            }

            foreach (Brick[] brickRow in bricks)
            {
                if (activeBlock.checkRightSideContact(brickRow))
                {
                    return true;
                }
            }

            return false;
        }

        private void moveDownBlock()
        {
            activeBlock.moveDown();
        }

        private void moveLeftBlock()
        {
            activeBlock.moveLeft();
        }

        private void moveRightBlock()
        {
            activeBlock.moveRight();
        }
    }
}
