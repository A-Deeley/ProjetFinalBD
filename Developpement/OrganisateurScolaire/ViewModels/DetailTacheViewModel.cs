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

namespace OrganisateurScolaire.ViewModels
{
    class DetailTacheViewModel : ViewModelBase
    {
        private UserControl _UserControlAfficher;
        private DAL dal;
        #region Attribut tache
        private string _TitreTache, _CategorieTache, _CoursTache, _DescriptionTache;
        private DateTime _DateFinTache, _DateDebutTache;
        List<Categorie> _Categories;
        List<Cours> _Cours;
        #endregion

        public UserControl UserControlAfficher
        {
            get { return _UserControlAfficher; }
            set
            {
                _UserControlAfficher = value;
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

        public List<Categorie> Categories
        {
            get { return _Categories; }
            set { _Categories = value;
                OnPropertyChanged();
            }
        }
        public List<Cours> Cours
        {
            get { return _Cours; }
            set
            {
                _Cours = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public DetailTacheViewModel()
        {
            dal = new DAL();
            Categories = new (dal.CategorieFactory().GetAll());
            Cours = new (dal.CoursFactory().GetAll());
            Ajouter userControl = new Ajouter();
            UserControlAfficher = userControl;
        }
    }
}
