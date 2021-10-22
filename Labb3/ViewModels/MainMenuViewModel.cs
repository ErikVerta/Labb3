using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class MainMenuViewModel : ObservableObject
    {
        public ICommand PlayCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand EditCommand { get; }

        private object _selectedViewModel;

        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public MainMenuViewModel()
        {
            PlayCommand = new RelayCommand(OpenPlayMenuView);
            CreateCommand = new RelayCommand(OpenCreateMenuView);
            EditCommand = new RelayCommand(OpenEditMenuView);
        }

        private void OpenPlayMenuView()
        {
            SelectedViewModel = new PlayMenuViewModel();
        }
        private void OpenCreateMenuView()
        {
            SelectedViewModel = new CreateViewModel();
        }
        private void OpenEditMenuView()
        {
            SelectedViewModel = new EditMenuViewModel();
        }
    }
}
