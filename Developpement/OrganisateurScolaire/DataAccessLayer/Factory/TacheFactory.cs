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
        public IEnumerable<Tache> GetTacheByCours(Cours cours)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT idTache, titre, dateDebut, dateFin, description, statut.etat " +
                    "FROM tblTaches taches " +
                    "JOIN tblStatuts statut ON taches.idStatut=statut.idStatut " +
                    "WHERE noCours=@noCours")
                .AddParameter("@noCours", cours.Numero)
                .Build();

            List<Tache> taches = new List<Tache>();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    DAL dal = new();
                    while (sqlReader.Read())
                    {
                        Tache tache = new() { 

                            ID = (int)sqlReader.GetInt64(0),
                            Titre = sqlReader.GetString(1),
                            DateDebut = GetDateTimeDBNull(sqlReader, 2),
                            DateFin = sqlReader.GetDateTime(3),
                            Description = GetStringDBNull(sqlReader, 4),
                            Statut = sqlReader.GetString(5),
                            Background = cours.CouleurBrush
                        };
                        tache.Rappels = new(dal.RappelFactory().GetRappelByTache(tache));

                        taches.Add(tache);
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
                    "SELECT * " +
                    "FROM tblTaches taches " +
                    "JOIN tblCours cours ON cours.noCours = taches.noCours " +
                    "JOIN tblProgrammeCours progCours ON progCours.noCours = cours.noCours " +
                    "JOIN tblProgrammes programmes ON programmes.noProgramme = progCours.noProgramme " +
                    "JOIN tblSessions sessions ON sessions.noProgramme = programmes.noProgramme " +
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

        /// <summary>
        /// Insert et update de tache
        /// </summary>
        /// <param name="tache">La tache </param>
        public void Save(Tache tache)
        {
            //Ajouter
            if (tache.ID == 0)
            {
                var command =
                 QueryBuilder
                .Init(Connection)
                .SetQuery(
                "Insert into tbltaches (noCours,idStatut,titre, dateDebut, description, noCategorie) value(@noCours,@idStatut,@titre, @dateDebut, @description, @noCategorie)")
                .AddParameter("@noCours", tache.NoCours)
                .AddParameter("@idStatut", 0)
                .AddParameter("@titre", tache.Titre)
                .AddParameter("@dateDebut", tache.Titre)
                .AddParameter("@description", tache.Titre)
                .AddParameter("@noCategorie", tache.IdCategorie)
                .Build();


                ExecuteNonQuery(command, 1);
            }
            //Modifier
            else
            {
                var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                "Update tbltaches set noCours = @noCours,idStatut = @idStatut ,titre = @titre , description = @description , noCategorie = @noCategorie where idTache = @idTache")
                .AddParameter("@idTache", tache.ID)
                .AddParameter("@noCours", tache.NoCours)
                .AddParameter("@titre", tache.Titre)
                .AddParameter("@idStatut", 0)
                .AddParameter("@dateDebut", tache.Titre)
                .AddParameter("@description", tache.Titre)
                .AddParameter("@noCategorie", tache.IdCategorie)
                .Build();
                 
                ExecuteNonQuery(command, 1);
            }

        }
    }
}
