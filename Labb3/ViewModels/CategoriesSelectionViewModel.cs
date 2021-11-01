using System.Collections.ObjectModel;
using System.Windows.Input;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class CategoriesSelectionViewModel
    {
        public PlayModel PlayModel { get; }
        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }
        public MainWindowViewModel MainWindowViewModel { get; }

        public CategoriesSelectionViewModel(PlayModel playModel, MainWindowViewModel mainWindowViewModel)
        {
            PlayModel = playModel;
            OkCommand = new RelayCommand(LoadPlayView);
            CancelCommand = new RelayCommand(LoadMainMenuView);
            MainWindowViewModel = mainWindowViewModel;
        }

        private void LoadMainMenuView()
        {
            MainWindowViewModel.SelectedViewModel = new MainMenuViewModel(MainWindowViewModel);
        }

        private void LoadPlayView()
        {
            var tempChosenCategories = new ObservableCollection<string>();
            foreach (var category in PlayModel.CurrentQuiz.Categories)
            {
                if (category.IsChecked && !tempChosenCategories.Contains(category.Name))
                {
                    tempChosenCategories.Add(category.Name);
                }
            }

            PlayModel.ChosenCategories = tempChosenCategories;
            MainWindowViewModel.SelectedViewModel = new PlayViewModel(PlayModel, MainWindowViewModel);
        }
    }
}
