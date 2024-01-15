using Dapper;
using pruebaApiAuth.Domain._2.Repository.Users;
using pruebaApiAuth.Infraestructure._2.Connections;
using System.Data;

namespace pruebaApiAuth.Infraestructure._1.DataAccess.Dapper.Users
{
    /// <summary>
    /// Class UsersRepositoryDapper
    /// </summary>
    public class UsersRepositoryDapper : IUsersRepository
    {
        #region [Properties]
        private readonly IDbConnectionFactory _connectionFactory;
        #endregion

        #region [Constructor]
        public UsersRepositoryDapper(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Metodo que valida el usuario y contraseña.
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<string> Login(pruebaApiAuth.Domain._1.Entities.Users itemRequest)
        {
            using (var connection = await _connectionFactory.GetConnectionAuthBD)
            {
                #region [Query]
                const string procedure = @"SELECT u.Password AS Password
                                           FROM Users u
                                           WHERE u.Email = @pEmail";
                #endregion

                #region [Parameters]
                var parameters = new DynamicParameters(new
                {
                    pEmail = itemRequest.Email,
                });
                #endregion

                #region [Execute]
                var response = await connection.QueryFirstOrDefaultAsync<string>(procedure, parameters, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }

        public async Task<int> AddUser(pruebaApiAuth.Domain._1.Entities.Users itemRequest)
        {
            using (var connection = await _connectionFactory.GetConnectionAuthBD)
            {
                #region [Query]
                const string procedure = @"INSERT INTO dbo.Users(Email,Password,IdOrganization) VALUES (@pEmail,@pPassword,@pIdOrganization)";
                #endregion

                #region [Parameters]
                var parameters = new DynamicParameters(new
                {
                    pEmail = itemRequest.Email,
                    pPassword = itemRequest.Password,
                    pIdOrganization = itemRequest.IdOrganization
                });
                #endregion

                #region [Execute]
                var response = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }

        public async Task<pruebaApiAuth.Domain._1.Entities.Users> ExistsOrganizationForUser(pruebaApiAuth.Domain._1.Entities.Users itemRequest)
        {
            using (var connection = await _connectionFactory.GetConnectionAuthBD)
            {
                #region [1.Query]
                const string procedure = @"SELECT u.Email,
                                                  u.IdOrganization

                                           FROM Users u LEFT JOIN Organization o on u.IdOrganization = o.Id
                                           WHERE u.Email = @pEmail AND
                                                 u.Id = @pIdOrgnanizacion";
                #endregion

                #region [2.Parameters]
                var parameters = new DynamicParameters(new
                {
                    pEmail = itemRequest.Email,
                    pIdOrgnanizacion = itemRequest.IdOrganization,
                });
                #endregion

                #region [3.Execute]
                var response = await connection.QueryFirstOrDefaultAsync<pruebaApiAuth.Domain._1.Entities.Users>(procedure, parameters, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }
        #endregion
    }
}
