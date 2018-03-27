//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionFilms.DBLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Film
    {
        public int Id { get; set; }
        public int IdGenre { get; set; }
        public string Name { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<double> Score { get; set; }
        public string File { get; set; }
        public string Tag { get; set; }
        public Nullable<bool> Watched { get; set; }
    
        public virtual Genre Genre { get; set; }
    }
}
