using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.Models
{
    internal sealed class Question : ObservableObject
    {
        public string Statement { get; }
        public string[] Answers { get; }
        public int CorrectAnswer { get; }
        public Category Category { get; }
        public string Image { get; }

        public Question(string statement, string[] answers, Category category, int correctAnswer)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
            Category = category;
        }
        [JsonConstructor]
        public Question(string statement, string[] answers, Category category, int correctAnswer, string image)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
            Category = category;
            Image = image;
        }
    }
}
