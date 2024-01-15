using pruebaApiAuth.Infraestructure._3.Common;
using pruebaApiAuth.Infraestructure._4.Common;
using System.Data;
using System.Data.SqlClient;

namespace pruebaApiAuth.Infraestructure._2.Connections
{
    /// <summary>
    /// Class DbConnectionFactory
    /// </summary>
    public class DbConnectionFactory : IDbConnectionFactory
    {
        #region [Properties]
        #endregion

        #region [Connections]
        public Task<IDbConnection> GetConnectionAuthBD => this.CreateConnection(DataBaseType.SqlServer, AppConfig.AuthBD);
        #endregion

        #region [Functions]
        /// <summary>
        /// Function what permitied create connection
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IDbConnection> CreateConnection(DataBaseType databaseType, string connectionString)
        {
            switch (databaseType)
            {
                case DataBaseType.SqlServer:
                    {
                        var _connection = new SqlConnection(connectionString);
                        await _connection.OpenAsync();

                        return _connection;
                    }

                default:
                    throw new ArgumentException("Invalid database type");
            }
        }
        #endregion
    }
}
