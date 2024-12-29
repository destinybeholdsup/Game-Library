using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Desktop_Assistant.DB
{
    public class LibraryHandler
    {
        private readonly string filePath = "gamelibrary.json";

        public void SaveLibrary(List<Game> games)
        {
            List<Game> existingGames = LoadLibrary();
            existingGames.AddRange(games);

            string json = JsonConvert.SerializeObject(existingGames, Formatting.Indented);
            File.WriteAllText(filePath, json);

            Console.WriteLine("Game library updated.");
        }

        public List<Game> LoadLibrary()
        {
            if (!File.Exists(filePath)) {
                return new List<Game>();
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Game>>(json) ?? new List<Game>();
        }

        public bool LibraryExists(bool create = true)
        {
            if (File.Exists(filePath)) {
                return true;
            }

            if (create) {
                File.WriteAllText(filePath, "[]");
                Console.WriteLine("Game library file created.");
                return true;
            }

            return false;
        }
    }
}
