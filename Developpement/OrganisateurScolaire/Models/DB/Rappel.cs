using System;
using System.Windows.Media;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblRappels).
    /// </summary>
    public class Rappel
    {

        public int ID { get; init; }
        public int IdTache { get; set; }
        public string Titre { get; init; }
        public string Message { get; init; }
        public DateTime DateRappel { get; init; }
        public Brush Background { get; set; }

        public string DateRappelString { get => $"{DateRappel:dd MMM}"; }

        public Rappel(int idTache, string titre, string message, DateTime dateRappel)
        {
            IdTache = idTache;
            Titre = titre;
            Message = message;
            DateRappel = dateRappel;
        }
        public Rappel() { }
    }
}
