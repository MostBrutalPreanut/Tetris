using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Board
    {
        public int height, width;
        public Block[,] Grid;
        public int score;
        public Tetromino nextTetromino;

        public Board (int height, int width)
        {
            this.height = height;
            this.width = width;
            Grid = new Block[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Grid[i, j] = new Block();
                    Grid[i, j].x = j;
                    Grid[i, j].y = i;
                }
            }
        }
        
        //draws current board.
        public void DrawBoard()
        {
            Console.Clear();
            smallHeader();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0)
                    {
                        if(j==0)
                        Console.Write("╔");
                        else if (j == width - 1)
                        Console.WriteLine("╦══════════════════╗");
                        else Console.Write("══");
                    }
                    else if (i == height - 1)
                    {
                        if (j == 0)
                            Console.Write("╚");
                        else if (j == width - 1)
                            Console.WriteLine("╝");
                        else Console.Write("══");
                    }
                    else if (i == 1)
                    {
                        if (j == 0)
                            Console.Write("║");
                        else if (j == width - 1)
                        {
                            if (score == 0)
                                Console.WriteLine("║  Score: {0}        ║", score);
                            else if (score >= 10 && score <100)
                                Console.WriteLine("║  Score: {0}       ║", score);
                            else
                                Console.WriteLine("║  Score: {0}     ║", score);
                        }
                        else
                            Grid[i, j].PrintBlock();
                    }
                    else if (i == 2)
                    {
                        if (j == 0)
                            Console.Write("║");
                        else if (j == width - 1)
                            Console.WriteLine("║  Next Tetromino: ║");
                        else if (Grid[i,j].color == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("--");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                            Grid[i, j].PrintBlock();
                    }
                    else if (i == 3)
                    {
                        if (j == 0)
                            Console.Write("║");
                        else if (j == width - 1)
                        {
                            Console.Write("║  ");
                                for (int k = 0; k < nextTetromino.thisTetromino.GetLength(1); k++)
                                {
                                    if(nextTetromino.thisTetromino[0, k]!=null)
                                       nextTetromino.thisTetromino[0, k].PrintBlock();
                                    else Console.Write("  ");
                                }
                                if(nextTetromino.thisTetromino.GetLength(1) == 2)
                                Console.Write("            ║");
                                else if (nextTetromino.thisTetromino.GetLength(1) == 3)
                                Console.Write("          ║");
                                else if (nextTetromino.thisTetromino.GetLength(1) == 4)
                                Console.Write("        ║");
                            Console.WriteLine();
                        }
                        else
                            Grid[i, j].PrintBlock();
                    }
                    else if (i == 4)
                    {
                        if (j == 0)
                            Console.Write("║");
                        else if (j == width - 1)
                        {
                            Console.Write("║  ");
                            if (nextTetromino.thisTetromino.GetLength(0) > 1)
                            {
                                for (int k = 0; k < nextTetromino.thisTetromino.GetLength(1); k++)
                                {
                                    if (nextTetromino.thisTetromino[1, k] != null)
                                        nextTetromino.thisTetromino[1, k].PrintBlock();
                                    else Console.Write("  ");
                                }
                            }
                            if (nextTetromino.thisTetromino.GetLength(1) == 2)
                                Console.Write("            ║");
                            else if (nextTetromino.thisTetromino.GetLength(1) == 3)
                                Console.Write("          ║");
                            else if (nextTetromino.thisTetromino.GetLength(1) == 4)
                                Console.Write("                ║");
                            Console.WriteLine();
                        }
                        else
                            Grid[i, j].PrintBlock();
                    }
                    else if (i == 5)
                    {
                        if (j == 0)
                            Console.Write("║");
                        else if (j == width - 1)
                        {
                            Console.WriteLine("║  Difficulty: {0}   ║", score / 20);
                        }
                        else
                            Grid[i, j].PrintBlock();
                    }
                    else if (i == 6)
                    {
                        if (j == 0)
                            Console.Write("║");
                        else if (j == width - 1)
                        {
                            Console.WriteLine("╠══════════════════╝");
                        }
                        else
                            Grid[i, j].PrintBlock();
                    }
                    else
                    {
                        if (j == 0)
                            Console.Write("║");
                        else if (j == width - 1)
                            Console.WriteLine("║");
                        else 
                            Grid[i, j].PrintBlock();
                    }
                }
            }
        }

        void smallHeader()
        {
            Console.Write("╔╦╗╔═╗╔╦╗╦═╗╦╔═╗\n ║ ║╣  ║ ╠╦╝║╚═╗\n ╩ ╚═╝ ╩ ╩╚═╩╚═╝");
            Console.WriteLine();
        }
    }


}
