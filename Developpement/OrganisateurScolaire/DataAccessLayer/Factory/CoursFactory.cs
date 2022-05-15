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
        public string GetName(string noCours)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT nomCours " +
                    "FROM tblCours " +
                    "WHERE noCours=@noCours")
                .AddParameter("@noCours", noCours)
                .Build();

            string nomCours = string.Empty;
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    sqlReader.Read();
                    nomCours = sqlReader.GetString(0);
                }
            }

            return nomCours;
        }

        public List<Cours> GetBySession(Session session)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT * " +
                    "FROM tblCours cours " +
                    "JOIN tblProgrammeSessionCours psc ON " +
                    "(psc.idCours=cours.idCours AND psc.idSession=@idSession AND psc.noProgramme=@noProgramme) " +
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
                            Id = (int)sqlReader.GetInt64(0),
                            Numero = sqlReader.GetString(1),
                            Nom = sqlReader.GetString(2),
                            Description = GetStringDBNull(sqlReader, 3)
                        };


                        cour.SetCouleur($"#{sqlReader.GetString(4)}");
                        cour.Taches = new(dal.TacheFactory().GetTacheByCours(session.ID, cour));

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
                            Id = (int)sqlReader.GetInt64(0),
                            Numero = sqlReader.GetString(1),
                            Nom = sqlReader.GetString(2),
                            Description = GetStringDBNull(sqlReader, 3)
                        };
                        cour.SetCouleur($"#{sqlReader.GetString(3)}");
                    }
                }
            }

            return cour;
        }
    }
}
