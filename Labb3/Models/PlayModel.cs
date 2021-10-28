using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.Models
{
    internal sealed class PlayModel : ObservableObject
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

        public PlayModel(Quiz currentQuiz)
        {
            CurrentQuiz = currentQuiz;
            AnsweredQuestions = new ObservableCollection<Question>();
        }

        //Makes sure that not all questions has been answered, then sets currentQuestion to a question that has not yet been answered.
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

        //Checks if the parameter answer is the same as CorrectAnswer.
        public bool ValidateAnswer(int answer)
        {
            return answer == CurrentQuestion.CorrectAnswer;
        }
    }
}
