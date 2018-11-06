using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Tetris
{
    class Menu
    {
        int menuSpace = 0;

        public void MainMenu()
        {
            Console.Clear();
            Header();
            Console.WriteLine("    Controls: Arrow Keys + Spacebar");
            Console.WriteLine();
            if (menuSpace == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("   Start Game   ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   Scoreboarrd   ");
                Console.WriteLine("   Exit   ");
            }
            else if (menuSpace == 1)
            {
                Console.WriteLine("   Start Game   ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("   Scoreboarrd   ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   Exit   ");
            }
            else if (menuSpace == 2)
            {
                Console.WriteLine("   Start Game   ");
                Console.WriteLine("   Scoreboarrd   ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("   Exit   ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            bool ck = true;
            while (ck)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if(key.Key == ConsoleKey.UpArrow)
                {
                    if (menuSpace > 0)
                    {
                        menuSpace--;
                        ck = false;
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (menuSpace < 2)
                    {
                        menuSpace++;
                        ck = false;
                    }
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    if(menuSpace == 0)
                    {
                        Tetris game = new Tetris();
                        game.dropRate = 1000;
                        game.Game();
                    }
                    if(menuSpace == 1)
                    {
                        if (File.Exists(Scoreboard.path))
                        {
                            scoreboard();
                        }
                        else Console.WriteLine("   No scores recorderd yet");
                    }
                    if (menuSpace == 2)
                    {
                        Environment.Exit(0);
                    }
                }
            }
            MainMenu();
        }

        public void scoreboard()
        {
            Console.Clear();
            Header();
            Console.WriteLine("    Press any key to go back");
            Scoreboard.load();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    MainMenu();
                }
            }
        }


        void Header()
        {
            Console.WriteLine("      ___           ___           ___           ___                       ___     ");
            Console.WriteLine("     /\\  \\         /\\  \\         /\\  \\         /  /\\        ___          /  /\\    ");
            Console.WriteLine("     \\:\\  \\       /::\\  \\        \\:\\  \\       /  /::\\      /  /\\        /  /:/_   ");
            Console.WriteLine("      \\:\\  \\     /:/\\:\\  \\        \\:\\  \\     /  /:/\\:\\    /  /:/       /  /:/ /\\  ");
            Console.WriteLine("      /::\\  \\   /::\\~\\:\\  \\       /::\\  \\   /  /:/~/:/   /__/::\\      /  /:/ /::\\ ");
            Console.WriteLine("     /:/\\:\\__\\ /:/\\:\\ \\:\\__\\     /:/\\:\\__\\ /__/:/ /:/___ \\__\\/\\:\\__  /__/:/ /:/\\:\\");
            Console.WriteLine("    /:/  \\/__/ \\:\\~\\:\\ \\/__/    /:/  \\/__/ \\  \\:\\/:::::/    \\  \\:\\/\\ \\  \\:\\/:/~/:/");
            Console.WriteLine("   /:/  /       \\:\\ \\:\\__\\     /:/  /       \\  \\::/~~~~      \\__\\::/  \\  \\::/ /:/ ");
            Console.WriteLine("   \\/__/         \\:\\ \\/__/     \\/__/         \\  \\:\\          /__/:/    \\__\\/ /:/  ");
            Console.WriteLine("                  \\:\\__\\                      \\  \\:\\         \\__\\/       /__/:/   ");
            Console.WriteLine("                   \\/__/                       \\__\\/                     \\__\\/    ");
            Console.WriteLine();
        }
    }
}
