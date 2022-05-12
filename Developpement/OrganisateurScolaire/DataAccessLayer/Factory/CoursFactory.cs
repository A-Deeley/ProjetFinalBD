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
    public class CoursFactory : FactoryBase
    {
        public List<Cours> GetByProgramme(string noProgramme)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblCours cours " +
                    "JOIN tblProgrammeCours progCours ON progCours.noCours = cours.noCours " +
                    "JOIN tblProgrammes programmes ON programmes.noProgramme = progCours.noProgramme " +
                    "WHERE programmes.noProgramme=@noProgramme")
                .AddParameter("@noProgramme", noProgramme)
                .Build();

            List<Cours> cours = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Cours cour = new()
                        {
                            Numero = sqlReader.GetString(0),
                            Nom = sqlReader.GetString(1),
                            Description = GetStringDBNull(sqlReader, 2)
                        };

                        DAL dal = new();

                        cour.SetCouleur($"#{sqlReader.GetString(3)}");
                        cour.Taches = new(dal.TacheFactory().GetTacheByCours(cour));

                        cours.Add(cour);
                    }
                }
            }

            return cours;
        }

        /// <summary>
        /// Get par no cours 
        /// </summary>
        /// <param name="noCours">le numero du cours chercher</param>
        /// <returns>le cours trouver</returns>
        public Cours  Get(string noCours)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblCours where noCours = @noCours")
                .AddParameter("@noCours", noCours)
                .Build();

            Cours cour = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        cour = new()
                        {
                            Numero = sqlReader.GetString(0),
                            Nom = sqlReader.GetString(1),
                            Description = GetStringDBNull(sqlReader, 2)
                        };
                        cour.SetCouleur($"#{sqlReader.GetString(3)}");
                    }
                }
            }

            return cour;
        }
    }
}
