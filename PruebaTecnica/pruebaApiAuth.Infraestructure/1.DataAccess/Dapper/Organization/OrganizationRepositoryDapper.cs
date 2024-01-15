using Dapper;
using pruebaApiAuth.Domain._2.Repository.Organization;
using pruebaApiAuth.Infraestructure._2.Connections;
using System.Data;

namespace pruebaApiAuth.Infraestructure._1.DataAccess.Dapper.Organization
{
    /// <summary>
    /// Class OrganizationRepositoryDapper
    /// </summary>
    public class OrganizationRepositoryDapper : IOrganizationRepository
    {
        #region [Properties]
        private readonly IDbConnectionFactory _connectionFactory;
        #endregion

        #region [Constructor]
        public OrganizationRepositoryDapper(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<int> AddOrganization(pruebaApiAuth.Domain._1.Entities.Organization itemRequest)
        {
            using (var connection = await _connectionFactory.GetConnectionAuthBD)
            {
                #region [Query]
                const string procedure = @"INSERT INTO dbo.Organization(Name,SlugTenant) VALUES (@pName,@pSlugTenant)";
                #endregion

                #region [Parameters]
                var parameters = new DynamicParameters(new
                {
                    pName = itemRequest.Name,
                    pSlugTenant = itemRequest.SlugTenant
                });
                #endregion

                #region [Execute]
                var response = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<bool> ExistsOrganization(int idOrganizacion)
        {
            using (var connection = await _connectionFactory.GetConnectionAuthBD)
            {
                #region [1.Query]
                const string procedure = @"SELECT COUNT(1)
                                           FROM Organization u
                                           WHERE u.Id = @pIdOrganizacion";
                #endregion

                #region [2.Parameters]
                var parameters = new DynamicParameters(new
                {
                    pIdOrganizacion = idOrganizacion
                });
                #endregion

                #region [3.Execute]
                var response = await connection.QueryFirstOrDefaultAsync<bool>(procedure, parameters, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> ExistsOrganization(string name)
        {
            using (var connection = await _connectionFactory.GetConnectionAuthBD)
            {
                #region [1.Query]
                const string procedure = @"SELECT COUNT(1)
                                           FROM Organization u
                                           WHERE u.Name = @pName";
                #endregion

                #region [2.Parameters]
                var parameters = new DynamicParameters(new
                {
                    pName = name
                });
                #endregion

                #region [3.Execute]
                var response = await connection.QueryFirstOrDefaultAsync<bool>(procedure, parameters, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptFileName"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public async Task ExecuteScript(string scriptFileName, string databaseName)
        {
            // Ruta base de la aplicación
            string baseDirectory = @"D:\GitHub\PruebaTecnica\pruebaApiAuth.Infraestructure";

            // Ruta relativa del script en la carpeta de migración
            string relativePath = Path.Combine("4.Migration", scriptFileName);

            // Ruta completa del script
            string scriptPath = Path.Combine(baseDirectory, relativePath);

            if (File.Exists(scriptPath))
            {
                var script = File.ReadAllText(scriptPath);

                script = script.Replace("{DatabaseName}", databaseName);

                using (var connection = await _connectionFactory.GetConnectionAuthBD)
                {
                    await connection.ExecuteAsync(script);
                }
            }
        }
        #endregion
    }
}
