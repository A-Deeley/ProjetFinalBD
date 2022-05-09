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
        public int ID { get; init; }
        public string Titre { get; init; }
        public string Message { get; init; }

    }
}
