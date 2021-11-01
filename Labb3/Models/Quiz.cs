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

        private readonly Random _random = new();

        public ICollection<Category> Categories { get; }

        [JsonConstructor]
        public Quiz(ICollection<Question> questions, string title, ICollection<Category> categories)
        {
            Questions = questions;
            Title = title;
            Categories = categories;
        }

        public Quiz(ICollection<Question> questions, string title)
        {
            Questions = questions;
            Title = title;
            Categories = new List<Category>();
            var categories = new List<string>();
            foreach (var question in questions)
            {
                categories.Add(question.Category.Name.ToLower());
            }
            AddCategories(categories.ToArray());
        }

        //Gets a random Question from Questions using the field _random.
        public Question GetRandomQuestion()
        {
            int index = _random.Next(0, Questions.Count);

            return Questions.ElementAt(index);
        }

        //instantiates a new Question object using the parameters and adds it to Questions.
        public void AddQuestion(string statement, int correctAnswer,string category ,params string[] answers)
        {
            Questions.Add(new Question(statement, answers, new Category(category), correctAnswer));
            AddCategories(category.ToLower());

        }

        //Removes the question using the parameter index as index.
        public void RemoveQuestion(int index)
        {
            string tempCategory = Questions.ElementAt(index).Category.Name;
            Questions.Remove(Questions.ElementAt(index));
            RemoveCategories(tempCategory);
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
        //Uses the parameter categories to determine if the category already exists in Categories.
        private void AddCategories(params string[] categories)
        {
            if (Categories.Count == 0)
            {
                if (Categories != null) Categories.Add(new Category(categories[0].ToLower()));
            }
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < Categories.Count; j++)
                {
                    if (categories[i].ToLower() == Categories.ElementAt(j).Name.ToLower())
                    {
                        break;
                    }
                    else if (j == Categories.Count - 1)
                    {
                        Categories.Add(new Category(categories[i].ToLower()));
                    }
                }
            }
        }
        //Uses the parameter categories to determine if there are questions with the same category. If there are none the category will be added to tempCategories
        //and then removed from Categories.
        private void RemoveCategories(params string[] categories)
        {
            var tempCategories = new List<string>();
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < Questions.Count; j++)
                {
                    if (categories[i].ToLower() == Questions.ElementAt(j).Category.Name.ToLower())
                    {
                        break;
                    }
                    else if (j == Questions.Count - 1)
                    {
                        tempCategories.Add(categories[i]);
                    }
                }
            }

            for (int i = 0; i < tempCategories.Count; i++)
            {
                for (int j = 0; j < Categories.Count; j++)
                {
                    if (Categories.ElementAt(j).Name.ToLower() == tempCategories.ElementAt(i).ToLower())
                    {
                        Categories.Remove(Categories.ElementAt(j));
                    }
                }
            }
        }
    }
}
