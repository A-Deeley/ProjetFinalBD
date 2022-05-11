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
        public string Titre { get; init; }
        public string Message { get; init; }
        public DateTime DateRappel { get; init; }
        public Brush Background { get; set; }

        public string DateRappelString { get => $"{DateRappel:dd MMM}"; }
    }
}
