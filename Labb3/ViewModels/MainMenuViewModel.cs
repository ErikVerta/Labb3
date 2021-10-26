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

        private async void OpenPlayView(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please choose a quiz to play.");
                return;
            }
            MainWindowViewModel.SelectedViewModel = new PlayViewModel(new PlayModel(await FileManagerModel.LoadFileAsync(title)), MainWindowViewModel);
        }

        private void OpenCreateView()
        {
            MainWindowViewModel.SelectedViewModel = new CreateViewModel(MainWindowViewModel);
        }

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
