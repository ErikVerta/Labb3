using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class CreateViewModel : ObservableObject
    {

        public ICommand SaveQuizCommand { get; }
        public ICommand AddQuestionCommand { get; }
        public ICommand RadioButtonCommand { get; }
        public ICommand MainMenuCommand { get; }

        private int CorrectAnswer { get; set; }

        public List<Question> Questions { get; }
        private string _statement;

        public string Statement
        {
            get => _statement;
            set => SetProperty(ref _statement, value);
        }

        private string _answer1;

        public string Answer1
        {
            get => _answer1;
            set => SetProperty(ref _answer1, value);
        }
        private string _answer2;

        public string Answer2
        {
            get => _answer2;
            set => SetProperty(ref _answer2, value);
        }
        private string _answer3;

        public string Answer3
        {
            get => _answer3;
            set => SetProperty(ref _answer3, value);
        }
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
                CheckTitleAvailability();
            }
        }

        private string _category;

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private string _validateTitleText;

        public string ValidateTitleText
        {
            get => _validateTitleText;
            set => SetProperty(ref _validateTitleText, value);
        }

        private Brush _validateTitleTextColor;

        public Brush ValidateTitleTextColor
        {
            get => _validateTitleTextColor;
            set => SetProperty(ref _validateTitleTextColor, value);
        }

        private MainWindowViewModel MainWindowViewModel { get; }

        public CreateViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
            SaveQuizCommand = new RelayCommand(SaveQuiz);
            AddQuestionCommand = new RelayCommand(SaveQuestion);
            RadioButtonCommand = new RelayCommand<string>(SetCorrectAnswer);
            MainMenuCommand = new RelayCommand(OpenMainMenuView);
            Questions = new List<Question>();
            ValidateTitleText = "Unavailable";
            ValidateTitleTextColor = Brushes.Red;
        }

        //Changes the SelectedViewModel to MainMenuViewModel.
        private void OpenMainMenuView()
        {
            MainWindowViewModel.SelectedViewModel = new MainMenuViewModel(MainWindowViewModel);
        }

        //Uses the radioButtons command-parameters to set the correctAnswer.
        private void SetCorrectAnswer(string number)
        {
            CorrectAnswer = int.Parse(number);
        }

        //Makes sure that the user has filled in all of the textBoxes then saves the question to a list of questions and
        //clears the textBoxes.
        private void SaveQuestion()
        {
            if (string.IsNullOrEmpty(Statement) || string.IsNullOrEmpty(Answer1) || string.IsNullOrEmpty(Answer2) ||
                string.IsNullOrEmpty(Answer3) || string.IsNullOrEmpty(Category))
            {
                MessageBox.Show("Please fill in all of the fields.");
                return;
            }
            else if (CorrectAnswer < 0 || CorrectAnswer > 2)
            {
                MessageBox.Show("Please chose the correct answer.");
                return;
            }
            string[] answers = new[] { Answer1, Answer2, Answer3 };
            Questions.Add(new Question(Statement, answers, new Category(Category), CorrectAnswer));
            Statement = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            Category = string.Empty;
            MessageBox.Show("Question added.");
        }

        //Changes the ValidateTitleText and color depending on if the title is already in use or not.
        private void CheckTitleAvailability()
        {
            if (!ValidateTitle(Title))
            {
                ValidateTitleText = "Unavailable";
                ValidateTitleTextColor = Brushes.Red;
                return;
            }

            ValidateTitleText = "Available";
            ValidateTitleTextColor = Brushes.Green;
        }

        //Validates the title by comparing it to the titles that are saved in the folder Labb3.
        private static bool ValidateTitle(string title)
        {
            var allTitles = Quiz.GetQuizTitles();
            foreach (var item in allTitles)
            {
                if (item.ToLower() == title.ToLower())
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(title))
                {
                    return false;
                }
            }

            return true;
        }

        //Makes sure that the title is available then saves the quiz to a .json file in the Labb3 folder.
        private void SaveQuiz()
        {
            if (ValidateTitleText == "Unavailable")
            {
                MessageBox.Show("Title is Unavailable!");
                return;
            }
            FileManagerModel.SaveFileAsync(new Quiz(Questions, Title));
            MessageBox.Show("Quiz Saved.");
            MainWindowViewModel.SelectedViewModel = new MainMenuViewModel(MainWindowViewModel);
        }
    }
}
