using MVVMLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLib.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        #region Fields
        private object _Header;
        #endregion

        #region Properties
        public object Header { get => _Header; protected set => SetProperty(nameof(Header), ref _Header, value); }
        #endregion
    }
}
