using System.Windows.Input;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class PlayViewModel : ObservableObject
    {
        public QuizGameModel CurrentQuizGame { get; set; }
        private int _rightAnswers;

        public int RightAnswers
        {
            get => _rightAnswers;
            set => SetProperty(ref _rightAnswers, value);
        }

        public ICommand AnswerCommand { get; }
        public ICommand NextQuestionCommand { get; }

        public PlayViewModel(QuizGameModel currentQuizGame)
        {
            CurrentQuizGame = currentQuizGame;
            AnswerCommand = new RelayCommand<int>(OnAnswer);
            NextQuestionCommand = new RelayCommand(CurrentQuizGame.GetQuestion);
        }

        private void OnAnswer(int answerIndex)
        {
            if (!CurrentQuizGame.ValidateAnswer(answerIndex))
            {
                WrongAnswer();
            }
            else
            {
                CorrectAnswer();
            }
        }

        private void CorrectAnswer()
        {
            RightAnswers++;
            CurrentQuizGame.AnsweredQuestions.Add(CurrentQuizGame.CurrentQuestion);
        }

        private void WrongAnswer()
        {
            CurrentQuizGame.AnsweredQuestions.Add(CurrentQuizGame.CurrentQuestion);
        }
    }
}
