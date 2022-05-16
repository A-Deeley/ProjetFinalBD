using MySql.Data.MySqlClient;
using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OrganisateurScolaire.DataAccessLayer.Factory
{
    /// <summary>
    /// Factory to handle Tache object manipulation (tblCategories)
    /// </summary>
    public class TacheFactory : FactoryBase
    {

        /// <summary>
        /// Gets all the <see cref="Tache"/> for the current session.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> to query the <see cref="Tache"/>s for.</param>
        /// <returns></returns>
        public IEnumerable<Tache> GetAllBySession(Session session)
        {
            DAL dal = new();
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT taches.idTache, titre, dateDebut, dateFin, taches.description, statuts.etat, categories.id, cours.couleur, cours.noCours " +
                    "FROM tblTaches taches " +
                    "JOIN tblCours cours ON cours.idCours=taches.idCours " +
                    "JOIN tblStatuts statuts ON statuts.idStatut=taches.idStatut " +
                    "JOIN tblProgrammeSessionCours psc ON (psc.noProgramme=@noProgramme AND psc.idSession=@idSession AND psc.idCours=cours.idCours) " +
                    "JOIN tblCategorieTaches categorieTaches ON categorieTaches.idTache=taches.idTache " +
                    "JOIN tblCategories categories ON categories.id=categorieTaches.idCategorie")
                .AddParameter("@noProgramme", session.Programme.Numero)
                .AddParameter("@idSession", session.ID)
                .Build();

            List<Tache> taches = new();

            using (command.Connection)
            {
                try
                {


                    command.Connection.Open();

                    using (MySqlDataReader sqlReader = command.ExecuteReader())
                        while (sqlReader.Read())
                        {
                            var brushConverter = new BrushConverter();
                            Brush bgBrush = (Brush)brushConverter.ConvertFromString($"#{sqlReader.GetString(7)}");
                            bgBrush.Freeze();
                            taches.Add(new()
                            {
                                ID = (int)sqlReader.GetInt64(0),
                                Titre = sqlReader.GetString(1),
                                DateDebut = GetDateTimeDBNull(sqlReader, 2),
                                DateFin = sqlReader.GetDateTime(3),
                                Description = GetStringDBNull(sqlReader, 4),
                                Statut = sqlReader.GetString(5),
                                NoCategorie = (int)sqlReader.GetInt64(6),
                                Background = bgBrush,
                                NoCours = sqlReader.GetString(8)
                            });
                        }

                }
                catch (Exception ex) { }
                return taches;
            }
        }

        /// <summary>
        /// Gets all the Taches associated with the specified cours.
        /// </summary>
        /// <param name="cours">The session to get the taches for.</param>
        /// <returns>A list of Taches.</returns>
        public IEnumerable<Tache> GetTacheByCours(string sessionId, Cours cour)
        {
            // TODO: Fix dis (refaire schéma?) --Andy
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "SELECT taches.idTache, titre, dateDebut, dateFin, taches.description, statut.etat, categories.nom " +
                    "FROM tblTaches taches " +
                    "JOIN tblStatuts statut ON taches.idStatut=statut.idStatut " +
                    "JOIN tblCategorieTaches categorieTaches ON taches.idTache=categorieTaches.idTache " +
                    "JOIN tblCategories categories ON categories.nom=categorieTaches.nomCategorie " +
                    "JOIN tblCours cours ON taches.noCours=cours.NoCours " +
                    "JOIN tblProgrammeSessionCours psc ON (idCours=@idCours AND idSession=@idSession")
                .AddParameter("@idCours", cour.Id)
                .AddParameter("@idSession", sessionId)
                .Build();

            List<Tache> taches = new List<Tache>();
            using (command.Connection)
            {
                command.Connection.Open();
                try
                {
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
                                NoCategorie = sqlReader.GetInt32(6),
                                Background = cour.CouleurBrush
                            };
                            tache.Rappels = new(dal.RappelFactory().GetRappelByTache(tache));

                            taches.Add(tache);
                        }
                    }
                }
                catch(Exception)
                {
                }
            }

            return taches;
        }

        /// <summary>
        /// Insert et update de tache
        /// </summary>
        /// <param name="tache">La tache </param>
        public void Save(Tache tache, int idCours)
        {
            //Ajouter
            if (tache.ID == 0)
            {
                var command =
                 QueryBuilder
                .Init(Connection)
                .SetQuery(
                "Insert into tbltaches (idCours,idStatut,titre, dateFin, description, idCategorie) values (@noCours,@idStatut,@titre, @dateFin, @description, @noCategorie)")
                .AddParameter("@noCours", idCours)
                .AddParameter("@idStatut", 0)
                .AddParameter("@titre", tache.Titre)
                .AddParameter("@dateFin", tache.DateFin)
                .AddParameter("@description", tache.Description)
                .AddParameter("@noCategorie", tache.Categorie.ID)
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
                "Update tbltaches set noCours = @noCours,idStatut = @idStatut ,titre = @titre , description = @description , dateFin = @dateFin, noCategorie= @noCategorie where idTache = @idTache")
                .AddParameter("@idTache", tache.ID)
                .AddParameter("@noCours", tache.NoCours)
                .AddParameter("@titre", tache.Titre)
                .AddParameter("@idStatut", 0)
                .AddParameter("@dateFin", tache.DateFin)
                .AddParameter("@description", tache.Description)
                .AddParameter("@noCategorie", tache.Categorie.ID)
                .Build();
                 
                ExecuteNonQuery(command, 1);

            }

        }

        /// <summary>
        /// Gets all the Taches today
        /// </summary>
        /// <returns>A list of Taches.</returns>
        public IEnumerable<Tache> GetTacheAujourdhui()
        {
            // TODO: Fix dis (refaire schéma?) --Andy
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("Select * from  TacheAujourdhui;")
                .Build();

            List<Tache> taches = new List<Tache>();
            using (command.Connection)
            {
                command.Connection.Open();
                try
                {
                    using (MySqlDataReader sqlReader = command.ExecuteReader())
                    {
                        DAL dal = new();
                         while (sqlReader.Read())
                        {
                            var brushConverter = new BrushConverter();
                            Brush bgBrush = (Brush)brushConverter.ConvertFromString($"#{sqlReader.GetString(7)}");
                            bgBrush.Freeze();
                            Tache tache = new()
                            {

                                ID = (int)sqlReader.GetInt64(0),
                                Titre = sqlReader.GetString(1),
                                DateDebut = GetDateTimeDBNull(sqlReader, 2),
                                DateFin = sqlReader.GetDateTime(3),
                                Description = GetStringDBNull(sqlReader, 4),
                                Statut = sqlReader.GetString(5),
                                NoCategorie = sqlReader.GetInt32(6),
                                Background = bgBrush,
                                NoCours = sqlReader.GetString(8),
                            };

                            tache.Rappels = new(dal.RappelFactory().GetRappelByTache(tache));

                            taches.Add(tache);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return taches;
        }

        /// <summary>
        /// Deletes the tache from the database.
        /// </summary>
        /// <param name="tache">The categorie to delete.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public bool DeleteTache(Tache tache)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("DELETE FROM tbltaches WHERE idTache=@id")
                .AddParameter("@id", tache.ID)
                .Build();

            return ExecuteNonQuery(command, 1);
        }
    }
}
