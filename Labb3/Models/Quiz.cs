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

        private readonly Random _random = new Random();

        [JsonConstructor]
        public Quiz(ICollection<Question> questions, string title)
        {
            Questions = questions;
            Title = title;
        }

        //Gets a random Question from Questions using the field _random.
        public Question GetRandomQuestion()
        {
            int index = _random.Next(0, Questions.Count);

            return Questions.ElementAt(index);
        }

        //instantiates a new Question object using the parameters and adds it to Questions.
        public void AddQuestion(string statement, int correctAnswer, params string[] answers)
        {
            Questions.Add(new Question(statement, answers, correctAnswer));
        }

        //Removes the question using the parameter index as index.
        public void RemoveQuestion(int index)
        {
            Questions.Remove(Questions.ElementAt(index));
        }

        //Searches through the folder Labb3 and gets the names of all of the files and returns them as a string[].
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
