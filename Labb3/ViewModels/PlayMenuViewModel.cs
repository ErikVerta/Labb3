using System.Windows.Input;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class PlayMenuViewModel : ObservableObject
    {
        public ICommand OpenPlayCommand { get; }

        private object _selectedViewModel;

        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public PlayMenuViewModel()
        {
            OpenPlayCommand = new RelayCommand(OpenPlayView);
        }

        private void OpenPlayView()
        {
            SelectedViewModel = new PlayViewModel(new QuizGameModel(FileManagerModel.LoadFile()));
        }
    }
}
