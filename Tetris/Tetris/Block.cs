using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Block
    {
        public int color = 0;
        public bool isCurrent = false;
        public int x, y;

        public bool isEmpty()
        {
            if (color == 0) return true;
            else return false;
        }

        //knows what color the current block is
        public void PrintBlock()
        {
            switch (color)
            {
                case 0: //black
                    Console.Write("  ");
                    break;
                case 1: //red
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 2: //blue
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 3: //green
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 4: //magenta
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 5: //white
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 6: //white
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 7: //white
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 8: //white
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 9: //white
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}
