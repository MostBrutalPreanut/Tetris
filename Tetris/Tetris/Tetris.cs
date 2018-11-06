using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Tetris
{
    class Tetris
    {
        Board board;
        bool isPlaying = true;
        public Tetromino currTetromino, nextTetromino;
        int difficulty = 2;
        
        Stopwatch deltaTime = new Stopwatch();
        public float dropRate;

        //generate currblock and next block. Call this at the start and everytime a collision happens

        public void start()
        {
            Menu m = new Menu();
            m.MainMenu();
        }

        public void Update()
        {
            if (Collisions.dropCollisionCheck(currTetromino, board))
            {
                deltaTime.Reset();
                checkRows();
                GenerateTetrominos();
                Tetromino.Spawn(currTetromino, board);
                deltaTime.Start();
            }
            if (deltaTime.ElapsedMilliseconds > dropRate)
            {
                Drop();
                deltaTime.Restart();
                board.DrawBoard();
            }
        }

        void input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    Move(0);
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    Move(1);
                }
                else if (key.Key == ConsoleKey.UpArrow && !Collisions.rotateCollisionCheck(currTetromino, board))
                {
                    Rotate(1);
                    board.DrawBoard();
                }
                else if (key.Key == ConsoleKey.DownArrow && !Collisions.rotateCollisionCheck(currTetromino, board))
                {
                    Rotate(0);
                    board.DrawBoard();
                }
                else if (key.Key == ConsoleKey.Spacebar && !Collisions.dropCollisionCheck(currTetromino, board))
                {
                    Drop();
                    board.DrawBoard();
                }
            }
        }

        //moves current tetromino (0 is left, 1 is right)
        public void Move(int side)
        {
            EraseTetronimo();
            if (side == 0 && currTetromino.x - 1 > 0 && !Collisions.sideCollisionCheck(0, currTetromino, board))
            {
                currTetromino.x--;
                for (int i = 0; i < currTetromino.thisTetromino.GetLength(0); i++)
                {
                    for (int j = 0; j < currTetromino.thisTetromino.GetLength(1); j++)
                    {
                        if (currTetromino.thisTetromino[i, j] != null)
                        {
                            board.Grid[currTetromino.y + i, currTetromino.x + j].x--;
                            currTetromino.thisTetromino[i, j].x--;
                        }
                    }
                }
                DrawTetronimo();
                board.DrawBoard();
            }

            if (side == 1 && currTetromino.x + currTetromino.thisTetromino.GetLength(1) + 1 < board.Grid.GetLength(1) && !Collisions.sideCollisionCheck(1, currTetromino, board))
            {
                currTetromino.x++;
                for (int i = 0; i < currTetromino.thisTetromino.GetLength(0); i++)
                {
                    for (int j = 0; j < currTetromino.thisTetromino.GetLength(1); j++)
                    {
                        if (currTetromino.thisTetromino[i, j] != null)
                        {
                            board.Grid[currTetromino.y + i, currTetromino.x + j].x++;
                            currTetromino.thisTetromino[i, j].x++;
                        }
                    }
                }
                DrawTetronimo();
                board.DrawBoard();
            }
        }

        //rotates (0 is clockwise, 1 is counterclockwise)
        public void Rotate(int side)
        {
            EraseTetronimo();
            Tetromino temp = new Tetromino(currTetromino);
            int newColumn, newRow = 0;
            if ( side == 0)
            {
                for (int oldColumn = currTetromino.thisTetromino.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
                {
                    newColumn = 0;
                    for (int oldRow = 0; oldRow < currTetromino.thisTetromino.GetLength(0); oldRow++)
                    {
                        temp.thisTetromino[newRow, newColumn] = currTetromino.thisTetromino[oldRow, oldColumn];
                        newColumn++;
                    }
                    newRow++;
                }
            }
            else
            {
                for (int oldColumn = 0; oldColumn < currTetromino.thisTetromino.GetLength(1); oldColumn++)
                {
                    newColumn = 0;
                    for (int oldRow = currTetromino.thisTetromino.GetLength(0) - 1; oldRow >= 0 ; oldRow--)
                    {
                        temp.thisTetromino[newRow, newColumn] = currTetromino.thisTetromino[oldRow, oldColumn];
                        newColumn++;                    }
                    newRow++;
                }
            }

            for (int i = 0; i < temp.thisTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < temp.thisTetromino.GetLength(1); j++)
                {
                    if (temp.thisTetromino[i, j] != null)
                    {
                        temp.thisTetromino[i, j].x = temp.x + j;
                        temp.thisTetromino[i, j].y = temp.y + i;
                    }
                }
            }

            currTetromino = temp;
            DrawTetronimo();
        }

        public void GenerateTetrominos()
        {
            if (nextTetromino == null)
            {
                currTetromino = new Tetromino(0);

            }
            else
            {
                for (int i = 0; i < currTetromino.thisTetromino.GetLength(0); i++)
                {
                    for (int j = 0; j < currTetromino.thisTetromino.GetLength(1); j++)
                    {
                        if (currTetromino.thisTetromino[i, j] != null)
                        {
                            board.Grid[currTetromino.y + i, currTetromino.x + j].isCurrent = false;
                        }
                    }
                }
                currTetromino = nextTetromino;
            }

            Thread.Sleep(20); //if below 12, nextTetromino will be the same as currTetromino. WIERD
            nextTetromino = new Tetromino(currTetromino.color);
            board.nextTetromino = nextTetromino;
            checkLose();
        }

        public void EraseTetronimo()
        {
            for (int i = 0; i < currTetromino.thisTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currTetromino.thisTetromino.GetLength(1); j++)
                {
                    if (currTetromino.thisTetromino[i, j] != null)
                    {
                        board.Grid[currTetromino.thisTetromino[i,j].y, currTetromino.thisTetromino[i, j].x].color = 0;
                        board.Grid[currTetromino.thisTetromino[i, j].y, currTetromino.thisTetromino[i, j].x].isCurrent = false;
                    }
                }
            }
        }

        public void DrawTetronimo()
        {
            for (int i = 0; i < currTetromino.thisTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currTetromino.thisTetromino.GetLength(1); j++)
                {
                    if (currTetromino.thisTetromino[i, j] != null)
                    {
                        board.Grid[currTetromino.thisTetromino[i, j].y, currTetromino.thisTetromino[i, j].x].color = currTetromino.thisTetromino[i, j].color;
                        board.Grid[currTetromino.thisTetromino[i, j].y, currTetromino.thisTetromino[i, j].x].isCurrent = true;
                    }            
                }
            }
        }

        public void Drop()
        {
                EraseTetronimo();
                currTetromino.y++;
                for (int i = 0; i < currTetromino.thisTetromino.GetLength(0); i++)
                {
                    for (int j = 0; j < currTetromino.thisTetromino.GetLength(1); j++)
                    {
                        if (currTetromino.thisTetromino[i, j] != null)
                        {
                            board.Grid[currTetromino.y + i, currTetromino.x + j].y--;
                            currTetromino.thisTetromino[i, j].y++;
                        }
                    }
                }
                DrawTetronimo();
        }

        void checkRows()
        {
            for (int i = 0; i < board.Grid.GetLength(0); i++)
            {
                int ck = 0;
                for (int j = 0; j < board.Grid.GetLength(1); j++)
                {
                    if (board.Grid[i, j].color > 0)
                    {
                        ck++;
                    }
                }
                if (ck == board.Grid.GetLength(1) - 2)
                {
                    DeleteRow(i);
                }
            }
        }

        void DeleteRow(int row)
        {
            for (int i = 0; i < board.Grid.GetLength(1); i++)
            {
                board.Grid[row, i].color = 9;
            }
            board.DrawBoard();
            Thread.Sleep(500);
            for (int i = row; i >= 0; i--)
            {
                for (int j = 0; j < board.Grid.GetLength(1); j++)
                {
                    if (i-1 > 0)
                    board.Grid[i, j].color = board.Grid[i-1, j].color;
                }
            }
            for (int i = board.Grid.GetLength(0) - 2; i >= row; i--)
            {
                for (int j = 0; j < board.Grid.GetLength(1); j++)
                {
                    if (board.Grid[i, j].color == 0)
                    {
                        board.Grid[i, j].color = board.Grid[i - 1, j].color;
                        board.Grid[i - 1, j].color = 0;
                    }                        
                }
            }
            board.score += 10;
            if (board.score / 10 > difficulty)
            {
                if (dropRate - 150 > 100)
                    dropRate = dropRate - 150;
                else if(dropRate - 40 > 100)
                    dropRate = dropRate - 40;
                difficulty = difficulty + 2;
            }
            board.DrawBoard();
            checkRows();
        }

        //NEEDS CHECKING
        void checkLose()
        {
            for (int i = 0; i < board.Grid.GetLength(1); i++)
            {
                if (board.Grid[1, i].color > 0)
                    GameOver();
            }
        }

        //NEEDS Writing
        void GameOver()
        {
            isPlaying = false;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╔══════════════════╗");
            Console.WriteLine("║    GAME OVER!    ║");
            if (board.score == 0)
                Console.WriteLine("║     score: {0}     ║", board.score);
            else if (board.score >= 10 && board.score < 100)
                Console.WriteLine("║    score: {0}     ║", board.score);
            else
                Console.WriteLine("║    score: {0}    ║", board.score);
            Console.WriteLine("╚══════════════════╝");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            Console.WriteLine("Press any key to save and continue");
            Console.ReadKey();
            Scoreboard.save(board.score);
            Menu m = new Menu();
            m.MainMenu();
        }

        public void Game()
        {
            board = new Board(25, 20);
            GenerateTetrominos();
            Tetromino.Spawn(currTetromino, board);
            board.DrawBoard();
            deltaTime.Start();
            while (isPlaying)
            {
                input();
                Update();
            }
        }
    }
}
