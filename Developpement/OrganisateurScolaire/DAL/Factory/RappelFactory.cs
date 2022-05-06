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
    /// Factory to handle Rappel object manipulation (tblRappels)
    /// </summary>
    public class RappelFactory : FactoryBase
    {

        /// <summary>
        /// Gets all the Rappels from the database.
        /// </summary>
        /// <returns>A list of Rappels.</returns>
        public IList<Rappel> GetRappelByTache(Tache tache)
        {
            // Initialize query.
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("SELECT idRappel, titre, message FROM tblRappels")
                .Build();

            // Fetch the rappels from the database.
            List<Rappel> rappels = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                        rappels.Add(new()
                        {
                            ID = (int)sqlReader.GetInt64(0),
                            Titre = sqlReader.GetString(1),
                            Message = sqlReader.GetString(2)
                        });
                }
            }

            return rappels;
        }
    }
}
