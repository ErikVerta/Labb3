using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class ResultViewModel : ObservableObject
    {
        public ICommand FinishCommand { get; }
        private int _correctAnswers;

        public int CorrectAnswers
        {
            get => _correctAnswers;
            set => SetProperty(ref _correctAnswers, value);
        }

        private int _totalQuestionsCount;

        public int TotalQuestionCount
        {
            get => _totalQuestionsCount;
            set => SetProperty(ref _totalQuestionsCount, value);
        }

        public MainWindowViewModel MainWindowViewModel { get; }

        public ResultViewModel(int correctAnswers, int totalQuestionsCount, MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
            _correctAnswers = correctAnswers;
            _totalQuestionsCount = totalQuestionsCount;
            FinishCommand = new RelayCommand(OpenMainMenuView);
        }

        //Changes SelectedViewModel to MainMenuViewModel.
        private void OpenMainMenuView()
        {
            MainWindowViewModel.SelectedViewModel = new MainMenuViewModel(MainWindowViewModel);
        }
    }
}
