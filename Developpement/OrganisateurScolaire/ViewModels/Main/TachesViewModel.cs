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
        private string _tacheSearchBar;
        private List<Cours> _searchByCoursOptions;
        private List<Categorie> _searchByCategorieOptions;
        private Cours _searchByCoursSelected;
        private Categorie _searchByCategorieSelected;
        private Session _currentSession;
        private ObservableCollection<Tache> _allTaches;

        public Session CurrentSession
        {
            get => _currentSession;
            set { _currentSession = value; OnPropertyChanged(); }
        }
        public List<Cours> SearchByCoursOptions
        {
            get => _searchByCoursOptions;
            set { _searchByCoursOptions = value; OnPropertyChanged(); }
        }
        public List<Categorie> SearchByCategorieOptions
        {
            get => _searchByCategorieOptions;
            set { _searchByCategorieOptions = value; OnPropertyChanged(); }
        }
        public Cours SearchByCoursSelected
        {
            get => _searchByCoursSelected;
            set { _searchByCoursSelected = value; OnPropertyChanged(); }
        }
        public Categorie SearchByCategorieSelected
        {
            get => _searchByCategorieSelected;
            set { _searchByCategorieSelected = value; OnPropertyChanged(); }
        }
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
        public ICommand ResetSearchButton { get; init; }
        public string TacheSearchBar
        {
            get => _tacheSearchBar;
            set { _tacheSearchBar = value; OnPropertyChanged(); }
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
        #region TacheSearchButton
        private void ResetSearchButton_Execute(object sender)
        {
            TacheSearchBar = string.Empty;
            SearchByCategorieSelected = null;
            SearchByCoursSelected = null;
        }
        private bool ResetSearchButton_CanExecute(object sender) => !string.IsNullOrEmpty(TacheSearchBar) || SearchByCategorieSelected is not null || SearchByCoursSelected is not null;
        #endregion
        #region Supprimer
        ICommand _Supprimer;
        public ICommand Supprimer
        {
            get { return _Supprimer; }
            set { _Supprimer = value; }
        }
        private void Supprimer_Execute(object sender)
        {
            DAL dal = new DAL();
            dal.TacheFactory().DeleteTache(TacheSelectionner);
            AllTaches = new (dal.TacheFactory().GetTacheAujourdhui());
        }
        private bool Supprimer_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        public TachesViewModel() 
        {
            OuvrirAjouter = new CommandeRelais(OuvrirAjouter_Execute, OuvrirAjouter_CanExecute);
            OuvrirModifier = new CommandeRelais(OuvrirModifier_Execute, OuvrirModifier_CanExecute);
            OuvrirDetail = new CommandeRelais(OuvrirDetail_Execute, OuvrirDetail_CanExecute);
            ResetSearchButton = new CommandeRelais(ResetSearchButton_Execute, ResetSearchButton_CanExecute);
            Supprimer = new CommandeRelais(Supprimer_Execute, Supprimer_CanExecute);
            PropertyChanged += OnSearchFiltersChanged;
            AllTaches = new(new DAL().TacheFactory().GetTacheAujourdhui());
            nbTache = new DAL().ProcedureFactory().Get();
        }

        

        public TachesViewModel(Session currentSession)
            :this()
        {
            DAL dal = new();
            CurrentSession = currentSession;
            AllTaches = new(dal.TacheFactory().GetAllBySession(CurrentSession));
            SearchByCategorieOptions = new(dal.CategorieFactory().GetAll());
            SearchByCoursOptions = new(dal.CoursFactory().GetBySession(CurrentSession));
        }
        public void UpdateSessionDetails(Session selectedSession)
        {
            try
            {
                DAL dal = new();
                CurrentSession = selectedSession;
                AllTaches = new(dal.TacheFactory().GetAllBySession(CurrentSession));
                SearchByCategorieOptions = new(dal.CategorieFactory().GetAll());
                SearchByCoursOptions = new(dal.CoursFactory().GetBySession(CurrentSession));
            }catch(Exception ex) { }
        }
        private async void OnSearchFiltersChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                string[] properties = new string[]
                {
                nameof(TacheSearchBar),
                nameof(SearchByCategorieSelected),
                nameof(SearchByCoursSelected)
                };

                if (!properties.Contains(e.PropertyName)) return;
                if (CurrentSession is null) return;

                AllTaches =
                    new(new DAL()
                    .TacheFactory()
                    .GetAllBySession(CurrentSession)
                    .Where(
                        (tache) => Matches(tache)
                    ));
            });
        }

        private bool Matches(Tache t)
        {
            bool returnTache = true;

            if (!string.IsNullOrEmpty(TacheSearchBar))
                returnTache = t.Contains(TacheSearchBar);

            if (returnTache && SearchByCategorieSelected is not null)
                returnTache = t.Categorie.ID == SearchByCategorieSelected.ID;

            if (returnTache && SearchByCoursSelected is not null)
                returnTache = t.NoCours == SearchByCoursSelected.Numero;

            return returnTache;
        }
    }
}
 