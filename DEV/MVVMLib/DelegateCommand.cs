using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMLib.Core
{
    public class DelegateCommand : ICommand
    {
        #region Fields
        private Action<object> _Execute;
        private Func<object, bool> _CanExecute;
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Constructors
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter) => _CanExecute != null ? _CanExecute(parameter) : true;

        public void Execute(object parameter) => _Execute(parameter);
        #endregion
    }
}
