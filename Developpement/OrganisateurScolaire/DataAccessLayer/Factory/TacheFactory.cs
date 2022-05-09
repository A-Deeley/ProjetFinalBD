using MySql.Data.MySqlClient;
using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.DataAccessLayer.Factory
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
        public async Task<IEnumerable<Tache>> GetTacheByCoursAsync(Cours cours)
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

                using (DbDataReader sqlReader = await command.ExecuteReaderAsync())
                {
                    while (sqlReader.Read())
                    {
                        taches.Add(new()
                        {
                            ID = (int)sqlReader.GetInt64(0),
                            Titre = sqlReader.GetString(1),
                            DateDebut = sqlReader.GetDateTime(2),
                            DateFin = sqlReader.GetDateTime(3),
                            Description = sqlReader.GetString(4),
                            Statut = sqlReader.GetString(5)
                        });
                    }
                }
            }

            return taches;
        }

        /// <summary>
        /// Queries the database and gets all the Taches for the current session.
        /// </summary>
        /// <returns>A list of Taches</returns>
        public IList<Tache> GetTachesBySession(Session session)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECt *" +
                    "FROM tblTaches taches" +
                    "JOIN tblCours cours ON cours.noCours = taches.noCours" +
                    "JOIN tblProgrammeCours progCours ON progCours.noCours = cours.noCours" +
                    "JOIN tblProgrammes programmes ON programmes.noProgramme = progCours.noProgramme" +
                    "JOIN tblSessions sessions ON sessions.noProgramme = programmes.noProgramme" +
                    "WHERE sessions.idSession=@idSession")
                .AddParameter("@idSession", session.ID)
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
                            Titre = sqlReader.GetString(1),
                            DateDebut = sqlReader.GetDateTime(2),
                            DateFin = sqlReader.GetDateTime(3),
                            Description = sqlReader.GetString(4),
                            Statut = sqlReader.GetString(5)
                        });
                    }
                }
            }

            return taches;
        }
    }
}
