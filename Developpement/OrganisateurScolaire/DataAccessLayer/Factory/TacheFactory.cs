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
                    "SELECT taches.idTache, titre, dateDebut, dateFin, description, statut.etat, categories.nom " +
                    "FROM tblTaches taches " +
                    "JOIN tblStatuts statut ON taches.idStatut=statut.idStatut " +
                    "JOIN tblCategorieTaches categorieTaches ON taches.idTache=categorieTaches.idTache " +
                    "JOIN tblCategories categories ON categories.nom=categorieTaches.nomCategorie " +
                    "WHERE noCours=@noCours")
                .AddParameter("@noCours", cours.Numero)
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
                                Categorie = sqlReader.GetString(6),
                                Background = cours.CouleurBrush
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
        public void Save(Tache tache)
        {
            //Ajouter
            if (tache.ID == 0)
            {
                var command =
                 QueryBuilder
                .Init(Connection)
                .SetQuery(
                "Insert into tbltaches (noCours,idStatut,titre, dateFin, description) values (@noCours,@idStatut,@titre, @dateFin, @description)")
                .AddParameter("@noCours", tache.NoCours)
                .AddParameter("@idStatut", 0)
                .AddParameter("@titre", tache.Titre)
                .AddParameter("@dateFin", tache.DateFin)
                .AddParameter("@description", tache.Description)
                .Build();


                ExecuteNonQuery(command, 1);

                // TODO: Insert entry in tblCategorieTache
            }
            //Modifier
            else
            {
                var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                "Update tbltaches set noCours = @noCours,idStatut = @idStatut ,titre = @titre , description = @description , dateFin = @dateFin, where idTache = @idTache")
                .AddParameter("@idTache", tache.ID)
                .AddParameter("@noCours", tache.NoCours)
                .AddParameter("@titre", tache.Titre)
                .AddParameter("@idStatut", 0)
                .AddParameter("@dateFin", tache.DateFin)
                .AddParameter("@description", tache.Description)
                .Build();
                 
                ExecuteNonQuery(command, 1);

                // TODO: Update entry in tblCategorieTaches
            }

        }
    }
}
