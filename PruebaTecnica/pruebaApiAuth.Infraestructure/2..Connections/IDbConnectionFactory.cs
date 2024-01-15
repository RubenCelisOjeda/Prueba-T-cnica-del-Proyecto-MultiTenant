using System.Data;

namespace pruebaApiAuth.Infraestructure._2.Connections
{
    /// <summary>
    /// Interfaz DbConnectionFactory
    /// </summary>
    public interface IDbConnectionFactory
    {
        public Task<IDbConnection> GetConnectionAuthBD { get; }
    }
}
