using MySql.Data.MySqlClient;
using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.DAL.Factory
{
    /// <summary>
    /// Factory to handle Tache object manipulation (tblCategories)
    /// </summary>
    public class TacheFactory : FactoryBase
    {
        /// <summary>
        /// Gets all the Taches associated with the specified cours.
        /// </summary>
        /// <param name="cours">The cours to get the taches for.</param>
        /// <returns>A list of Taches.</returns>
        public IList<Tache> GetTacheByCours(Cours cours)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT idTache, titre, dateDebut, dateFin, description, etat " +
                    "FROM tblTaches taches " +
                    "JOIN tblStatut statut ON taches.idStatut=statut.idStatut" +
                    "WHERE noCours=@noCours")
                .AddParameter("@noCours", cours.Numero)
                .Build();

            List<Tache> taches = new List<Tache>();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        taches.Add(new()
                        {
                            ID = (int)sqlReader.GetInt64(0),

                        })
                    }
                }
            }
        }

        public IList<Tache> GetAll()
        {

        }
    }
}
