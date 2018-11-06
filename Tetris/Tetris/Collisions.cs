using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Collisions
    {
        public static bool dropCollisionCheck(Tetromino currTetronimo, Board board)
        {
            for (int i = currTetronimo.thisTetromino.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < currTetronimo.thisTetromino.GetLength(1); j++)
                {
                    if ((currTetronimo.thisTetromino[i, j] != null && board.Grid[currTetronimo.thisTetromino[i, j].y + 1, currTetronimo.thisTetromino[i, j].x].color != 0 && !board.Grid[currTetronimo.thisTetromino[i, j].y + 1, currTetronimo.thisTetromino[i, j].x].isCurrent)
                        || currTetronimo.y + currTetronimo.thisTetromino.GetLength(0) == board.Grid.GetLength(0) - 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool sideCollisionCheck(int side, Tetromino currTetronimo, Board board)
        {
            if (side == 0)
            {
                for (int i = currTetronimo.thisTetromino.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < currTetronimo.thisTetromino.GetLength(1); j++)
                    {
                        if ((currTetronimo.thisTetromino[i, j] != null && board.Grid[currTetronimo.thisTetromino[i, j].y, currTetronimo.thisTetromino[i, j].x - 1].color != 0 && !board.Grid[currTetronimo.thisTetromino[i, j].y, currTetronimo.thisTetromino[i, j].x - 1].isCurrent)
                            || currTetronimo.y + currTetronimo.thisTetromino.GetLength(0) == board.Grid.GetLength(0) - 1)
                        {
                            return true;
                        }
                    }
                }
            }
            if (side == 1)
            {
                for (int i = currTetronimo.thisTetromino.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < currTetronimo.thisTetromino.GetLength(1); j++)
                    {
                        if ((currTetronimo.thisTetromino[i, j] != null && board.Grid[currTetronimo.thisTetromino[i, j].y, currTetronimo.thisTetromino[i, j].x + 1].color != 0 && !board.Grid[currTetronimo.thisTetromino[i, j].y, currTetronimo.thisTetromino[i, j].x + 1].isCurrent)
                            || currTetronimo.y + currTetronimo.thisTetromino.GetLength(0) == board.Grid.GetLength(0) - 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool rotateCollisionCheck(Tetromino currTetronimo, Board board)
        {
            for (int i = 0; i < currTetronimo.thisTetromino.GetLength(0); i++)
            {
                if ((currTetronimo.thisTetromino[i, currTetronimo.thisTetromino.GetLength(1) - 1] != null
                    && (board.Grid[currTetronimo.thisTetromino[i, currTetronimo.thisTetromino.GetLength(1) - 1].y, currTetronimo.thisTetromino[i, currTetronimo.thisTetromino.GetLength(1) - 1].x + 1].color != 0
                    || (board.Grid[currTetronimo.thisTetromino[i, currTetronimo.thisTetromino.GetLength(1) - 1].y, currTetronimo.thisTetromino[i, currTetronimo.thisTetromino.GetLength(1) - 1].x + 1].x > board.Grid.GetLength(1) - 2))))
                {
                    return true;
                }

                if ((currTetronimo.thisTetromino[i, 0] != null
                    && board.Grid[currTetronimo.thisTetromino[i, 0].y, currTetronimo.thisTetromino[i, 0].x - 1].color != 0
                    && (board.Grid[currTetronimo.thisTetromino[i, 0].y, currTetronimo.thisTetromino[i, 0].x - 1].x > 1)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
