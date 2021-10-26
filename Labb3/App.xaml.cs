using System.Windows;
using Labb3.ViewModels;
using Labb3.Views;

namespace Labb3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindowViewModel = new MainWindowViewModel();
            var mainMenu = new MainWindow {DataContext = mainWindowViewModel};

            mainMenu.Show();
        }
    }
}
