using System.Windows;
using System.Windows.Input;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class EditViewModel : ObservableObject
    {
        public ICommand SaveQuestionCommand { get; }
        public ICommand FinishCommand { get; }
        public ICommand RadioButtonCommand { get; }
        public ICommand MainMenuCommand { get; }

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

        private bool _finishButtonIsEnabled;

        public bool FinishButtonIsEnabled
        {
            get => _finishButtonIsEnabled;
            set => SetProperty(ref _finishButtonIsEnabled, value);
        }

        public EditViewModel(EditModel editModel, MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
            EditModel = editModel;
            RadioButtonCommand = new RelayCommand<string>(SetCorrectAnswer);
            SaveQuestionCommand = new RelayCommand(SaveQuestion);
            FinishCommand = new RelayCommand(SaveEditedQuiz);
            MainMenuCommand = new RelayCommand(OpenMainMenuView);
            Statements = editModel.GetStatements();
            ComboBoxText = string.Empty;
            Statement = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            FinishButtonIsEnabled = false;
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
                EditModel.CurrentQuestion.Answers[1] != Answer2 || EditModel.CurrentQuestion.Answers[2] != Answer3))
            {
                MessageBox.Show("Unsaved changes.");
                return;
            }
            FileManagerModel.RemoveFileAsync(EditModel.CurrentQuiz);
            FileManagerModel.SaveFileAsync(EditModel.CurrentQuiz);
            MessageBox.Show("Quiz saved.");
            MainWindowViewModel.SelectedViewModel = new MainMenuViewModel(MainWindowViewModel);
        }

        //Makes sure that changes has been made then removes the old question and saves the new one.
        private void SaveQuestion()
        {
            if (EditModel.CurrentQuestion.Statement == Statement || EditModel.CurrentQuestion.Answers[0] == Answer1 ||
                EditModel.CurrentQuestion.Answers[1] == Answer2 || EditModel.CurrentQuestion.Answers[2] == Answer3)
            {
                MessageBox.Show("No changes has been made.");
                return;
            }
            var index = EditModel.GetIndex(ComboBoxText);
            EditModel.CurrentQuiz.RemoveQuestion(index);
            EditModel.CurrentQuiz.AddQuestion(Statement, CorrectAnswer, Answer1, Answer2, Answer3);
            ComboBoxText = string.Empty;
            Statements = EditModel.GetStatements();
        }

        //Uses the radioButtons command-parameters to set the correctAnswer.
        private void SetCorrectAnswer(string index)
        {
            CorrectAnswer = int.Parse(index);
        }

        //If the ComboBoxText is empty it will clear all of the TextBoxes and set uncheck all radiButtons,
        //otherwise it will set the currentQuestion and set the corresponding properties.
        private void ChangeQuestion()
        {
            if (string.IsNullOrEmpty(ComboBoxText))
            {
                Statement = string.Empty;
                Answer1 = string.Empty;
                Answer2 = string.Empty;
                Answer3 = string.Empty;
                Answer1RadioButtonIsChecked = false;
                Answer2RadioButtonIsChecked = false;
                Answer3RadioButtonIsChecked = false;
                return;
            }
            EditModel.SetCurrentQuestion(ComboBoxText);

            Statement = EditModel.CurrentQuestion.Statement;
            Answer1 = EditModel.CurrentQuestion.Answers[0];
            Answer2 = EditModel.CurrentQuestion.Answers[1];
            Answer3 = EditModel.CurrentQuestion.Answers[2];

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
            }

            FinishButtonIsEnabled = true;
        }
    }
}
