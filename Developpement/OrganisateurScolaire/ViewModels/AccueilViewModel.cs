using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganisateurScolaire.DataAccessLayer;

namespace OrganisateurScolaire.ViewModels
{
    public class AccueilViewModel : ViewModelBase
    {

        private ObservableCollection<Session> _sessions;

        public ObservableCollection<Session> Sessions
        {
            get => _sessions;
            set { _sessions = value; OnPropertyChanged(); }
        }

        public AccueilViewModel()
        {
            DAL dal = new();

            Sessions = new(dal.SessionFactory().GetAllWithoutInfo().Result);
        }
    }
}
