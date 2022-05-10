﻿using OrganisateurScolaire.Models;
using OrganisateurScolaire.Models.Enums;
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
        /// <param name="saison">The season to check.</param>
        /// <param name="annee">The year to check.</param>
        /// <returns>The first Session found for the given saison and year.</returns>
        public async Task<Session> GetBySaisonAsync(Saisons saison, int annee)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblSessions " +
                    "WHERE saison=@saison AND annee=@anne")
                .AddParameter("@saison", nameof(saison))
                .AddParameter("@annee", annee)
                .Build();

            Session? session = null;
            using (command.Connection)
            {
                command.Connection.Open();

                using (DbDataReader sqlReader = await command.ExecuteReaderAsync())
                {
                    sqlReader.Read();

                    session = new()
                    {
                        ID = (int)sqlReader.GetInt64(0),
                        Annee = sqlReader.GetInt16(2),
                        Saison = Enum.Parse<Saisons>(sqlReader.GetString(3))
                    };

                    string noProgramme = sqlReader.GetString(1);

                    DAL dal = new();
                    session.Programme = await dal.ProgrammeFactory().GetByIdAsync(noProgramme);
                }
            }

            return session;
        }

        public async Task<IEnumerable<Session>> GetAllWithoutInfo()
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT idSession, annee, saison " +
                    "FROM tblSessions")
                .Build();

            List<Session> sessions = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (DbDataReader sqlReader = await command.ExecuteReaderAsync())
                {
                    while (sqlReader.Read())
                        sessions.Add(new()
                        {
                            ID = (int)sqlReader.GetInt64(0),
                            Annee = sqlReader.GetInt16(1),
                            Saison = Enum.Parse<Saisons>(sqlReader.GetString(2))
                        });
                }
            }

            return sessions;
        }
    }
}