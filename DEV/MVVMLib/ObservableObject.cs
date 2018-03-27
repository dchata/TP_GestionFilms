using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLib.Core
{
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Events
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        public void SetProperty<T>(string propertyName, ref T obj, T value)
        {
            if ((obj == null && value != null) || (obj != null && !obj.Equals(value)))
            {
                OnPropertyChanging(propertyName);
                obj = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
