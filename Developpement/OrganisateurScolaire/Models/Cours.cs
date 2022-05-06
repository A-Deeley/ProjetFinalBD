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
        public string Numero { get; private set; }
        public string Nom { get; private set; }
        public string Description { get; private set; }
        public Brush Couleur { get; private set; }
    }
}
