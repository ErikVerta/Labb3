using System.Windows;
using System.Windows.Input;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class MainMenuViewModel : ObservableObject
    {
        public ICommand OpenPlayCommand { get; }
        public ICommand OpenEditCommand { get; }
        public ICommand OpenCreateCommand { get; }

        public MainWindowViewModel MainWindowViewModel { get; }

        public string[] QuizTitles { get; }

        public MainMenuViewModel(MainWindowViewModel mainWindowViewModel)
        {
            OpenPlayCommand = new RelayCommand<string>(OpenPlayView);
            OpenCreateCommand = new RelayCommand(OpenCreateView);
            OpenEditCommand = new RelayCommand<string>(OpenEditView);
            QuizTitles = Quiz.GetQuizTitles();
            MainWindowViewModel = mainWindowViewModel;
        }

        //Uses the PlayButton command-parameter to make sure that a quiz has been selected then changes the SelectedViewModel to PlayViewModel.
        private async void OpenPlayView(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please choose a quiz to play.");
                return;
            }
            MainWindowViewModel.SelectedViewModel = new PlayViewModel(new PlayModel(await FileManagerModel.LoadFileAsync(title)), MainWindowViewModel);
        }
        //Changes the SelectedViewModel to CreateViewModel.
        private void OpenCreateView()
        {
            MainWindowViewModel.SelectedViewModel = new CreateViewModel(MainWindowViewModel);
        }

        //Uses the EditButton command-parameter to make sure that a quiz has been selected then changes the SelectedViewModel to EditViewModel.
        private async void OpenEditView(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please choose a quiz to edit.");
                return;
            }
            MainWindowViewModel.SelectedViewModel = new EditViewModel(new EditModel(await FileManagerModel.LoadFileAsync(title)), MainWindowViewModel);
        }
    }
}
