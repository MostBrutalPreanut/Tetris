using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tetromino : Block
    {
        public Block[,] thisTetromino;

        public static Block[,] I = new Block[1, 4] { { new Block(), new Block(), new Block(), new Block() } };
        public static Block[,] O = new Block[2, 2] { { new Block(), new Block() }, { new Block(), new Block() } };
        public static Block[,] T = new Block[2, 3] { { null, new Block(), null }, { new Block(), new Block(), new Block() } };
        public static Block[,] S = new Block[2, 3] { { null, new Block(), new Block() }, { new Block(), new Block(), null } };
        public static Block[,] Z = new Block[2, 3] { { new Block(), new Block(), null }, { null, new Block(), new Block() } };
        public static Block[,] J = new Block[2, 3] { { new Block(), null, null }, { new Block(), new Block(), new Block() } };
        public static Block[,] L = new Block[2, 3] { { null, null, new Block() }, { new Block(), new Block(), new Block() } };

        public Tetromino (int prevColor)
        {
            Random tetrominoNum = new Random();
            bool ck = false;
            int tempColor = 0;

            switch (tetrominoNum.Next(1, 8))
            {
                case 1:
                    thisTetromino = I;
                    break;
                case 2:
                    thisTetromino = O;
                    break;
                case 3:
                    thisTetromino = T;
                    break;
                case 4:
                    thisTetromino = S;
                    break;
                case 5:
                    thisTetromino = Z;
                    break;
                case 6:
                    thisTetromino = J;
                    break;
                case 7:
                    thisTetromino = L;
                    break;
            }

            while (!ck)
            {
                tempColor = tetrominoNum.Next(1, 9);
                if (tempColor != prevColor)
                {
                    ck = true;
                }
            }
            for (int i = 0; i < thisTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < thisTetromino.GetLength(1); j++)
                {
                    if (thisTetromino[i,j] != null)
                    thisTetromino[i,j].color = tempColor;
                }
            }
        }

        public Tetromino(Tetromino tet)
        {
            int y = tet.thisTetromino.GetLength(1);
            int x = tet.thisTetromino.GetLength(0);
            int color = tet.color;
            this.thisTetromino = new Block[y, x];
            this.x = tet.x;
            this.y = tet.y;
            this.color = tet.color;
            for (int i = 0; i < thisTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < thisTetromino.GetLength(1); j++)
                {
                    if (thisTetromino[i, j] != null)
                        thisTetromino[i, j].color = color;
                }
            }
        }

        public static void Spawn(Tetromino currTetromino, Board board)
        {
            for (int i = 0; i < currTetromino.thisTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currTetromino.thisTetromino.GetLength(1); j++)
                {
                    if (currTetromino.thisTetromino[i, j] != null)
                    {
                        board.Grid[1 + i, (board.width / 2) - (currTetromino.thisTetromino.GetLength(0) / 2) + j].color = currTetromino.thisTetromino[i, j].color;
                        board.Grid[1 + i, (board.width / 2) - (currTetromino.thisTetromino.GetLength(0) / 2) + j].isCurrent = true;
                        currTetromino.thisTetromino[i, j].x = (board.width / 2) - (currTetromino.thisTetromino.GetLength(0) / 2) + j;
                        currTetromino.thisTetromino[i, j].y = 1 + i;
                    }
                }
            }
            currTetromino.x = (board.width / 2) - (currTetromino.thisTetromino.GetLength(0) / 2);
            currTetromino.y = 1;
        }

    }
}
