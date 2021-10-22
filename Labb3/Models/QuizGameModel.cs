using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.Models
{
    internal sealed class QuizGameModel : ObservableObject
    {
        private Question _currentQuestion;

        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set => SetProperty(ref _currentQuestion, value);
        }

        private Quiz CurrentQuiz { get; }

        private ICollection<Question> _answeredQuestions;

        public ICollection<Question> AnsweredQuestions
        {
            get => _answeredQuestions;
            set => SetProperty(ref _answeredQuestions, value);
        }

        public QuizGameModel(Quiz currentQuiz)
        {
            CurrentQuiz = currentQuiz;
        }

        public void GetQuestion()
        {
            var question = CurrentQuiz.GetRandomQuestion();
            if (AnsweredQuestions.Count == CurrentQuiz.Questions.Count)
            {
                return;
            }
            else if (AnsweredQuestions.Contains(question))
            {
                GetQuestion();
            }

            CurrentQuestion = question;
        }

        public bool ValidateAnswer(int answer)
        {
            return CurrentQuestion.Answers[answer].ToLower() == CurrentQuestion.Answers[CurrentQuestion.CorrectAnswer];
        }
    }
}
