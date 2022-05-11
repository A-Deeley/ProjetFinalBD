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

namespace OrganisateurScolaire.ViewModels
{
    class DetailTacheViewModel : ViewModelBase
    {
        private UserControl _UserControlAfficher, _RappelOption;
        private DAL dal;
        #region Attribut tache
        private string _TitreTache, _CategorieTache, _CoursTache, _DescriptionTache, _StatutTache;
        private DateTime _DateFinTache, _DateDebutTache;
        List<Categorie> _Categories;
        List<Cours> _Cours;
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

        #region Propriété tache
        public string TitreTache
        {
            get { return _TitreTache; }
            set { _TitreTache = value;
                OnPropertyChanged();
            }
        }

        public string CategorieTache
        {
            get { return _CategorieTache; }
            set
            { _CategorieTache = value;
                OnPropertyChanged();
            }
        }

        public string CoursTache
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
        public DateTime DateDebutTache
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

        #region Bouton
        ICommand _AjouterTache;
        ICommand AjouterTache
        {
            get{ return _AjouterTache; }
            set { _AjouterTache = value; }
        }
        private void AjouterTache_Execute(object sender)
        {
            //dal.TacheFactory().Save();
        }
        private bool AjouterTache_CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        public DetailTacheViewModel()
        {
            dal = new DAL();
            Categories = new (dal.CategorieFactory().GetAll());
            //LesCours = new (dal.CoursFactory().GetByProgramme(base.SelectedSession.ID));
            AfficherInfo userControl = new AfficherInfo();
            UserControlAfficher = userControl;
            ListeRappel rappelControl = new ListeRappel();
            RappelOption = rappelControl;
        }
    }
}
