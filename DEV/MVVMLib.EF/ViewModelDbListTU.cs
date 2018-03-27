using MVVMLib.Core;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace MVVMLib.ViewModels
{ 
    public class ViewModelDbList<T, U> : ViewModelDb<T> 
        where T : DbContext, new() 
        where U : ObservableObject, new()
    {
        #region Fields
        private ObservableCollection<U> _SelectedItems;
        private U _SelectedItem;
        private DelegateCommand _AddItemCommand;
        private DelegateCommand _RemoveItemCommand;
        #endregion

        #region Properties
        public DelegateCommand AddItemCommand { get => _AddItemCommand; set => _AddItemCommand = value; }
        public DelegateCommand RemoveItemCommand { get => _RemoveItemCommand; set => _RemoveItemCommand = value; }

        protected DbSet<U> DbSet => Entities.Set<U>();
        public ObservableCollection<U> ItemsSource => DbSet.Local;
        public ObservableCollection<U> SelectedItems { get => _SelectedItems; private set => SetProperty(nameof(SelectedItems), ref _SelectedItems, value); }
        public U SelectedItem { get => _SelectedItem; set => SetProperty(nameof(SelectedItem), ref _SelectedItem, value); }
        #endregion

        #region Constructors
        public ViewModelDbList()
        {
            InitCommands();
            SelectedItems = new ObservableCollection<U>();
            RefreshCommand.Execute(null);
        }

        public ViewModelDbList(T entities)
            :base(entities)
        {
            InitCommands();
            SelectedItems = new ObservableCollection<U>();
            RefreshCommand.Execute(null);
        }
        #endregion

        #region Methods
        private void InitCommands()
        {
            AddItemCommand = new DelegateCommand(AddItem_Execute, AddItem_CanExecute);
            RemoveItemCommand = new DelegateCommand(RemoveItem_Execute, RemoveItem_CanExecute);
        }
        
        #region Add Item
        protected virtual void AddItem_Execute(object parameter)
        {
            U entity = new U();

            ItemsSource.Add(entity);
            SelectedItem = entity;
        }
        protected virtual bool AddItem_CanExecute(object parameter) => true;
        #endregion
        
        #region Remove Item
        protected virtual void RemoveItem_Execute(object parameter)
        {
            if (parameter is U entityToDelete)
                ItemsSource.Remove(entityToDelete);
            else if (SelectedItems.Any())
                foreach (U item in SelectedItems.ToList())
                    ItemsSource.Remove(SelectedItem);
        }
        protected virtual bool RemoveItem_CanExecute(object parameter) => SelectedItems.Any() || parameter is U;
        #endregion

        #region Refresh 
        protected override void Refresh_Execute(object parameter)
        {
            base.Refresh_Execute(parameter);

            RefreshDbSet(DbSet);
        }
        #endregion
        #endregion
    }
}
