using MySql.Data.MySqlClient;

namespace OrganisateurScolaire.DataAccessLayer.Factory
{
    /// <summary>
    /// Base factory implementation containing the MySqlConnection.
    /// </summary>
    public abstract class FactoryBase
    {
        /// <summary>
        /// The MySqlConnection instance.
        /// </summary>
        private MySqlConnection _connection;

        /// <summary>
        /// Gets a clone of the current MySqlConnection instance of the factory base.
        /// </summary>
        protected MySqlConnection Connection
        {
            get => (MySqlConnection)_connection.Clone();
        }

        /// <summary>
        /// Initialises the base MySqlConnection string from the application settings.
        /// </summary>
        protected FactoryBase() { _connection = new(Properties.Settings.Default.ConnectionString); }

        /// <summary>
        /// Executes the requested NonQuery and returns if the command was successful.
        /// </summary>
        /// <param name="command">The command to execute the NonQuery on.</param>
        /// <param name="expectedAffectedRows">The number of rows that should be affected. 
        /// This is the value that is checked to determine if the command was successful or not.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        protected bool ExecuteNonQuery(MySqlCommand command, int expectedAffectedRows = 1)
        {

            bool reussi = false;
            using (command.Connection)
            {
                command.Connection.Open();

                reussi = command.ExecuteNonQuery() == expectedAffectedRows;
            }

            return reussi;
        }
    }
}
