using System.Windows.Input;
using System.Windows.Media;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class PlayViewModel : ObservableObject
    {
        public QuizGameModel CurrentQuizGame { get; set; }
        private int _correctAnswers;

        public int CorrectAnswers
        {
            get => _correctAnswers;
            set => SetProperty(ref _correctAnswers, value);
        }

        public ICommand AnswerQuestionCommand { get; }
        public ICommand NextQuestionCommand { get; }

        private bool _nextButtonIsEnabled;

        public bool NextButtonIsEnabled
        {
            get => _nextButtonIsEnabled;
            set => SetProperty(ref _nextButtonIsEnabled, value);
        }

        private Brush _answer1ButtonColor;

        public Brush Answer1ButtonColor
        {
            get => _answer1ButtonColor;
            set => SetProperty(ref _answer1ButtonColor, value);
        }
        private Brush _answer2ButtonColor;

        public Brush Answer2ButtonColor
        {
            get => _answer2ButtonColor;
            set => SetProperty(ref _answer2ButtonColor, value);
        }
        private Brush _answer3ButtonColor;

        public Brush Answer3ButtonColor
        {
            get => _answer3ButtonColor;
            set => SetProperty(ref _answer3ButtonColor, value);
        }
        public MainMenuViewModel MainMenuViewModel { get; }

        public PlayViewModel(QuizGameModel currentQuizGame, MainMenuViewModel mainMenuViewModel)
        {
            CurrentQuizGame = currentQuizGame;
            AnswerQuestionCommand = new RelayCommand<string>(OnAnswer);
            NextQuestionCommand = new RelayCommand(NextQuestion);
            NextButtonIsEnabled = false;
            currentQuizGame.GetQuestion();
            Answer1ButtonColor = Brushes.Wheat;
            Answer2ButtonColor = Brushes.Wheat;
            Answer3ButtonColor = Brushes.Wheat;
            MainMenuViewModel = mainMenuViewModel;
        }

        private void NextQuestion()
        {
            Answer1ButtonColor = Brushes.Wheat;
            Answer2ButtonColor = Brushes.Wheat;
            Answer3ButtonColor = Brushes.Wheat;
            if (!CurrentQuizGame.GetQuestion())
            {
                MainMenuViewModel.SelectedViewModel = new ResultViewModel(CorrectAnswers, CurrentQuizGame.AnsweredQuestions.Count);
            }
            NextButtonIsEnabled = false;
        }
        private void OnAnswer(string answerIndex)
        {
            if (!CurrentQuizGame.ValidateAnswer((int.Parse(answerIndex))))
            {
                NextButtonIsEnabled = true;
            }
            else
            {
                CorrectAnswers++;
                NextButtonIsEnabled = true;
            }

            switch (CurrentQuizGame.CurrentQuestion.CorrectAnswer)
            {
                case 0:
                    Answer1ButtonColor = Brushes.Green;
                    Answer2ButtonColor = Brushes.Red;
                    Answer3ButtonColor = Brushes.Red;
                    break;
                case 1:
                    Answer1ButtonColor = Brushes.Red;
                    Answer2ButtonColor = Brushes.Green;
                    Answer3ButtonColor = Brushes.Red;
                    break;
                case 2:
                    Answer1ButtonColor = Brushes.Red;
                    Answer2ButtonColor = Brushes.Red;
                    Answer3ButtonColor = Brushes.Green;
                    break;
            }
            CurrentQuizGame.AnsweredQuestions.Add(CurrentQuizGame.CurrentQuestion);
        }
    }
}
