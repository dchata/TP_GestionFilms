using GestionFilms.DBLib;
using MVVMLib.Core;
using MVVMLib.EF;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;

namespace GestionFilms.Client.ViewModels
{
    public class ViewModelMain : ViewModelList<ViewModelBase>
    {
        #region Fields
        private ViewModelBase _SelectedViewModel;
        private DelegateCommand _NavigateCommand;
        private DelegateCommand _PlayCommand;
        DelegateCommand _ExitCommand;
        #endregion Fields

        #region Properties
        public ViewModelBase SelectedViewModel { get => _SelectedViewModel; set => SetProperty(nameof(SelectedViewModel), ref _SelectedViewModel, value); }
        public DelegateCommand NavigateCommand => _NavigateCommand;
        public DelegateCommand ExitCommand => _ExitCommand;
        public DelegateCommand PlayCommand => _PlayCommand;
        #endregion Properties

        #region Constructors
        public ViewModelMain()
        {
            SelectedViewModel = new ViewModelHome();
            _NavigateCommand = new DelegateCommand(Navigate_Execute, Navigate_CanExecute);
            _PlayCommand = new DelegateCommand(Play_Execute, Play_CanExecute);
            _ExitCommand = new DelegateCommand((parameter) => App.Current.Shutdown());
        }
        #endregion Constructors

        #region Methods
        #region Navigate

        private bool Navigate_CanExecute(object parameter) => parameter is ViewModelBase || (parameter is Type vmType && typeof(ViewModelBase).IsAssignableFrom(vmType));

        private void Navigate_Execute(object parameter)
        {
            if (parameter is ViewModelBase vm)
            {
                this.SelectedViewModel = vm;
            }
            else if (parameter is Type vmType && typeof(ViewModelBase).IsAssignableFrom(vmType)
                    && vmType.GetConstructor(Type.EmptyTypes).Invoke(null) is ViewModelBase viewModel)
            {
                this.SelectedViewModel = viewModel;
            }
        }
        #endregion

        #region Play
        private void Play_Execute(object parameter)
        {
            if (parameter is Film film)
            {
                //using (Process vlc = new Process())
                //{
                //    vlc.StartInfo.FileName = film.File;
                //    vlc.StartInfo.Arguments = "-vvv " + film.File;
                //    vlc.Start();
                //}

                GestionFilm.MoviePlayer.MainWindow viewer = new GestionFilm.MoviePlayer.MainWindow(film.File);

                viewer.Show();
                
                film.Watched = true;
            }
        }

        private bool Play_CanExecute(object parameter)
        {
            if (parameter is Film film)
            {
                if (File.Exists(film.File))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

        #region AddItem
        protected override void AddItem_Execute(object parameter)
        {
            if (parameter is Type viewModelType &&
                viewModelType.GetConstructor(Type.EmptyTypes).Invoke(null) is ViewModelBase viewModel)
            {
                ItemsSource.Add(viewModel);
                SelectedItem = viewModel;
            }
        }
        #endregion
        #endregion Methods
    }
}
