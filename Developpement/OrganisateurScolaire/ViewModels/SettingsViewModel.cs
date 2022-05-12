using OrganisateurScolaire.Models;
using OrganisateurScolaire.VueModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrganisateurScolaire.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        #region Attribut Session
        private List<Programme> _Programmes;
        private Programme _Programme;
        private List<Session> _Sessions;
        private Session _Session;
        #endregion
        #region Attribut Categorie
        private List<Categorie> _Categories;
        private Categorie _Categorie;
        private string _NomCategorie
        #endregion

        #region Propriété Session 
        public List<Programme> Programmes
        {
            get { return _Programmes; }
            set { _Programmes = value;
                OnPropertyChanged();
            }
        }
        public Programme Programme
        {
            get { return _Programme; }
            set { _Programme = value;
                OnPropertyChanged();
            }
        }

        public Session Session
        {
            get { return _Session; }
            set { _Session = value;
                OnPropertyChanged();
            }
        }

        public List<Session> Sessions
        {
            get { return _Sessions; }
            set { _Sessions = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Categorie
        public List<Categorie> Categories
        {
            get { return _Categories; }
            set { _Categories = value;
                OnPropertyChanged();
            }
        }
        public Categorie Categorie
        {
            get { return _Categorie; }
            set { _Categorie = value;
                OnPropertyChanged();
            }
        }
        public string NomCategorie
        {
            get { return _NomCategorie; }
            set { _NomCategorie = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region bouton
        #region Appliquer Session
        ICommand _AppliquerSession;
        ICommand AppliquerSession
        {
            get { return _AppliquerSession; }
            set { _AppliquerSession = value; }
        }
        private void AppliquerSession_Execute(object sender)
        {
            //modifier le programme associer à la session ou l'ajouter
        }
        private bool AppliquerSession_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Renitialiser Categorie
        ICommand _RenitialiserCategorie;
        ICommand RenitialiserCategorie
        {
            get { return _RenitialiserCategorie; }
            set { _RenitialiserCategorie = value; }
        }
        private void RenitialiserCategorie_Execute(object sender)
        {
            Categorie = null;
            NomCategorie = "";
        }
        private bool RenitialiserCategorie_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        #region Appliquer Categorie
        ICommand _AppliquerCategorie;
        ICommand AppliquerCategorie
        {
            get { return _AppliquerCategorie; }
            set { _AppliquerCategorie = value; }
        }
        private void AppliquerCategorie_Execute(object sender)
        {
            //Ajouter ou modifier une categorie
        }
        private bool AppliquerCategorie_CanExecute(object parameter)
        {
            return true;
        }
        #endregion
        #endregion

        public SettingsViewModel()
        {
            this.AppliquerSession = new CommandeRelais(AppliquerSession_Execute, AppliquerSession_CanExecute);
        }
    }
}
