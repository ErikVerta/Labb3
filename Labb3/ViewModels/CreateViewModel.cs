using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Labb3.ViewModels
{
    internal sealed class CreateViewModel : ObservableObject
    {

        public ICommand SaveQuizCommand { get; }
        public ICommand AddQuestionCommand { get; }
        public ICommand RadioButtonCommand { get; }
        public ICommand MainMenuCommand { get; }
        public ICommand OpenImageCommand { get; }

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

        private string _answer4;

        public string Answer4
        {
            get => _answer4;
            set
            {
                SetProperty(ref _answer4, value);
                SetAnswer4RadioButtonIsEnabled();
            }
        }

        private bool _answer4RadioButtonIsEnabled;

        public bool Answer4RadioButtonIsEnabled
        {
            get => _answer4RadioButtonIsEnabled;
            set => SetProperty(ref _answer4RadioButtonIsEnabled, value);
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

        private string _imageTextBox;

        public string ImageTextBox
        {
            get => _imageTextBox;
            set => SetProperty(ref _imageTextBox, value);
        }

        private MainWindowViewModel MainWindowViewModel { get; }

        public CreateViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
            SaveQuizCommand = new RelayCommand(SaveQuiz);
            AddQuestionCommand = new RelayCommand(SaveQuestion);
            RadioButtonCommand = new RelayCommand<string>(SetCorrectAnswer);
            MainMenuCommand = new RelayCommand(OpenMainMenuView);
            OpenImageCommand = new RelayCommand(OpenImage);
            Questions = new List<Question>();
            ValidateTitleText = "Unavailable";
            ValidateTitleTextColor = Brushes.Red;
        }

        //Opens a FileDialog and sets the imageTextBox to the chosen image path.
        private void OpenImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ImageTextBox = op.FileName;
            }
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
                MessageBox.Show("Please fill in all of the required boxes.");
                return;
            }
            else if (CorrectAnswer < 0 || CorrectAnswer > 3)
            {
                MessageBox.Show("Please choose the correct answer.");
                return;
            }

            if (string.IsNullOrEmpty(Answer4) && CorrectAnswer == 3)
            {
                MessageBox.Show("Answer 4 can't be the correct answer because there is no answer 4, please change the correct answer or fill in the answer 4 textbox.");
                return;
            }

            string[] answers;
            if (string.IsNullOrEmpty(Answer4))
            {
                answers = new[] { Answer1, Answer2, Answer3 };
            }
            else
            {
                answers = new[] { Answer1, Answer2, Answer3, Answer4 };
            }
            if (string.IsNullOrEmpty(ImageTextBox))
            {
                Questions.Add(new Question(Statement, answers, new Category(Category), CorrectAnswer));
            }
            else
            {
                FileManagerModel.SaveImage(ImageTextBox);
                Questions.Add(new Question(Statement, answers, new Category(Category), CorrectAnswer, GetImagePath()));
            }
            Statement = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            Answer4 = string.Empty;
            Category = string.Empty;
            ImageTextBox = string.Empty;
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
            if (string.IsNullOrEmpty(title))
            {
                return false;
            }

            var allTitles = Quiz.GetQuizTitles();
            foreach (var item in allTitles)
            {
                if (item.ToLower() == title.ToLower())
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

        //Returns the new path of the image in the labb3/Image folder.
        private string GetImagePath()
        {
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(localPath, @"Labb3", @"Images");
            string imageName = Path.GetFileName(ImageTextBox);
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, imageName);
        }

        //Sets Answer4RadioButtonIsEnabled to false if Answer4 is null or empty, otherwise sets it to true.
        private void SetAnswer4RadioButtonIsEnabled()
        {
            if (string.IsNullOrEmpty(Answer4))
            {
                Answer4RadioButtonIsEnabled = false;
            }
            else
            {
                Answer4RadioButtonIsEnabled = true;
            }
        }
    }
}
