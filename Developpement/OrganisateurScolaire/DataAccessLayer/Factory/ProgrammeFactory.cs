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
        public async Task<Programme> GetByIdAsync(string noProgramme)
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

                using (DbDataReader sqlReader = await command.ExecuteReaderAsync())
                {
                    sqlReader.Read();

                    programme = new()
                    {
                         Numero = sqlReader.GetString(0),
                         Nom = sqlReader.GetString(1)
                    };
                }

                DAL dal = new();
                programme.Cours = await dal.CoursFactory().GetByProgrammeAsync(noProgramme);
            }

            return programme;
        }
    }
}
