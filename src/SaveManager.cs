namespace Nocturnal.src
{
    public struct SaveData
    {
        public string Date;
        public string Hour;
        public string Player;
        public int Chapter;
        public int Gender;
        public int Stage;
    };

    public class SaveManager
    {
        private static int saveNr = 0;

        public static void CreateSave()
        {
            if (!Directory.Exists("data\\saves"))
                Directory.CreateDirectory("data\\saves");

            string path = $"{Directory.GetCurrentDirectory()}\\data\\saves\\save_{saveNr}.dat";

            if (!File.Exists(path))
            {
                using (StreamWriter newSave = File.CreateText(path))
                {
                    saveNr++;
                    newSave.WriteLine(Logger.GetFormattedUtcTimestamp());
                    newSave.WriteLine($"Player::Unknown");
                    newSave.WriteLine($"Sex::Undefined");
                    newSave.WriteLine($"{0}::{1}");
                }
            }
        }

        public static void LoadSave(int nr)
        {
            SaveData save;
            string path = $"{Directory.GetCurrentDirectory()}\\data\\saves\\save_{saveNr}.dat";

            if (!File.Exists(path))
            {
                Globals.Game.LoadLogo();
                return;
            }

            using (StreamReader oldSave = File.OpenText(path))
            {
                string s;
                while ((s = oldSave.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }

            //Hero.heroes["Hero"].Name = save.player;
            //Hero.heroes["Hero"].Sex = save.gender;
        }

        public static void UpdateSave(int saveNr, string player, int sex, int chapter, int stage)
        {

        }

        public static string PrintSex(int sex)
        {
            //if (sex == Sex.Male)
            //{
            //    return "Male";
            //}
            //else if (sex == Sex.Female)
            //{
            //    return "Female";
            //}
            return "Undefined";
        }

        private static string GetChapterString(int chapter)
        {
            if (chapter == 0 || chapter < 0)
            {
                return Globals.JsonReader["names"]["story"]["prologue"].ToString();
            }
            else if (chapter == 1 || chapter == 2)
            {
                return $"{Globals.JsonReader["names"]["story"]["chapter"].ToString()} {chapter}";
            }
            return Globals.JsonReader["names"]["story"]["epilogue"].ToString();
        }

        private static void LoadSaveInfo(string saveToLoad)
        {
            SaveData save;

            if (!File.Exists(saveToLoad))
            {
                Console.WriteLine($"{saveToLoad} - nie ma takiego pliku!");
                return;
            }

            using (StreamReader oldSave = File.OpenText(saveToLoad))
            {
                string s;
                while ((s = oldSave.ReadLine()) != null)
                {
                    Console.WriteLine($"\t{s}");
                }
            }

            //Console.WriteLine($"\t{save.Player}, {PrintSex(save.Gender)} | {GetChapterString(save.Chapter)} : {save.Stage} | {save.Date} {save.Hour}");
        }

        public static void SearchForSaves()
        {
            string path = $"{Directory.GetCurrentDirectory()}\\data\\saves";
            var files = Directory.GetFiles(path, "save_*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".dat"));

            if (files.Count() > 0)
            {
                foreach (dynamic file in files)
                    LoadSaveInfo(file);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(Globals.JsonReader["messages"]["no_saves_found"].ToString());
                Console.ResetColor();
            }
        }
    }
}
