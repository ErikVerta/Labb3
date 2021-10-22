using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.Models
{
    internal sealed class Question
    {
        public string Statement { get; }
        public string[] Answers { get; }
        public int CorrectAnswer { get; }

        public Question(string statement, string[] answers, int correctAnswer)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }
}
