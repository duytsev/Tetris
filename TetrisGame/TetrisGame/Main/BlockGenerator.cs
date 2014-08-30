using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using TetrisGame.Drawable.Blocks;

namespace TetrisGame.Blocks
{
    //uses simple random algorithm, can be improved
    class BlockGenerator
    {
        private List<Texture2D> textures;
        private int lastColor;

        enum Shapes {I, L, J, O, S, Z, T }

        public BlockGenerator(List<Texture2D> textures)
        {
            this.textures = textures;
        }

        public Block generateBlock()
        {
            Texture2D texture = generateTexture();
            return generateShape(texture);
        }

        private Block generateShape(Texture2D texture)
        {
            Random rand = new Random();
            Block block = null;
            int shape = rand.Next(0, 7);
            switch (shape)
            {
                case (int)Shapes.I:
                    block = new IBlock(4, 0, texture);
                    break;
                case (int) Shapes.L:
                    block = new LBlock(4, 0, texture);
                    break;
                case (int) Shapes.J:
                    block = new JBlock(4, 0, texture);
                    break;
                case (int)Shapes.O:
                    block = new OBlock(4, 0, texture);
                    break;
                case (int)Shapes.S:
                    block = new SBlock(4, 0, texture);
                    break;
                case (int)Shapes.Z:
                    block = new ZBlock(4, 0, texture);
                    break;
                case (int)Shapes.T:
                    block = new TBlock(4, 0, texture);
                    break;
                default:
                    break;
            }
            return block;
        }

        private Texture2D generateTexture()
        {
            Random rand = new Random();
            int color = rand.Next(0, 5);
            while (color == lastColor)
            {
                color = rand.Next(0, 5);
            }
            lastColor = color;
            return textures[color];
        }
    }
}
