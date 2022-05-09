using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.DAL.Factory
{
    public class CoursFactory : FactoryBase
    {
        public async Task<List<Cours>> GetByProgrammeAsync(string noProgramme)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblCours cours " +
                    "JOIN tblProgrammeCours progCours ON progCours.noCours = cours.noCours " +
                    "JOIN tblProgramme programmes ON programmes.noProgramme = progCours.noProgramme " +
                    "WHERE programme.noProgramme=@noProgramme")
                .AddParameter("@noProgramme", noProgramme)
                .Build();

            List<Cours> cours = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (DbDataReader sqlReader = await command.ExecuteReaderAsync())
                {
                    while (sqlReader.Read())
                    {
                        Cours cour = new()
                        {
                            Numero = sqlReader.GetString(0),
                            Nom = sqlReader.GetString(1),
                            Description = sqlReader.GetString(2)
                        };

                        DAL dal = new();

                        cour.Taches = await dal.TacheFactory().GetTacheByCoursAsync(cour);

                        cour.SetCouleur(sqlReader.GetString(3));
                        cours.Add(cour);
                    }
                }
            }

            return cours;
        }
    }
}
