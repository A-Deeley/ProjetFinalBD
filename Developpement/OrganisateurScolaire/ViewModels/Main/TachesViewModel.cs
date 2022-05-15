using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OrganisateurScolaire.DataAccessLayer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.ViewModels
{
    public class TachesViewModel : ViewModelBase
    {
        private ObservableCollection<Tache> _allTaches;

        public ObservableCollection<Tache> AllTaches
        {
            get => _allTaches;
            set { _allTaches = value; OnPropertyChanged(); }
        }

        public TachesViewModel() { }

        public TachesViewModel(Session currentSession)
        {
            AllTaches = new(new DAL().TacheFactory().GetAllBySession(currentSession));
        }
        public void UpdateSessionDetails(Session selectedSession)
        {
            DAL dal = new();
            AllTaches = new(dal.TacheFactory().GetAllBySession(selectedSession));
        }
    }
}
