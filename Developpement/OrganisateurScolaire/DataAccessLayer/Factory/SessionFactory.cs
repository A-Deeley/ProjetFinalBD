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
    public class SessionFactory : FactoryBase
    {
        /// <summary>
        /// Queries the database for a Session and returns the first one found.
        /// </summary>
        /// <param name="sessionId">The ID of the session to find</param>
        /// <returns>The first Session found for the given saison and year.</returns>
        public Session GetBySaison(string sessionId)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblSessions " +
                    "WHERE idSession=@id")
                .AddParameter("@id", sessionId)
                .Build();

            Session? session = null;
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    sqlReader.Read();

                    session = new()
                    {
                        ID = sqlReader.GetString(0)
                    };

                    string noProgramme = sqlReader.GetString(1);

                    DAL dal = new();
                    session.Programme = dal.ProgrammeFactory().GetById(noProgramme);
                }
            }

            return session;
        }

        /// <summary>
        /// Queries the database for all the sessions and returns them without the Programme field filled in.
        /// </summary>
        /// <returns>The list of sessions.</returns>
        public IEnumerable<Session> GetAllWithoutInfo()
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblSessions")
                .Build();

            List<Session> sessions = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                    while (sqlReader.Read())
                        sessions.Add(new()
                        {
                            ID = sqlReader.GetString(0)
                        });
            }

            return sessions;
        }
    }
}
