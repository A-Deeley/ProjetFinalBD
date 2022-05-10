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
    public class ProgrammeFactory : FactoryBase
    {
        /// <summary>
        /// Queries the database for all the programmes from a given id and gets the first one found.
        /// </summary>
        /// <param name="noProgramme">The identifier of the Programme</param>
        /// <returns>The first programme found.</returns>
        public Programme GetById(string noProgramme)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblProgrammes " +
                    "WHERE noProgramme=@noProgramme")
                .AddParameter("@noProgramme", noProgramme)
                .Build();

            Programme? programme = null;
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    sqlReader.Read();

                    programme = new()
                    {
                         Numero = sqlReader.GetString(0),
                         Nom = sqlReader.GetString(1)
                    };
                }

                if (programme is not null)
                {
                    DAL dal = new();
                    programme.Cours = dal.CoursFactory().GetByProgramme(noProgramme);
                }
            }

            return programme ?? new();
        }

        /// <summary>
        /// Queries the database for a programme based on its' session and returns the first one found.
        /// </summary>
        /// <param name="session">The session to search for.</param>
        /// <returns>The first programme found that matches the session.</returns>
        public Programme GetBySession(Session session)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT programmes.* " +
                    "FROM tblProgrammes programmes " +
                    "JOIN tblSessionProgrammes sessionProgrammes ON sessionProgrammes.noProgramme = programmes.noProgramme " +
                    "JOIN tblSessions sessions ON sessions.idSession=sessionProgrammes.idSession " +
                    "WHERE sessions.idSession=@id")
                .AddParameter("@id", session.ID)
                .Build();

            Programme? programme = null;
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    sqlReader.Read();

                    programme = new()
                    {
                        Numero = sqlReader.GetString(0),
                        Nom = sqlReader.GetString(1)
                    };


                    if (programme is not null)
                    {
                        DAL dal = new();
                        programme.Cours = dal.CoursFactory().GetByProgramme(programme.Numero);
                    }
                }
            }

            return programme ?? new();
        }
    }
}
