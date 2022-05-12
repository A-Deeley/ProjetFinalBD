using MySql.Data.MySqlClient;
using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.DataAccessLayer.Factory
{
    /// <summary>
    /// Factory to handle Categorie object manipulation (tblCategories)
    /// </summary>
    public class CategorieFactory : FactoryBase
    {

        /// <summary>
        /// Gets all the Categories from the database.
        /// </summary>
        /// <returns>A list of all the categories.</returns>
        public IList<Categorie> GetAll()
        {
            // Initialize query.
            var command = 
                QueryBuilder
                .Init(Connection)
                .SetQuery("SELECT * FROM tblCategories")
                .Build();


            // Fetch the results from the table.
            List<Categorie> categories = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                        categories.Add(new()
                        {
                            ID = (int)sqlReader.GetInt64(0),
                            Nom = sqlReader.GetString(1)
                        });
                }
            }

            return categories;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <returns></returns>
        public Categorie Get(int id)
        {
            // Initialize query.
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("SELECT * FROM tblCategories where idCategorie = @IdCategorie ")
                .AddParameter("@IdCategorie", id)
                .Build();


            // Fetch the results from the table.
            Categorie categorie = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                        categorie = new()
                        {
                            ID = (int)sqlReader.GetInt64(0),
                            Nom = sqlReader.GetString(1)
                        };
                }
            }

            return categorie;
        }

        /// <summary>
        /// Updates the categorie in the database.
        /// </summary>
        /// <param name="categorie">The modified category to update.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public bool UpdateCategorie(Categorie categorie)
        {
            // Initialize query.
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("UPDATE tblCategories SET nom=@nom WHERE idCategorie=@id")
                .AddParameter("@nom", categorie.Nom)
                .AddParameter("@id", categorie.ID)
                .Build();

            // Update the table.
            return ExecuteNonQuery(command);
        }

        /// <summary>
        /// Deletes the categorie from the database.
        /// </summary>
        /// <param name="categorie">The categorie to delete.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public bool DeleteCategorie(Categorie categorie)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("DELETE FROM tblCategories WHERE idCategorie=@id")
                .AddParameter("@id", categorie.ID)
                .Build();

            return ExecuteNonQuery(command, 1);
        }
    }
}
