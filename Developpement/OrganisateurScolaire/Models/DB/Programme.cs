using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblProgrammes).
    /// </summary>
    public class Programme
    {
        public string Numero { get; init; }
        public string Nom { get; init; }
        public List<Cours> Cours { get; set; }
    }
}
