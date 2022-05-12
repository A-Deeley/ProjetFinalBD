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

        
    }
}
