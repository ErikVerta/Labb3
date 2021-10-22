using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Labb3.Models;

namespace Labb3.Views
{
    /// <summary>
    /// Interaction logic for PlayMenuView.xaml
    /// </summary>
    public partial class PlayMenuView : UserControl
    {
        public PlayMenuView()
        {
            InitializeComponent();
            QuizTitlesBox.ItemsSource = Quiz.GetQuizTitles();
        }
    }
}
