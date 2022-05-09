using OrganisateurScolaire.Models.Enums;
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
        public int ID { get; init; }
        public Programme Programme { get; set; }
        public int Annee { get; init; }
        public Saisons Saison { get; init; }
    }
}
