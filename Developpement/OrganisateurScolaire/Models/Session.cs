using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblSessions).
    /// </summary>
    public class Session
    {
        public int ID { get; private set; }
        public Programme Programme { get; private set; }
        public int Annee { get; private set; }
        public string Saison { get; private set; }
    }
}
