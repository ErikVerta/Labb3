using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.ViewModels
{
    internal sealed class MainWindowViewModel : ObservableObject
    {
        private object _selectedViewModel;

        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        //Sets the SelectedViewModel to MainMenuViewModel, this will be triggered on startup.
        public MainWindowViewModel()
        {
            SelectedViewModel = new MainMenuViewModel(this);
        }
    }
}
