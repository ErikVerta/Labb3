using System.Windows.Input;
using Labb3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3.ViewModels
{
    internal sealed class CreateViewModel : ObservableObject
    {
        public ICommand CheckTitleCommand { get; set; }
        public ICommand SaveQuizCommand { get; set; }

        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public CreateViewModel()
        {
            IsVisible = false;
            CheckTitleCommand = new RelayCommand<string>(CheckTitleAvailability);
            SaveQuizCommand = new RelayCommand(SaveQuiz);
        }

        private void CheckTitleAvailability(string inputTitle)
        {
            if (!ValidateTitle(inputTitle))
            {
                IsVisible = true;
                return;
            }

            IsVisible = false;
        }

        private bool ValidateTitle(string title)
        {
            var allTitles = Quiz.GetQuizTitles();
            foreach (var item in allTitles)
            {
                if (item.ToLower() == title.ToLower())
                {
                    return false;
                }
            }

            return true;
        }

        private void SaveQuiz()
        {

        }
    }
}
