using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblSessions).
    /// </summary>
    public class Session : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string caller = null) => PropertyChanged?.Invoke(this, new(caller));
        #endregion

        private Programme? _programme;

        public string ID { get; init; }
        public Programme? Programme 
        {
            get => _programme;
            set { _programme = value; OnPropertyChanged(); }
        }


        public override string ToString()
        {
            return string.Join(' ', ID.Split('_'));
        }
    }
}
