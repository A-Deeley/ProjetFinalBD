using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblTaches).
    /// </summary>
    public class Tache
    {
        public int ID { get; private set; }
        public Cours Cours { get; private set; }
        public Statut Statut { get; private set; }
        public string Titre { get; private set; }
        public DateTime DateDebut { get; private set; }
        public DateTime DateFin { get; private set; }
        public string Description { get; private set; }
    }
}
