using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tetris
{
    class Scoreboard
    {
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\tetris.txt";
        TextWriter sw = new StreamWriter(path);

        public static void save(int score)
        {
            if (File.Exists(path))
            {
                string[] prevScores = File.ReadAllLines(path);

                List<int> tempScores = new List<int>();
                for (int i = 0; i < prevScores.Length; i++)
                {
                    tempScores.Add(Convert.ToInt32(prevScores[i]));
                }
                tempScores.Add(score);
                tempScores.Sort();
                tempScores.Reverse();
                string[] newScores = new string[10];
                for (int i = 0; i < 10; i++)
                {
                    newScores[i] = tempScores[i].ToString();
                }
                File.WriteAllLines(path, newScores);
            }
            else
            {
                string[] newScores = new string[] { score.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                File.WriteAllLines(path, newScores);
            }
        }
        public static void load()
        {
            string[] temp = File.ReadAllLines(path);
            for (int i = 0; i < temp.Length; i++)
            {
                Console.WriteLine(i+1 + ". " + temp[i]);
            }
        }
    }
}
