using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Quiz CurrentQuiz { get; }

        private ObservableCollection<Question> _answeredQuestions;

        public ObservableCollection<Question> AnsweredQuestions
        {
            get => _answeredQuestions;
            set => SetProperty(ref _answeredQuestions, value);
        }

        public QuizGameModel(Quiz currentQuiz)
        {
            CurrentQuiz = currentQuiz;
            AnsweredQuestions = new ObservableCollection<Question>();
        }

        public bool GetQuestion()
        {
            if (AnsweredQuestions.Count == CurrentQuiz.Questions.Count)
            {
                return false;
            }

            var question = CurrentQuiz.GetRandomQuestion();
            while (AnsweredQuestions.Contains(question))
            {
                question = CurrentQuiz.GetRandomQuestion();
            }

            CurrentQuestion = question;
            return true;
        }

        public bool ValidateAnswer(int answer)
        {
            return CurrentQuestion.Answers[answer].ToLower() == CurrentQuestion.Answers[CurrentQuestion.CorrectAnswer].ToLower();
        }
    }
}
