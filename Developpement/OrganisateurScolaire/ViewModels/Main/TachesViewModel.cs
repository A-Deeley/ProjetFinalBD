using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OrganisateurScolaire.DataAccessLayer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganisateurScolaire.VueModel;
using OrganisateurScolaire.Vue.Tache;
using System.Windows.Input;

namespace OrganisateurScolaire.ViewModels
{
    public class TachesViewModel : ViewModelBase
    {
        private Tache _TacheSelectionner;
        private int _nbTache;
        private ObservableCollection<Tache> _allTaches;

        public ObservableCollection<Tache> AllTaches
        {
            get => _allTaches;
            set { _allTaches = value; OnPropertyChanged(); }
        }
        public Tache TacheSelectionner
        {
            get { return _TacheSelectionner; }
            set { _TacheSelectionner = value; OnPropertyChanged(); }
        }
        public int nbTache
        {
            get { return _nbTache; }
            set { _nbTache = value; OnPropertyChanged(); }
        }

        #region OuvrirAjouter
        ICommand _OuvrirAjouter;
        public ICommand OuvrirAjouter
        {
            get { return _OuvrirAjouter; }
            set { _OuvrirAjouter = value; }
        }
        private void OuvrirAjouter_Execute(object sender)
        {
                var AjouterWindow = new TacheWindow();
                AjouterWindow.DataContext = new DetailTacheViewModel();
                AjouterWindow.ShowDialog();
        }
        private bool OuvrirAjouter_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region OuvrirModifier
        ICommand _OuvrirModifier;
        public ICommand OuvrirModifier
        {
            get { return _OuvrirModifier; }
            set { _OuvrirModifier = value; }
        }
        private void OuvrirModifier_Execute(object sender)
        {
            var AjouterWindow = new TacheWindow();
            AjouterWindow.DataContext = new DetailTacheViewModel(TacheSelectionner, "Modifier");
            AjouterWindow.ShowDialog();
        }
        private bool OuvrirModifier_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region OuvrirDetail
        ICommand _OuvrirDetail;
        public ICommand OuvrirDetail
        {
            get { return _OuvrirDetail; }
            set { _OuvrirDetail = value; }
        }
        private void OuvrirDetail_Execute(object sender)
        {
            var AjouterWindow = new TacheWindow();
            AjouterWindow.DataContext = new DetailTacheViewModel(TacheSelectionner, "");
            AjouterWindow.ShowDialog();
        }
        private bool OuvrirDetail_CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        public TachesViewModel() 
        {
            this.OuvrirAjouter = new CommandeRelais(OuvrirAjouter_Execute, OuvrirAjouter_CanExecute);
            this.OuvrirModifier = new CommandeRelais(OuvrirModifier_Execute, OuvrirModifier_CanExecute);
            this.OuvrirDetail = new CommandeRelais(OuvrirDetail_Execute, OuvrirDetail_CanExecute);
            AllTaches = new(new DAL().TacheFactory().GetTacheAujourdhui());
            nbTache = new DAL().ProcedureFactory().Get();
        }

        public TachesViewModel(Session currentSession)
        {
            AllTaches = new(new DAL().TacheFactory().GetAllBySession(currentSession));

            this.OuvrirAjouter = new CommandeRelais(OuvrirAjouter_Execute, OuvrirAjouter_CanExecute);
            this.OuvrirModifier = new CommandeRelais(OuvrirModifier_Execute, OuvrirModifier_CanExecute);
            this.OuvrirDetail = new CommandeRelais(OuvrirDetail_Execute, OuvrirDetail_CanExecute);
            nbTache = new DAL().ProcedureFactory().Get();
        }
        public void UpdateSessionDetails(Session selectedSession)
        {
            DAL dal = new();
            AllTaches = new(dal.TacheFactory().GetAllBySession(selectedSession));
        }
    }
}
