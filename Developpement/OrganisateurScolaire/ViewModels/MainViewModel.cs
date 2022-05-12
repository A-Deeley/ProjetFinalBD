using OrganisateurScolaire.DataAccessLayer;
using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public AccueilViewModel Accueil { get; init; }
        public TachesViewModel Taches { get; init; }

        private Session _selectedSession;

        public Session SelectedSession
        {
            get => _selectedSession;
            set { _selectedSession = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            Accueil = new();
            PropertyChanged += OnSelectedSessionChanged;
            Taches = new();
        }

        
        /// <summary>
        /// When the selected Session is changed, make sure the data is loaded and reset the UI for the Cours and Rappels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedSessionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedSession)) return;
            // If the programme is null, fill it the first time.
            if (SelectedSession.Programme is null)
            {
                DAL dal = new();
                SelectedSession.Programme = dal.ProgrammeFactory().GetBySession(SelectedSession);
            }

            Accueil.UpdateSessionDetails(SelectedSession);
        }
    }
}
