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

        public string[] GetStatements()
        {
            var statements = new List<string>();
            foreach (var question in CurrentQuiz.Questions)
            {
                statements.Add(question.Statement);
            }

            var returnArray = statements.ToArray();
            return returnArray;
        }

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
