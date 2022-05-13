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
                .SetQuery("SELECT nom FROM tblCategories")
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
                            Nom = sqlReader.GetString(0)
                        });
                }
            }

            return categories;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <returns></returns>
        public Categorie Get(string value)
        {
            // Initialize query.
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("SELECT nom FROM tblCategories where nom=@value")
                .AddParameter("@value", value)
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
                            Nom = sqlReader.GetString(0)
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
                .SetQuery("UPDATE tblCategories SET nom=@nom WHERE nom=@nom")
                .AddParameter("@nom", categorie.Nom)
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
                .SetQuery("DELETE FROM tblCategories WHERE nom=@nom")
                .AddParameter("@nom", categorie.Nom)
                .Build();

            return ExecuteNonQuery(command, 1);
        }
    }
}
