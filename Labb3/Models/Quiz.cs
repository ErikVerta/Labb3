using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace Labb3.Models
{
    internal sealed class Quiz
    {
        public ICollection<Question> Questions { get; }
        public string Title { get; }

        private readonly Random _r = new Random();

        [JsonConstructor]
        public Quiz(ICollection<Question> questions, string title)
        {
            Questions = questions;
            Title = title;
        }

        public Question GetRandomQuestion()
        {
            int index = _r.Next(0, Questions.Count);

            return Questions.ElementAt(index);
        }

        public void AddQuestion(string statement, int correctAnswer, params string[] answers)
        {
            Questions.Add(new Question(statement, answers, correctAnswer));
        }

        public void RemoveQuestion(int index)
        {
            Questions.Remove(Questions.ElementAt(index));
        }

        public static string[] GetQuizTitles()
        {
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fullPath = Path.Combine(localPath, @"Labb3");
            Directory.CreateDirectory(fullPath);

            var fileNames = Directory.GetFiles(fullPath);

            for (int i = 0; i < fileNames.Length; i++)
            {
                fileNames[i] = Path.GetFileNameWithoutExtension(fileNames[i]);
            }

            return fileNames;
        }
    }
}
