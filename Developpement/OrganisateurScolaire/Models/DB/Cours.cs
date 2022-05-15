using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblCours).
    /// </summary>
    public class Cours : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string caller = null) => PropertyChanged?.Invoke(this, new(caller));
        #endregion

        public int Id { get; init; }
        public string Numero { get; init; }
        public string Nom { get; init; }
        public string Description { get; init; }
        public List<Tache> Taches { get; set; }
        public Brush CouleurBrush { get; private set; }

        public void SetCouleur(string hex)
        {
            var brushConverter = new BrushConverter();
            CouleurBrush = (Brush)brushConverter.ConvertFromString(hex);
            CouleurBrush.Freeze();
        }

        public IEnumerable<Rappel> GetAllRappels()
        {
            List<Rappel> allRappels = new();

            foreach (Tache tache in Taches)
            {
                allRappels.AddRange(tache.Rappels);
            }

            return allRappels;
        }
    }
}
