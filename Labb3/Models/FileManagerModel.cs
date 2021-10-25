using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace Labb3.Models
{
    internal sealed class FileManagerModel
    {
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
    }
}
