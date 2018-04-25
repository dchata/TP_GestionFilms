using GestionFilms.DBLib;
using MVVMLib.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFilms.Client.ViewModels
{
    public class ViewModelFilms : ViewModelDbList<NetflexEntities, Film>
    {
        #region Fields
        private ViewModelFilmGenre _VMFilmGenre;
        private ObservableCollection<Film> _FilteredItemsSource;
        private string _Search;
        #endregion Fields

        #region Properties
        public ViewModelFilmGenre VMFilmGenre { get => _VMFilmGenre; private set => SetProperty(nameof(VMFilmGenre), ref _VMFilmGenre, value); }
        public ObservableCollection<Film> FilteredItemsSource { get => _FilteredItemsSource; private set => SetProperty(nameof(FilteredItemsSource), ref _FilteredItemsSource, value); }
        public string Search { get => _Search; set => SetProperty(nameof(Search), ref _Search, value); }
        #endregion Properties

        #region Constructors
        public ViewModelFilms()
        {
            VMFilmGenre = new ViewModelFilmGenre(Entities);
            FilteredItemsSource = new ObservableCollection<Film>(ItemsSource);
            ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
        }

        #endregion Constructors

        #region Methods
        protected override void OnPropertyChanging(string propertyName)
        {
            base.OnPropertyChanging(propertyName);

            switch (propertyName)
            {
                case nameof(ItemsSource):
                    if (ItemsSource != null)
                    {
                        ItemsSource.CollectionChanged -= ItemsSource_CollectionChanged;
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(ItemsSource):
                case nameof(Search):
                    if (ItemsSource != null)
                    {
                        ItemsSource.CollectionChanged -= ItemsSource_CollectionChanged;
                        ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
                        //Si la propriété ItemsSource ou Search changent, on réinitialise FilteredItemsSource
                        SetFilteredItemsSource();
                    }
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        ///     Méthode pour filtrer si un film doit être visible en fonction du texte de recherche.
        /// </summary>
        /// <param name="f">Film à filtrer.</param>
        /// <returns>Détermine si le film doit être visible.</returns>
        private bool Filter(Film f) => f.Name?.ToLower()?.Contains(Search.ToLower()) == true || f.Genre?.Name?.ToLower()?.Contains(Search.ToLower()) == true;

        /// <summary>
        ///     Initialise la collection de films filtrée.
        /// </summary>
        private void SetFilteredItemsSource()
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                FilteredItemsSource = new ObservableCollection<Film>(ItemsSource.Where(f => Filter(f)));
            }
            else
            {
                FilteredItemsSource = new ObservableCollection<Film>(ItemsSource);
            }
        }

        private void ItemsSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                //Dans le cas où un ou plusieurs éléments ont été ajoutés à ItemsSource
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Film film in e.NewItems)
                    {
                        //Si il n'y a pas de texte de recherche ou si la personen doit être visible
                        if (string.IsNullOrWhiteSpace(Search) || Filter(film))
                        {
                            //On ajoute la personne à la collection filtrée.
                            FilteredItemsSource.Add(film);
                        }
                    }
                    break;
                //Dans le cas où un ou plusieurs éléments ont été supprimés à ItemsSource
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (Film film in e.OldItems)
                    {
                        //Si la collection filtrée contient la personne
                        if (FilteredItemsSource.Contains(film))
                        {
                            //On enlève la personne.
                            FilteredItemsSource.Remove(film);
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    //Sur l'événement Reset, on reconstruit la collection filtrée.
                    SetFilteredItemsSource();
                    break;
                default:
                    break;
            }
        }

        protected override void AddItem_Execute(object parameter)
        {
            base.AddItem_Execute(parameter);

            this.SelectedItem.Name = "Nouveau film";
            this.SelectedItem.Watched = false;

        }

        //On peut ajouter seulement si on est pas en cours de recherche.
        protected override bool AddItem_CanExecute(object parameter) => base.AddItem_CanExecute(parameter) && string.IsNullOrWhiteSpace(Search);

        protected override void Refresh_Execute(object parameter)
        {
            base.Refresh_Execute(parameter);

            //Include permet de charger également les genres associés aux Films.
            this.DbSet.Include(nameof(Film.Genre)).ToList(); //Charge les genres de film.
        }
        #endregion Methods
    }
}
