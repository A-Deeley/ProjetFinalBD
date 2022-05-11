using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganisateurScolaire.DataAccessLayer;
using System.ComponentModel;

namespace OrganisateurScolaire.ViewModels
{
    public class AccueilViewModel : ViewModelBase
    {
       
        private ObservableCollection<Session> _sessions;

        private Cours _selectedCours;
        private ObservableCollection<Cours> _cours;

        private ObservableCollection<Rappel> _rappelsOfSelectedCours;

        
        public ObservableCollection<Session> Sessions
        {
            get => _sessions;
            set { _sessions = value; OnPropertyChanged(); }
        }

        public Cours SelectedCours
        {
            get => _selectedCours;
            set { _selectedCours = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Cours> Cours
        {
            get => _cours;
            set { _cours = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Rappel> RappelsOfSelectedCours
        {
            get => _rappelsOfSelectedCours;
            set { _rappelsOfSelectedCours = value; OnPropertyChanged(); }
        }

        public AccueilViewModel()
        {
            PropertyChanged += OnSelectedSessionChanged;
            PropertyChanged += OnSelectedCoursChanged;
            DAL dal = new();

            Sessions = new(dal.SessionFactory().GetAllWithoutInfo());
        }

        /// <summary>
        /// When the cours is changed, set the rappels to be the ones for the selected cours.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedCoursChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedCours)) return;
            if (SelectedCours is null) return;

            if (SelectedCours.Taches is null)
            {
                DAL dal = new();
                SelectedCours.Taches = new(dal.TacheFactory().GetTacheByCours(SelectedCours));
            }

            RappelsOfSelectedCours = new(SelectedCours.GetAllRappels());
        }

        /// <summary>
        /// When the selected Session is changed, make sure the data is loaded and reset the UI for the Cours and Rappels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedSessionChanged(object sender, PropertyChangedEventArgs e)
        {
            // Check if the property that raised the event is the SelectedSession.
            if (e.PropertyName != nameof(SelectedSession)) return;

            // If the programme is null, fill it the first time.
            if (SelectedSession.Programme is null)
            {
                DAL dal = new();
                SelectedSession.Programme = dal.ProgrammeFactory().GetBySession(SelectedSession);
            }

            RappelsOfSelectedCours = null;
            SelectedCours = null;
            Cours = new(SelectedSession.Programme.Cours);
        }
    }
}
