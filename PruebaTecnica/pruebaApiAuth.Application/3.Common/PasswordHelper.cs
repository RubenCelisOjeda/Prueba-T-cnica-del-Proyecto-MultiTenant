namespace pruebaApiAuth.Application._3.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Método para encriptar una contraseña
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {
            var salt = GenerateSalt();
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashPassword;
        }

        /// <summary>
        /// Método para generar una salto aleatoria   
        /// </summary>
        /// <returns></returns>
        public static string GenerateSalt()
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return salt;
        }

        /// <summary>
        /// Método para verificar si una contraseña coincide con su hash encriptado
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var verify = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return verify;
        }
    }
}
