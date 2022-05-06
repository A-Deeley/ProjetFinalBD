using MySql.Data.MySqlClient;

namespace OrganisateurScolaire.DAL.Factory
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
    }
}
