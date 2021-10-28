using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace Labb3.Models
{
    internal sealed class FileManagerModel
    {
        
        //Saves the parameter quiz to a .json file in the folder Labb3.
        public static async void SaveFileAsync(Quiz quiz)
        {
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(localPath, @"Labb3");
            string fullPath = Path.Combine(folderPath, @$"{quiz.Title}.json");
            Directory.CreateDirectory(folderPath);

            await using FileStream createStream = File.Create(fullPath);
            await JsonSerializer.SerializeAsync(createStream, quiz);
            await createStream.DisposeAsync();
        }


        //Loads the file with the same name as the parameter title and returns it as a Quiz.
        public static async Task<Quiz> LoadFileAsync(string title)
        {
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(localPath, @"Labb3");
            string fullPath = Path.Combine(folderPath, @$"{title}.json");
            Directory.CreateDirectory(folderPath);

            await using FileStream openStream = File.OpenRead(fullPath);
            Quiz quiz = await JsonSerializer.DeserializeAsync<Quiz>(openStream);
            await openStream.DisposeAsync();

            return quiz;
        }

        //Removes the file with the same title as the parameter quiz.
        public static void RemoveFileAsync(Quiz quiz)
        {
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(localPath, @"Labb3");
            string fullPath = Path.Combine(folderPath, @$"{quiz.Title}.json");
            Directory.CreateDirectory(folderPath);

            File.Delete(fullPath);
        }
    }
}
