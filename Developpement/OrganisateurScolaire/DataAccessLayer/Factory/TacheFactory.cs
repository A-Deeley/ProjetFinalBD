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
                    "SELECT taches.idTache, titre, dateDebut, dateFin, description, statut.etat, categories.idCategorie " +
                    "FROM tblTaches taches " +
                    "JOIN tblStatuts statut ON taches.idStatut=statut.idStatut " +
                    "JOIN tblCategorieTaches categorieTaches ON taches.idTache=categorieTaches.idTache " +
                    "JOIN tblCategories categories ON categories.idCategorie=categorieTaches.idCategorie " +
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
                            IdCategorie = (int)sqlReader.GetInt64(6),
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
