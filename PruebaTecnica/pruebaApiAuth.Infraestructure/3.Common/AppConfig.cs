using Microsoft.Extensions.Configuration;

namespace pruebaApiAuth.Infraestructure._3.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppConfig
    {
        #region [Properties]
        /// <summary>
        /// 
        /// </summary>
        private static IConfiguration? _configuration;

        /// <summary>
        /// 
        /// </summary>
        public static string AuthBD;
        #endregion

        #region [Constructor]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public static void Initialize(IConfiguration? configuration)
        {

            _configuration = configuration;
            Load();
        }
        #endregion

        #region [Functions]
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static void Load()
        {
            AuthBD = _configuration["DataBaseConnection:AuthBD"];
        }
        #endregion
    }
}
