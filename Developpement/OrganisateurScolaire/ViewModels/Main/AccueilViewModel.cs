﻿using OrganisateurScolaire.Models;
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
       
        private List<Session> _sessions;

        private Cours _selectedCours;
        private ObservableCollection<Cours> _cours;

        private ObservableCollection<Rappel> _rappelsOfSelectedCours;

        
        public List<Session> Sessions
        {
            get => _sessions;
            set { _sessions = value; OnPropertyChanged(); }
        }

        public Cours SelectedCours
        {
            get => _selectedCours;
            set { _selectedCours = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Cours> Cours
        {
            get => _cours;
            set { _cours = value; OnPropertyChanged(); }
        }

        public Session CurrentSession { get; private set; }

        public ObservableCollection<Rappel> RappelsOfSelectedCours
        {
            get => _rappelsOfSelectedCours;
            set { _rappelsOfSelectedCours = value; OnPropertyChanged(); }
        }

        public AccueilViewModel()
        {
            PropertyChanged += OnSelectedCoursChanged;
            DAL dal = new();
            RappelsOfSelectedCours = new(dal.RappelFactory().GetRappelAujourdhui());
            Sessions = new(dal.SessionFactory().GetAllWithoutInfo());

        }

        /// <summary>
        /// When the cours is changed, set the rappels to be the ones for the selected cours.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedCoursChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedCours)) return;
            if (SelectedCours is null) return;

            if (SelectedCours.Taches is null)
            {
                DAL dal = new();
                SelectedCours.Taches = new(dal.TacheFactory().GetTacheByCours(CurrentSession.ID, SelectedCours));
            }

            RappelsOfSelectedCours = new(SelectedCours.GetAllRappels());
        }

        public void UpdateSessionDetails(Session selectedSession)
        {
            DAL dal = new();
            RappelsOfSelectedCours = null;
            SelectedCours = null;
            Cours = new(dal.CoursFactory().GetBySession(selectedSession));
            CurrentSession = selectedSession;
        }
    }
}
