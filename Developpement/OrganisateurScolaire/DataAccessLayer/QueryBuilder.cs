using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganisateurScolaire.DataAccessLayer
{
    public class QueryBuilder
    {
        private readonly MySqlCommand _command;

        private QueryBuilder(MySqlConnection connection)
        {
            _command = new();
            _command.Connection = connection;
        }

        public static QueryBuilder Init(MySqlConnection connection)
        {
            return new QueryBuilder(connection);
        }
        
        public QueryBuilder AddParameter(string parameterName, object value)
        {
            _command.Parameters.AddWithValue(parameterName, value);
            return this;
        }

        public QueryBuilder SetQuery(string query)
        {
            _command.CommandText = query;
            return this;
        }

        public MySqlCommand Build() => _command;
    }
}
