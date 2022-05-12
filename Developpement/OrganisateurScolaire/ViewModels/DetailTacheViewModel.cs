using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using OrganisateurScolaire.Models;
using OrganisateurScolaire.DataAccessLayer;
using OrganisateurScolaire.Vue;
using OrganisateurScolaire.Vue.Tache;
using OrganisateurScolaire.VueModel;

namespace OrganisateurScolaire.ViewModels
{
    class DetailTacheViewModel : ViewModelBase
    {
        private UserControl _UserControlAfficher, _RappelOption;
        private DAL dal;
        #region Attribut tache
        private string _TitreTache, _DescriptionTache, _StatutTache, _BoutonTitreTache;
        private int IdTache;
        private Cours _CoursTache;
        private Categorie _CategorieTache;
        private DateTime? _DateDebutTache;
        private DateTime _DateFinTache;
        List<Categorie> _Categories;
        List<Cours> _Cours;
        Tache tacheLocal;
        #endregion
        #region Attribut rappel
        private string _TitreRappel, _MessageRappel;
        private DateTime _DateRappel;
        List<Rappel> _Rappels;
        #endregion

        #region userControls
        public UserControl UserControlAfficher
        {
            get { return _UserControlAfficher; }
            set
            {
                _UserControlAfficher = value;
                OnPropertyChanged();
            }
        }
        public UserControl RappelOption
        {
            get { return _RappelOption; }
            set
            {
                _RappelOption = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public string BoutonTitreTache { 
            get { return _BoutonTitreTache; }
            set { _BoutonTitreTache = value;
                OnPropertyChanged();
            }
        }

        #region Propriété tache
        public string TitreTache
        {
            get { return _TitreTache; }
            set { _TitreTache = value;
                OnPropertyChanged();
            }
        }

        public Categorie CategorieTache
        {
            get { return _CategorieTache; }
            set
            { _CategorieTache = value;
                OnPropertyChanged();
            }
        }

        public Cours CoursTache
        {
            get { return _CoursTache; }
            set { _CoursTache = value;
                OnPropertyChanged();
            }
        }
        public string DescriptionTache
        {
            get { return _DescriptionTache; }
            set { _DescriptionTache = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateFinTache
        {
            get { return _DateFinTache; }
            set
            {
                _DateFinTache = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DateDebutTache
        {
            get { return _DateDebutTache; }
            set
            {
                _DateDebutTache = value;
                OnPropertyChanged();
            }
        }
        public string StatutTache
        {
            get { return _StatutTache; }
            set { _StatutTache = value;
                OnPropertyChanged();
            }
        }

        public List<Categorie> Categories
        {
            get { return _Categories; }
            set { _Categories = value;
                OnPropertyChanged();
            }
        }
        public List<Cours> LesCours
        {
            get { return _Cours; }
            set
            {
                _Cours = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Propriété Rappel
        public List<Rappel> Rappels
        {
            get { return _Rappels; }
            set { _Rappels = value;
                OnPropertyChanged();
            }
        }

        public string TitreRappel
        {
            get { return _TitreRappel; }
            set { _TitreRappel = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateRappel
        {
            get { return _DateRappel; }
            set { _DateRappel = value;
                OnPropertyChanged();
            }
        }
        public string MessageRappel
        {
            get { return _MessageRappel; }
            set { _MessageRappel = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Bouton
        #region Ajouter tache
        ICommand _AjouterTache;
        public ICommand AjouterTache
        {
            get{ return _AjouterTache; }
            set { _AjouterTache = value; }
        }
        private void AjouterTache_Execute(object sender)
        {
            Tache nouvelleTache = new Tache(IdTache, TitreTache, DateFinTache, DescriptionTache, CoursTache.Numero, CategorieTache.ID);
            dal.TacheFactory().Save(nouvelleTache);
        }
        private bool AjouterTache_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Afficher Ajouter Rappel
        ICommand _AfficherAjouterRappel;
        public ICommand AfficherAjouterRappel
        {
            get { return _AfficherAjouterRappel; }
            set { _AfficherAjouterRappel = value; }
        }
        private void AfficherAjouterRappel_Execute(object sender)
        {
            AjouterRappel rappelControl = new AjouterRappel();
            RappelOption = rappelControl;
        }
        private bool AfficherAjouterRappel_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Ajouter Rappel
        ICommand _AjouterRappel;
        public ICommand AjouterRappel
        {
            get { return _AjouterRappel; }
            set { _AjouterRappel = value; }
        }
        private void AjouterRappel_Execute(object sender)
        {
            Rappel nouveauRappel = new Rappel(IdTache,TitreRappel, MessageRappel, DateRappel);
            dal.RappelFactory().Save(nouveauRappel);
            Rappels = new (dal.RappelFactory().GetRappelByTache(tacheLocal));
            ListeRappel rappelControl = new ListeRappel();
            RappelOption = rappelControl;
        }
        private bool AjouterRappel_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        #endregion

        /// <summary>
        /// Constructeur pour modifier ou afficher les informations
        /// </summary>
        /// <param name="tache">La tache reçu à afficher </param>
        /// <param name="Affichage">Qu'est-ce que tu veux afficher</param>
        public DetailTacheViewModel(Tache tache, string Affichage)
        {
            dal = new DAL();
            tacheLocal = tache;
            IdTache = tache.ID;
            TitreTache = tache.Titre;
            DateDebutTache = tache.DateDebut;
            DateFinTache = tache.DateFin;
            DescriptionTache = tache.Description;
            CoursTache = dal.CoursFactory().Get(tache.NoCours);
            CategorieTache = dal.CategorieFactory().Get(tache.IdCategorie);
            if(Affichage == "Modifier")
            {
                Categories = new(dal.CategorieFactory().GetAll());
                //LesCours = new (dal.CoursFactory().GetByProgramme(base.SelectedSession.ID));
                BoutonTitreTache = "Modifier";
                Ajouter userControl = new Ajouter();
                UserControlAfficher = userControl;
            }
            else
            {
                Rappels = tache.Rappels;
                AfficherInfo userControl = new AfficherInfo();
                UserControlAfficher = userControl;
                ListeRappel rappelControl = new ListeRappel();
                RappelOption = rappelControl;

                this.AfficherAjouterRappel = new CommandeRelais(AfficherAjouterRappel_Execute, AfficherAjouterRappel_CanExecute);
                this.AjouterRappel = new CommandeRelais(AjouterRappel_Execute, AjouterRappel_CanExecute);
            }
        }

        /// <summary>
        /// Constructeur pour ajouter une tache
        /// </summary>
        public DetailTacheViewModel()
        {
            dal = new DAL();
            BoutonTitreTache = "Ajouter";
            Categories = new(dal.CategorieFactory().GetAll());
            //LesCours = new (dal.CoursFactory().GetByProgramme(base.SelectedSession.ID));
            IdTache = 0;
            this.AjouterTache = new CommandeRelais(AjouterTache_Execute, AjouterRappel_CanExecute);
            Ajouter userControl = new Ajouter();
            UserControlAfficher = userControl;
        }
    }
}
