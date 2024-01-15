using Dapper;
using pruebaAppApi.Domain._2.Repository.Products;
using pruebaAppApi.Infraestructure._2.Connections;
using System.Data;

namespace pruebaAppApi.Infraestructure._1.DataAccess.Dapper.Products
{
    /// <summary>
    /// Class ProductsRepositoryDapper
    /// </summary>
    public class ProductsRepositoryDapper : IProductsRepository
    {
        #region [Properties]
        private readonly IDbConnectionFactory _connectionFactory;
        #endregion

        #region [Constructor]
        public ProductsRepositoryDapper(IDbConnectionFactory connectionFactory)
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
        public async Task<IEnumerable<pruebaAppApi.Domain._1.Entities.Products>> GetAllProduct()
        {
            using (var connection = await _connectionFactory.GetConnectionProductBD)
            {
                #region [Query]
                const string procedure = @"SELECT Id,
                                                  Name,
                                                  IdTenant

                                           FROM Product";
                #endregion

                #region [Parameters]
                #endregion

                #region [Execute]
                var response = await connection.QueryAsync<pruebaAppApi.Domain._1.Entities.Products>(procedure, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<pruebaAppApi.Domain._1.Entities.Products> GetProduct(int id)
        {
            using (var connection = await _connectionFactory.GetConnectionProductBD)
            {
                #region [Query]
                const string procedure = @"SELECT Id,
                                                  Name,
                                                  IdTenant

                                           FROM Product
                                           WHERE Id = @pId";
                #endregion

                #region [Parameters]
                var parameters = new DynamicParameters(new
                {
                    pId = id
                });
                #endregion

                #region [Execute]
                var response = await connection.QueryFirstOrDefaultAsync<pruebaAppApi.Domain._1.Entities.Products>(procedure, parameters, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<int> AddProduct(pruebaAppApi.Domain._1.Entities.Products itemRequest)
        {
            using (var connection = await _connectionFactory.GetConnectionProductBD)
            {
                #region [Query]
                const string procedure = @"INSERT INTO dbo.Product(Name,IdTenant) VALUES (@pName,@pIdTenant)";
                #endregion

                #region [Parameters]
                var parameters = new DynamicParameters(new
                {
                    pName = itemRequest.Name,
                    pIdTenant = itemRequest.IdTenant
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
        public async Task<int> UpdateProduct(pruebaAppApi.Domain._1.Entities.Products itemRequest)
        {
            using (var connection = await _connectionFactory.GetConnectionProductBD)
            {
                #region [Query]
                const string procedure = @"UPDATE Product SET Name = @pName
                                           WHERE Id = @pId";
                #endregion

                #region [Parameters]
                var parameters = new DynamicParameters(new
                {
                    pName = itemRequest.Name,
                    pId = itemRequest.Id
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
        public async Task<int> DeleteProduct(int id)
        {
            using (var connection = await _connectionFactory.GetConnectionProductBD)
            {
                #region [Query]
                const string procedure = @"DELETE FROM Product WHERE Id = @pId";
                #endregion

                #region [Parameters]
                var parameters = new DynamicParameters(new
                {
                    pId = id
                });
                #endregion

                #region [Execute]
                var response = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.Text);
                return response;
                #endregion
            }
        }
        #endregion
    }
}
