using MVVMLib.Core;
using MVVMLib.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFilms.Client.ViewModels
{
    public class ViewModelMain : ViewModelList<ViewModelBase>
    {
        #region Fields
        private ViewModelBase _SelectedViewModel;
        private DelegateCommand _NavigateCommand;
        DelegateCommand _ExitCommand;
        #endregion Fields

        #region Properties
        public ViewModelBase SelectedViewModel { get => _SelectedViewModel; set => SetProperty(nameof(SelectedViewModel), ref _SelectedViewModel, value); }
        public DelegateCommand NavigateCommand => _NavigateCommand;
        public DelegateCommand ExitCommand => _ExitCommand;
        #endregion Properties

        #region Constructors
        public ViewModelMain()
        {
            SelectedViewModel = new ViewModelHome();
            _NavigateCommand = new DelegateCommand(Navigate_Execute, Navigate_CanExecute);
            _ExitCommand = new DelegateCommand((parameter) => App.Current.Shutdown());
        }

        #endregion Constructors

        #region Methods
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

        protected override void AddItem_Execute(object parameter)
        {
            if (parameter is Type viewModelType &&
                viewModelType.GetConstructor(Type.EmptyTypes).Invoke(null) is ViewModelBase viewModel)
            {
                ItemsSource.Add(viewModel);
                SelectedItem = viewModel;
            }
        }
        #endregion Methods
    }
}
