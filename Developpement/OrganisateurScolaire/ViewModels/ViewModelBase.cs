using OrganisateurScolaire.DataAccessLayer;
using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OrganisateurScolaire.ViewModels;

namespace OrganisateurScolaire.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string caller = null) => PropertyChanged?.Invoke(this, new(caller));
        #endregion

        private Session _selectedSession;

        public Session SelectedSession
        {
            get => _selectedSession;
            set { _selectedSession = value; OnPropertyChanged(); }
        }


        /// <summary>
        /// When the selected Session is changed, make sure the data is loaded and reset the UI for the Cours and Rappels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSelectedSessionChanged(object sender, PropertyChangedEventArgs e)
        {
            // If the programme is null, fill it the first time.
            if (SelectedSession.Programme is null)
            {
                DAL dal = new();
                SelectedSession.Programme = dal.ProgrammeFactory().GetBySession(SelectedSession);
            }
        }
    }
}
