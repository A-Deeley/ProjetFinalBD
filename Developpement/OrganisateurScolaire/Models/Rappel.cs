using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblRappels).
    /// </summary>
    public class Rappel
    {
        public int ID { get; private set; }
        public Tache Tache { get; private set; }
        public string Titre { get; private set; }
        public string Message { get; private set; }
    }
}
