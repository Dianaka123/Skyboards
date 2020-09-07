
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SteamHelper
    {
        private static readonly string path = Application.persistentDataPath + "/score1.gd";

        public static int ReadHighScore()
        {
            if (!File.Exists(path))
            {
                return 0;
            }
            using (StreamReader reader = new StreamReader(path))
            {
                return int.Parse(reader.ReadLine());
            }
        }

        public static void WriteHighScore(int highScore)
        {
            using (var writer = new StreamWriter(path, false))
            {
                writer.WriteLine(highScore);
            }
        }
    }
}
