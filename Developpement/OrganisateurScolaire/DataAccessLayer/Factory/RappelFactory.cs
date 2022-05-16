using MySql.Data.MySqlClient;
using OrganisateurScolaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OrganisateurScolaire.DataAccessLayer.Factory
{
    /// <summary>
    /// Factory to handle <see cref="Rappel"/> object manipulation (tblRappels)
    /// </summary>
    public class RappelFactory : FactoryBase
    {

        /// <summary>
        /// Gets all the Rappels from the database associated with the specified Tache.
        /// </summary>
        /// <param name="tache">The tache to get the attached Rappels for.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Rappel"/>.</returns>
        public IList<Rappel> GetRappelByTache(Tache tache)
        {
            // Initialize query.
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("SELECT idRappel, titre, message FROM tblRappels WHERE idTache=@id")
                .AddParameter("@id", tache.ID)
                .Build();

            // Fetch the rappels from the database.
            List<Rappel> rappels = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                        rappels.Add(new()
                        {
                            ID = (int)sqlReader.GetInt64(0),
                            Titre = sqlReader.GetString(1),
                            Message = sqlReader.GetString(2),
                            Background = tache.Background
                        });
                }
            }

            return rappels;
        }

        /// <summary>
        /// Insert de rappel
        /// </summary>
        /// <param name="tache"></param>
        public void Save(Rappel rappel)
        {
                var command =
                 QueryBuilder
                .Init(Connection)
                .SetQuery(
                "Insert into tblRappels (idTache ,dateRappel ,titre , message) value(@idTache ,@dateRappel ,@titre , @message)")
                .AddParameter("@idTache", rappel.IdTache)
                .AddParameter("@dateRappel", rappel.DateRappel)
                .AddParameter("@titre", rappel.Titre)
                .AddParameter("@message", rappel.Message)
                .Build();
            ExecuteNonQuery(command, 1);
        }

        /// <summary>
        /// Gets all the Rappels from the database associated with the specified Tache.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Rappel"/>.</returns>
        public IList<Rappel> GetRappelAujourdhui()
        {
            var brushConverter = new BrushConverter();
            // Initialize query.
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("Call RappelTache;")
                .Build();

            // Fetch the rappels from the database.
            List<Rappel> rappels = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                    rappels.Add(new()
                    {
                        ID = (int)sqlReader.GetInt64(0),
                        IdTache = (int)sqlReader.GetInt64(1),
                        DateRappel = sqlReader.GetDateTime(2),
                        Titre = sqlReader.GetString(3),
                        Message = sqlReader.GetString(4),
                        Background = (Brush)brushConverter.ConvertFromString($"#{sqlReader.GetString(5)}")
                }) ;
                }
            }

            return rappels;
        }

        public IList<Rappel> GetRappelAujourdhuiCours(Cours cours)
        {
            var brushConverter = new BrushConverter();
            // Initialize query.
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery("call RappelCours(@noCours);")
                .AddParameter("@noCours", cours.Numero)
                .Build();

            // Fetch the rappels from the database.
            List<Rappel> rappels = new();
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    while (sqlReader.Read())
                        rappels.Add(new()
                        {
                            ID = (int)sqlReader.GetInt64(0),
                            IdTache = (int)sqlReader.GetInt64(1),
                            DateRappel = sqlReader.GetDateTime(2),
                            Titre = sqlReader.GetString(3),
                            Message = sqlReader.GetString(4),
                            Background = (Brush)brushConverter.ConvertFromString($"#{sqlReader.GetString(5)}")
                        });
                }
            }

            return rappels;
        }
    }
}
