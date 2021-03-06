using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class PlayViewModel : ObservableObject
    {
        public PlayModel PlayModel { get; }
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

        private Brush _answer4ButtonColor;

        public Brush Answer4ButtonColor
        {
            get => _answer4ButtonColor;
            set => SetProperty(ref _answer4ButtonColor, value);
        }

        private Visibility _answer4ButtonVisibility;

        public Visibility Answer4ButtonVisibility
        {
            get => _answer4ButtonVisibility;
            set => SetProperty(ref _answer4ButtonVisibility, value);
        }

        private Visibility _imageVisibility;

        public Visibility ImageVisibility
        {
            get => _imageVisibility;
            set => SetProperty(ref _imageVisibility, value);
        }

        private Visibility _imageStatementTextVisibility;

        public Visibility ImageStatementTextVisibility
        {
            get => _imageStatementTextVisibility;
            set => SetProperty(ref _imageStatementTextVisibility, value);
        }

        private Visibility _noImageStatementTextVisibility;

        public Visibility NoImageStatementTextVisibility
        {
            get => _noImageStatementTextVisibility;
            set => SetProperty(ref _noImageStatementTextVisibility, value);
        }
        public MainWindowViewModel MainWindowViewModel { get; }

        public PlayViewModel(PlayModel playModel, MainWindowViewModel mainWindowViewModel)
        {
            PlayModel = playModel;
            AnswerQuestionCommand = new RelayCommand<string>(OnAnswer);
            NextQuestionCommand = new RelayCommand(NextQuestion);
            NextButtonIsEnabled = false;
            NextQuestion();
            MainWindowViewModel = mainWindowViewModel;
        }

        //If PlayModel.GetQuestion returns false the user has answered all the question and it changes SelectedViewModel to ResultViewModel.
        //Otherwise it checks if the question has an image and 3 or 4 question and hides and shows the necessary elements, then it sets the NextButtonIsEnabled to false;
        private void NextQuestion()
        {
            Answer1ButtonColor = Brushes.Wheat;
            Answer2ButtonColor = Brushes.Wheat;
            Answer3ButtonColor = Brushes.Wheat;
            Answer4ButtonColor = Brushes.Wheat;
            if (!PlayModel.GetQuestion())
            {
                MainWindowViewModel.SelectedViewModel = new ResultViewModel(CorrectAnswers, PlayModel.AnsweredQuestions.Count, MainWindowViewModel);
            }

            if (string.IsNullOrEmpty(PlayModel.CurrentQuestion.Image))
            {
                ImageVisibility = Visibility.Collapsed;
                ImageStatementTextVisibility = Visibility.Collapsed;
                NoImageStatementTextVisibility = Visibility.Visible;
            }
            else
            {
                ImageVisibility = Visibility.Visible;
                ImageStatementTextVisibility = Visibility.Visible;
                NoImageStatementTextVisibility = Visibility.Collapsed;
            }

            if (PlayModel.CurrentQuestion.Answers.Length != 4)
            {
                Answer4ButtonVisibility = Visibility.Collapsed;
            }
            else
            {
                Answer4ButtonVisibility = Visibility.Visible;
            }
            NextButtonIsEnabled = false;
        }

        //If the user guesses right it will increase CorrectAnswers by 1 then it changes the color of the answerButtons to show the correct answer.
        //Then it adds the question to AnsweredQuestions.
        private void OnAnswer(string answerIndex)
        {
            if (!PlayModel.ValidateAnswer((int.Parse(answerIndex))))
            {
                NextButtonIsEnabled = true;
            }
            else
            {
                CorrectAnswers++;
                NextButtonIsEnabled = true;
            }

            Answer1ButtonColor = Brushes.Red;
            Answer2ButtonColor = Brushes.Red;
            Answer3ButtonColor = Brushes.Red;
            Answer4ButtonColor = Brushes.Red;
            switch (PlayModel.CurrentQuestion.CorrectAnswer)
            {
                case 0:
                    Answer1ButtonColor = Brushes.Green;
                    break;
                case 1:
                    Answer2ButtonColor = Brushes.Green;
                    break;
                case 2:
                    Answer3ButtonColor = Brushes.Green;
                    break;
                case 3:
                    Answer4ButtonColor = Brushes.Green;
                    break;
            }
            PlayModel.AnsweredQuestions.Add(PlayModel.CurrentQuestion);
        }
    }
}
