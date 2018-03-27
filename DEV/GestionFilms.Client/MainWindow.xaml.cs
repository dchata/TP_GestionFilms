using GestionFilms.Client.ViewModels;
using MahApps.Metro.Controls;

namespace GestionFilms.Client
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelMain();
        }
    }
}
