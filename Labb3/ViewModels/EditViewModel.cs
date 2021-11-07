using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Labb3.ViewModels
{
    internal sealed class EditViewModel : ObservableObject
    {
        public ICommand SaveQuestionCommand { get; }
        public ICommand FinishCommand { get; }
        public ICommand RadioButtonCommand { get; }
        public ICommand MainMenuCommand { get; }
        public ICommand OpenImageCommand { get; }

        public EditModel EditModel { get; }
        public MainWindowViewModel MainWindowViewModel { get; }

        private string[] _statements;

        public string[] Statements
        {
            get => _statements;
            set => SetProperty(ref _statements, value);
        }

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

        private string _category;

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public int CorrectAnswer { get; set; }
        private string _comboBoxText;

        public string ComboBoxText
        {
            get => _comboBoxText;
            set
            {
                SetProperty(ref _comboBoxText, value);
                ChangeQuestion();
            }
        }

        private bool _answer1RadioButtonIsChecked;

        public bool Answer1RadioButtonIsChecked
        {
            get => _answer1RadioButtonIsChecked;
            set => SetProperty(ref _answer1RadioButtonIsChecked, value);
        }
        private bool _answer2RadioButtonIsChecked;

        public bool Answer2RadioButtonIsChecked
        {
            get => _answer2RadioButtonIsChecked;
            set => SetProperty(ref _answer2RadioButtonIsChecked, value);
        }
        private bool _answer3RadioButtonIsChecked;

        public bool Answer3RadioButtonIsChecked
        {
            get => _answer3RadioButtonIsChecked;
            set => SetProperty(ref _answer3RadioButtonIsChecked, value);
        }

        private bool _answer4RadioButtonIsChecked;

        public bool Answer4RadioButtonIsChecked
        {
            get => _answer4RadioButtonIsChecked;
            set => SetProperty(ref _answer4RadioButtonIsChecked, value);
        }

        private bool _answer4RadioButtonIsEnabled;

        public bool Answer4RadioButtonIsEnabled
        {
            get => _answer4RadioButtonIsEnabled;
            set => SetProperty(ref _answer4RadioButtonIsEnabled, value);
        }

        private bool _finishButtonIsEnabled;

        public bool FinishButtonIsEnabled
        {
            get => _finishButtonIsEnabled;
            set => SetProperty(ref _finishButtonIsEnabled, value);
        }

        private string _imageTextBox;

        public string ImageTextBox
        {
            get => _imageTextBox;
            set => SetProperty(ref _imageTextBox, value);
        }

        public EditViewModel(EditModel editModel, MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
            EditModel = editModel;
            RadioButtonCommand = new RelayCommand<string>(SetCorrectAnswer);
            SaveQuestionCommand = new RelayCommand(SaveQuestion);
            FinishCommand = new RelayCommand(SaveEditedQuiz);
            MainMenuCommand = new RelayCommand(OpenMainMenuView);
            OpenImageCommand = new RelayCommand(OpenImage);
            Statements = editModel.GetStatements();
            ComboBoxText = string.Empty;
            Statement = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            Answer4 = string.Empty;
            FinishButtonIsEnabled = false;
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

        //Makes sure that the question are saved then removes the old Quiz.json file from the folder Labb3 and saves the new one.
        //It then changes the selectedViewModel to MainMenuViewModel.
        private void SaveEditedQuiz()
        {
            if (!string.IsNullOrEmpty(ComboBoxText) && (EditModel.CurrentQuestion.Statement != Statement || EditModel.CurrentQuestion.Answers[0] != Answer1 ||
                EditModel.CurrentQuestion.Answers[1] != Answer2 || EditModel.CurrentQuestion.Answers[2] != Answer3 ||
                EditModel.CurrentQuestion.Category.Name != Category || EditModel.CurrentQuestion.Image != ImageTextBox))
            {
                MessageBox.Show("Unsaved changes.");
                return;
            }
            else if (!string.IsNullOrEmpty(ComboBoxText) && EditModel.CurrentQuestion.Answers.Length == 4 && EditModel.CurrentQuestion.Answers[3] != Answer4)
            {
                MessageBox.Show("Unsaved changes.");
                return;
            }
            FileManagerModel.RemoveFileAsync(EditModel.CurrentQuiz);
            FileManagerModel.SaveFileAsync(EditModel.CurrentQuiz);
            MessageBox.Show("Quiz saved.");
            MainWindowViewModel.SelectedViewModel = new MainMenuViewModel(MainWindowViewModel);
        }

        //Makes sure that changes has been made and that that all of the required textboxes are filled in. Then removes the old question and adds the new one.
        private void SaveQuestion()
        {
            if (EditModel.CurrentQuestion.Answers.Length == 4)
            {
                if (EditModel.CurrentQuestion.Statement == Statement && EditModel.CurrentQuestion.Answers[0] == Answer1 &&
                    EditModel.CurrentQuestion.Answers[1] == Answer2 && EditModel.CurrentQuestion.Answers[2] == Answer3 && 
                    EditModel.CurrentQuestion.Answers[3] == Answer4 && EditModel.CurrentQuestion.Category.Name == Category && EditModel.CurrentQuestion.Image == ImageTextBox)
                {
                    MessageBox.Show("No changes has been made.");
                    return;
                }
            }
            else
            {
                if (EditModel.CurrentQuestion.Statement == Statement && EditModel.CurrentQuestion.Answers[0] == Answer1 &&
                    EditModel.CurrentQuestion.Answers[1] == Answer2 && EditModel.CurrentQuestion.Answers[2] == Answer3 &&
                    string.IsNullOrEmpty(Answer4) && EditModel.CurrentQuestion.Category.Name == Category && EditModel.CurrentQuestion.Image == ImageTextBox)
                {
                    MessageBox.Show("No changes has been made.");
                    return;
                }
            }
            if (string.IsNullOrEmpty(Answer1) || string.IsNullOrEmpty(Answer2) || string.IsNullOrEmpty(Answer3))
            {
                MessageBox.Show("You need a minimum of 3 answers, please fill in the required boxes.");
                return;
            }
            else if (string.IsNullOrEmpty(Statement) || string.IsNullOrEmpty(Category))
            {
                MessageBox.Show("A Question needs a statement and a category, please fill in the required boxes.");
                return;
            }
            else if (string.IsNullOrEmpty(Answer4) && CorrectAnswer == 3)
            {
                MessageBox.Show("Answer 4 can't be the correct answer because there is no answer 4, please change the correct answer or fill in the answer 4 textbox.");
                return;
            }
            var index = EditModel.GetIndex(ComboBoxText);
            EditModel.CurrentQuiz.RemoveQuestion(index);
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
                EditModel.CurrentQuiz.AddQuestion(Statement, CorrectAnswer, Category, answers: answers);
            }
            else
            {
                FileManagerModel.SaveImage(ImageTextBox);
                EditModel.CurrentQuiz.AddQuestion(Statement, CorrectAnswer, Category,GetImagePath(), answers);
            }
            ComboBoxText = string.Empty;
            Statements = EditModel.GetStatements();
            MessageBox.Show("Question Saved.");
        }

        //Uses the radioButtons command-parameters to set the correctAnswer.
        private void SetCorrectAnswer(string index)
        {
            CorrectAnswer = int.Parse(index);
        }

        //If the ComboBoxText is empty it will clear all of the TextBoxes and uncheck all radioButtons,
        //otherwise it will set the currentQuestion and set the corresponding properties.
        private void ChangeQuestion()
        {
            if (string.IsNullOrEmpty(ComboBoxText))
            {
                Statement = string.Empty;
                Answer1 = string.Empty;
                Answer2 = string.Empty;
                Answer3 = string.Empty;
                Answer4 = string.Empty;
                Category = string.Empty;
                ImageTextBox = string.Empty;
                Answer1RadioButtonIsChecked = false;
                Answer2RadioButtonIsChecked = false;
                Answer3RadioButtonIsChecked = false;
                Answer4RadioButtonIsChecked = false;
                return;
            }
            EditModel.SetCurrentQuestion(ComboBoxText);

            Statement = EditModel.CurrentQuestion.Statement;
            Answer1 = EditModel.CurrentQuestion.Answers[0];
            Answer2 = EditModel.CurrentQuestion.Answers[1];
            Answer3 = EditModel.CurrentQuestion.Answers[2];
            if (EditModel.CurrentQuestion.Answers.Length == 4)
            {
                Answer4 = EditModel.CurrentQuestion.Answers[3];
            }

            if (string.IsNullOrEmpty(EditModel.CurrentQuestion.Image))
            {
                ImageTextBox = string.Empty;
            }
            else
            {
                ImageTextBox = EditModel.CurrentQuestion.Image;
            }
            Category = EditModel.CurrentQuestion.Category.Name;

            switch (EditModel.CurrentQuestion.CorrectAnswer)
            {
                case 0:
                    Answer1RadioButtonIsChecked = true;
                    break;
                case 1:
                    Answer2RadioButtonIsChecked = true;
                    break;
                case 2:
                    Answer3RadioButtonIsChecked = true;
                    break;
                case 3:
                    Answer4RadioButtonIsChecked = true;
                    break;
            }

            FinishButtonIsEnabled = true;
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
        //Returns the new path of the image in the labb3/Image folder.
        private string GetImagePath()
        {
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(localPath, @"Labb3", @"Images");
            string imageName = Path.GetFileName(ImageTextBox);
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, imageName);
        }
    }
}
