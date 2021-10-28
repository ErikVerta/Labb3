using System.Collections.Generic;
using System.Linq;

namespace Labb3.Models
{
    internal sealed class EditModel
    {
        public Quiz CurrentQuiz { get; }

        public Question CurrentQuestion { get; set; }

        public EditModel(Quiz currentQuiz)
        {
            CurrentQuiz = currentQuiz;
        }

        //Gets the index of the question whose statement corresponds to the parameter statement.
        public int GetIndex(string statement)
        {
            foreach (var question in CurrentQuiz.Questions)
            {
                if (question.Statement.ToLower() == statement.ToLower())
                {
                    return CurrentQuiz.Questions.ToList().IndexOf(question);
                }
            }
            return 0;
        }

        //Gets the statement from all of the questions in the quiz and returns them as a string[].
        public string[] GetStatements()
        {
            var statements = new string[CurrentQuiz.Questions.Count];
            for (int i = 0; i < CurrentQuiz.Questions.Count; i++)
            {
                statements[i] = CurrentQuiz.Questions.ElementAt(i).Statement;
            }

            return statements;
        }
        //Looks for the question whose statement corresponds to the parameter statement and sets CurrentQuestion to that question.
        public void SetCurrentQuestion(string statement)
        {
            foreach (var question in CurrentQuiz.Questions)
            {
                if (question.Statement.ToLower() == statement.ToLower())
                {
                    CurrentQuestion = question;
                }
            }
        }
    }
}
