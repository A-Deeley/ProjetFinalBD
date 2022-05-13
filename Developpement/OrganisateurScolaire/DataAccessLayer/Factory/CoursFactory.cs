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
        public List<Cours> GetBySession(Session session)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblCours cours " +
                    "JOIN tblProgrammeSessionCours psc ON " +
                    "(psc.noCours=cours.noCours AND psc.idSession=@idSession AND psc.noProgramme=@noProgramme) " +
                    "WHERE idSession=@idsession")
                .AddParameter("@idsession", session.ID)
                .AddParameter("@noProgramme", session.Programme?.Numero)
                .Build();

            List<Cours> cours = new();
            using (command.Connection)  
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    DAL dal = new();
                    while (sqlReader.Read())
                    {
                        Cours cour = new()
                        {
                            Numero = sqlReader.GetString(0),
                            Nom = sqlReader.GetString(1),
                            Description = GetStringDBNull(sqlReader, 2)
                        };


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
