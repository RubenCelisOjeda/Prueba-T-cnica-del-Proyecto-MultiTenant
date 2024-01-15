using Microsoft.Extensions.Configuration;

namespace pruebaApiAuth.Application._3.Common
{
    public static class JwtSetting
    {
        #region [Properties]
        /// <summary>
        /// 
        /// </summary>
        private static IConfiguration? _configuration;

        /// <summary>
        /// 
        /// </summary>
        public static string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string AudienceToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string IssuerToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static int Expire { get; set; }

        #endregion

        #region [Constructor]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public static void Initialize(IConfiguration configuration)
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
            key = _configuration["Jwt:key"];
            AudienceToken = _configuration["Jwt:AudienceToken"];
            IssuerToken = _configuration["Jwt:IssuerToken"];
            Expire = Convert.ToInt32(_configuration["Jwt:Expire"]);
        }
        #endregion
    }
}
