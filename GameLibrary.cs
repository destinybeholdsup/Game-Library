using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GameLibraryApp
{
    public class GameLibrary
    {
        private readonly string filePath = "gamelibrary.json";

        public void SaveLibrary(List<Game> games)
        {
            string json = JsonConvert.SerializeObject(games, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Game library saved.");
        }

        public List<Game> LoadLibrary()
        {
            if (!File.Exists(filePath))
                return new List<Game>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Game>>(json) ?? new List<Game>();
        }

        public bool LibraryExists()
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                File.WriteAllText(filePath, "[]");
                Console.WriteLine("Game library file created.");
                return false;
            }
        }
    }
}
