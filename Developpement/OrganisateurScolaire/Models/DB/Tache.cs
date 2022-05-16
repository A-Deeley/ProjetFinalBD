using System;
using OrganisateurScolaire.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblTaches).
    /// </summary>
    public class Tache : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string caller = null) => PropertyChanged?.Invoke(this, new(caller));
        #endregion
        private string _noCours, _couleur;
        private string _coursName;
        private int _NoCategorie;


        public int ID { get; init; }
        public List<Rappel> Rappels { get; set; }
        public string Statut { get; init; }
        public string Titre { get; set; }
        public DateTime? DateDebut { get; init; }
        public DateTime DateFin { get; set; }
        public string Description { get; set; }
        public Brush Background { get; set; }
        public string NoCours
        {
            get => _noCours;
            set { _noCours = value; CoursName = new DAL().CoursFactory().GetName(value); OnPropertyChanged(); }
        }
        public int NoCategorie
        {
            get { return _NoCategorie; }
            set { _NoCategorie = value; Categorie = new DAL().CategorieFactory().Get(value); OnPropertyChanged(); }
        }
        public Categorie Categorie { get; set; }
        public string CoursName 
        {
            get => _coursName;
            set { _coursName = value; OnPropertyChanged(); }
        }


        public Tache(int iD , string titre, DateTime dateFin, string description, string noCours, Categorie categorie)
        {
            ID = iD;
            Titre = titre;
            DateFin = dateFin;
            Description = description;
            NoCours = noCours;
            Categorie = categorie;

        }
        public Tache() {

        }

        public bool Contains(string value)
        {
            return $"{ID} {Statut} {Titre} {Description} {CoursName} {Categorie.Nom} {DateDebut} {DateFin} {NoCours}".ToLower().Contains(value.ToLower());
        }

        public override string ToString()
        {
            return CoursName;
        }
    }
}
