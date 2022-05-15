using OrganisateurScolaire.DataAccessLayer;
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
        private string _NomCategorie;
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
                if (value is not null)
                    NomCategorie = Categorie.Nom;
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
        public ICommand AppliquerSession { get; init; }
        private void AppliquerSession_Execute(object sender)
        {
            Session.Programme = Programme;
        }
        private bool AppliquerSession_CanExecute(object parameter)
        {
            return Programme is not null && Session.Programme != Programme;
        }
        #endregion
        #region Renitialiser Categorie
        public ICommand RenitialiserCategorie { get; init; }
        private void ReinitialiserCategorie_Execute(object sender)
        {
            NomCategorie = "";
            Categorie = null;
        }
        private bool ReinitialiserCategorie_CanExecute(object parameter)
        {
            return Categorie is not null && NomCategorie != Categorie.Nom;
        }
        #endregion
        #region Appliquer Categorie
        public ICommand AppliquerCategorie { get; init; }
        private void AppliquerCategorie_Execute(object sender)
        {
            Categorie.Nom = NomCategorie;
            new DAL().CategorieFactory().UpdateCategorie(Categorie);
        }
        private bool AppliquerCategorie_CanExecute(object parameter)
        {
            return Categorie is not null && NomCategorie != Categorie.Nom;
        }
        #endregion
        #endregion

        public SettingsViewModel(List<Session> sessions, Session currentSession, List<Programme> programmes)
        {
            Sessions = sessions;
            Session = currentSession ?? null;
            Programmes = programmes;
            Programme = currentSession?.Programme ?? null;
            AppliquerSession = new CommandeRelais(AppliquerSession_Execute, AppliquerSession_CanExecute);
            RenitialiserCategorie = new CommandeRelais(ReinitialiserCategorie_Execute, ReinitialiserCategorie_CanExecute);
            AppliquerCategorie = new CommandeRelais(AppliquerCategorie_Execute, AppliquerCategorie_CanExecute);
            Categories = new(new DAL().CategorieFactory().GetAll());
        }
    }
}
