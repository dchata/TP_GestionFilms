using MVVMLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GestionFilms.MoviePlayer.ViewModels
{
    public class ViewModelMain
    {
        #region Fields
        private string _File;
        private DelegateCommand _PlayCommand;
        private DelegateCommand _PauseCommand;
        private DelegateCommand _StopCommand;
        #endregion Fields

        #region Properties
        public DelegateCommand PlayCommand => _PlayCommand;
        public DelegateCommand PauseCommand => _PauseCommand;
        public DelegateCommand StopCommand => _StopCommand;
        public string File { get => _File; set => _File = value; }
        #endregion Properties

        #region Constructors
        public ViewModelMain(string movieFile)
        {
            File = movieFile;

            _PlayCommand = new DelegateCommand(Play_Execute, Play_CanExecute); //Commande de lecture
            _PauseCommand = new DelegateCommand(Pause_Execute, Pause_CanExecute); //Commande de mise en pause
            _StopCommand = new DelegateCommand(Stop_Execute, Stop_CanExecute); //Commande d'arrêt

            MessageBox.Show("Cliquez sur Play pour démarrer la lecture du fichier vidéo !");
        }
        #endregion Constructors

        #region Methods
        #region Play
        private void Play_Execute(object parameter)
        {
            if (parameter is MediaElement media)
            {
                media = (MediaElement)media.FindName("netflexPlayer"); //pour récupérer le MediaElement utilisé dans la MainWindow

                media.Visibility = System.Windows.Visibility.Visible; //on affiche le fichier vidéo au cas où l'utilisateur a précedemment cliqué sur stop
                media.Play();//lecture du fichier vidéo
            }
        }
        private bool Play_CanExecute(object parameter) => true;
        #endregion

        #region Pause
        private void Pause_Execute(object parameter)
        {
            if (parameter is MediaElement media)
            {
                media = (MediaElement)media.FindName("netflexPlayer");

                media.Pause(); //on met en pause la lecture du fichier vidéo
            }
        }
        private bool Pause_CanExecute(object parameter) => true;
        #endregion

        #region Stop
        private void Stop_Execute(object parameter)
        {
            if (parameter is MediaElement media)
            {
                media.Stop(); //on arrête la lecture du fichier vidéo
                media.Visibility = System.Windows.Visibility.Hidden; //On masque le fichier vidéo
            }
        }
        private bool Stop_CanExecute(object parameter) => true;
        #endregion
        #endregion Methods
    }
}
