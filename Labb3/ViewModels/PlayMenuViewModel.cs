using System.Windows.Input;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class PlayMenuViewModel : ObservableObject
    {
        public ICommand OpenPlayCommand { get; }

        public MainMenuViewModel MainMenuViewModel { get; }

        public string[] QuizTitles { get; }

        public PlayMenuViewModel(MainMenuViewModel mainMenuViewModel)
        {
            OpenPlayCommand = new RelayCommand<string>(OpenPlayView);
            QuizTitles = Quiz.GetQuizTitles();
            MainMenuViewModel = mainMenuViewModel;
        }

        private async void OpenPlayView(string title)
        {
            MainMenuViewModel.SelectedViewModel = new PlayViewModel(new QuizGameModel(await FileManagerModel.LoadFileAsync(title)), MainMenuViewModel);
        }
    }
}
