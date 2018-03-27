using GestionFilms.DBLib;
using MVVMLib.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFilms.Client.ViewModels
{
    public class ViewModelFilmGenre : ViewModelDbList<NetflexEntities, Genre>
    {
        #region Constructors
        public ViewModelFilmGenre() { }

        public ViewModelFilmGenre(NetflexEntities entities)
            : base(entities)
        {

        }
        #endregion Constructors

        #region Methods
        protected override void AddItem_Execute(object parameter)
        {
            base.AddItem_Execute(parameter);

            this.SelectedItem.Name = "Nouveau genre";
        }

        protected override void Refresh_Execute(object parameter)
        {
            base.Refresh_Execute(parameter);

            this.DbSet.OrderBy(fg => fg.Name).ToList(); //Charge les genres de film.
        }
        #endregion Methods
    }
}
