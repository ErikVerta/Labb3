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

        public MainWindowViewModel()
        {
            _selectedViewModel = new MainMenuViewModel(this);
        }
    }
}
