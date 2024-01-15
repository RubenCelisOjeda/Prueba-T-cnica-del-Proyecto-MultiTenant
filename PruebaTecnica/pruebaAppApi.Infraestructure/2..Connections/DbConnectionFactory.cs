using Dapper;
using Microsoft.AspNetCore.Http;
using pruebaAppApi.Infraestructure._3.Common;
using System.Data;
using System.Data.SqlClient;

namespace pruebaAppApi.Infraestructure._2.Connections
{
    /// <summary>
    /// Class DbConnectionFactory
    /// </summary>
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbConnectionFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Task<IDbConnection> GetConnectionProductBD => CreateConnection(DataBaseType.SqlServer, AppConfig.ProductBD);
        public Task<IDbConnection> GetConnectionBD => CreateConnection(DataBaseType.SqlServer_Auth, AppConfig.AuthBD);

        public async Task<IDbConnection> CreateConnection(DataBaseType databaseType, string connectionString)
        {
            switch (databaseType)
            {
                case DataBaseType.SqlServer:
                    {
                        #region [1.Obtenemos el tenant]
                        var slugTenant = _httpContextAccessor.HttpContext.Items["TenantId"] as string;
                        if (string.IsNullOrEmpty(slugTenant))
                        {
                            throw new InvalidOperationException("Tenant ID not found in the context.");
                        }
                        #endregion

                        #region [2.Obtenemos el nombre de la orgzanizacion]
                        var databaseName = await this.GetDataBaseByTenantId(slugTenant);
                        #endregion

                        #region [3.Formateamos la conexion ]
                        var newDatabaseName = $"{"database="}{"DB"}_{databaseName};";
                        string newConnectionString = connectionString.Replace("database=;", newDatabaseName);
                        #endregion

                        #region [4.Abrimos conexion]
                        var connection = new SqlConnection(newConnectionString);
                        await connection.OpenAsync(); 
                        #endregion

                        return connection;
                    }
                    case DataBaseType.SqlServer_Auth:
                    {
                        #region [1.Abrimos conexion]
                        var connection = new SqlConnection(connectionString);
                        await connection.OpenAsync();
                        #endregion

                        return connection;
                    }

                default:
                    throw new ArgumentException("Invalid database type");
            }
        }

        private async Task<string> GetDataBaseByTenantId(string slugTenant)
        {
            using (var connection = await this.GetConnectionBD)
            {
                #region [1.Query]
                const string query = @"SELECT Name
                                       FROM dbo.Organization
                                       WHERE SlugTenant = @pSlugTenant";
                #endregion

                #region [2.Parameters]
                var parameters = new DynamicParameters(new
                {
                    pSlugTenant = slugTenant
                });
                #endregion

                #region [3.Execute]
                var response = await connection.QueryFirstOrDefaultAsync<string>(query, parameters);
                return response;
                #endregion
            }
        }
    }
}
