using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblCours).
    /// </summary>
    public class Cours
    {
        public string Numero { get; init; }
        public string Nom { get; init; }
        public string Description { get; init; }
        public List<Tache> Taches { get; set; }
        public Brush CouleurBrush { get; private set; }

        public void SetCouleur(string hex)
        {
            var brushConverter = new BrushConverter();
            CouleurBrush = (Brush)brushConverter.ConvertFromString(hex);
        }
    }
}
