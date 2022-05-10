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
        private Session _selectedSession;
        private ObservableCollection<Session> _sessions;

        public Session SelectedSession
        {
            get => _selectedSession;
            set { _selectedSession = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Session> Sessions
        {
            get => _sessions;
            set { _sessions = value; OnPropertyChanged(); }
        }

        public AccueilViewModel()
        {
            PropertyChanged += OnSelectedSessionChanged;
            DAL dal = new();

            Sessions = new(dal.SessionFactory().GetAllWithoutInfo());
        }

        private void OnSelectedSessionChanged(object sender, PropertyChangedEventArgs e)
        {
            // Check if the property that raised the event is the SelectedSession.
            if (e.PropertyName != nameof(SelectedSession)) return;

            // If the programme is not null it has already been set, ignore.
            if (SelectedSession.Programme is not null) return;

            DAL dal = new();

            SelectedSession.Programme = dal.ProgrammeFactory().GetBySession(SelectedSession);
        }
    }
}
