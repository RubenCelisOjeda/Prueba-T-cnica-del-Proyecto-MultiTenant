using System.Data;

namespace pruebaAppApi.Infraestructure._2.Connections
{
    /// <summary>
    /// Interfaz DbConnectionFactory
    /// </summary>
    public interface IDbConnectionFactory
    {
        public Task<IDbConnection> GetConnectionProductBD { get; }
        public Task<IDbConnection> GetConnectionBD { get; }
    }
}
