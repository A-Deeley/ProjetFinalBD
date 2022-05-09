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
        public int ID { get; init; }
        public List<Rappel> Rappels { get; }
        public string Statut { get; }
        public string Titre { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Description { get; set; }
    }
}
