using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb3.Models
{
    internal sealed class Category: ObservableObject
    {
        public string Name { get; }
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public Category(string name)
        {
            Name = name;
            IsChecked = false;
        }
    }
}
