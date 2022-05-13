using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblTaches).
    /// </summary>
    public class Tache
    {

        public int ID { get; init; }
        public List<Rappel> Rappels { get; set; }
        public string Statut { get; init; }
        public string Titre { get; set; }
        public DateTime? DateDebut { get; init; }
        public DateTime DateFin { get; set; }
        public string Description { get; set; }
        public Brush Background { get; set; }
        public string NoCours { get; set; }
        public string Categorie { get; set; }


        public Tache(int iD , string titre, DateTime dateFin, string description, string noCours, string categorie)
        {
            ID = iD;
            Titre = titre;
            DateFin = dateFin;
            Description = description;
            NoCours = noCours;
            Categorie = categorie;
        }
        public Tache() { }
    }
}
