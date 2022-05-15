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

        private string _noCours;
        private string _coursName;
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
        public string Categorie { get; set; }
        public string CoursName 
        {
            get => _coursName;
            set { _coursName = value; OnPropertyChanged(); }
        }


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
