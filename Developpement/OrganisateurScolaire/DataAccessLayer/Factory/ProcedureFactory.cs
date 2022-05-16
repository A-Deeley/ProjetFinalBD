using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using OrganisateurScolaire.Models;

namespace OrganisateurScolaire.DataAccessLayer.Factory
{
    public class ProcedureFactory : FactoryBase
    {
        public int Get()
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "call TacheAuj()")
                .Build();

            int nbTache = 0;
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    sqlReader.Read();
                    nbTache = (int)sqlReader.GetInt64(0);
                }
            }

            return nbTache;
        }
        public int GetCours(int id)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "call TacheAujCours(@id)")
                .AddParameter("@id", id)
                .Build();

            int nbTache = 0;
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    sqlReader.Read();
                    nbTache = (int)sqlReader.GetInt64(0);
                }
            }

            return nbTache;
        }
        public int GetCat(int id)
        {
            var command =
                QueryBuilder
                .Init(Connection)
                .SetQuery(
                    "call TacheAujCat(@id);")
                .AddParameter("@id", id)
                .Build();

            int nbTache = 0;
            using (command.Connection)
            {
                command.Connection.Open();

                using (MySqlDataReader sqlReader = command.ExecuteReader())
                {
                    sqlReader.Read();
                    nbTache = (int)sqlReader.GetInt64(0);
                }
            }

            return nbTache;
        }
    }
}
