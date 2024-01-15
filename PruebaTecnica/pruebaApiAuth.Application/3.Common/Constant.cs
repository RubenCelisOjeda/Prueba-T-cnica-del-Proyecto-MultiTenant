namespace pruebaApiAuth.Application._3.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class Constant
    {
        /// <summary>
        /// 
        /// </summary>
        public struct ResponseMessage
        {
            public const string SuccessAddMessage = "POST Request successful.";

            public const string SuccessMessage = "Se ejecuto correctamente.";
            public const string WarningMessage = "No se pudo ejecutar la consulta.";
            public const string ErrorMessage = "Error al ejecutar la consulta.";
            public const string WarningDeleteMessage = "No se pudo eliminar,intente nuevamente.";
            public const string WarningAddMessage = "No se pudo registrar,intente nuevamente.";
            public const string WarningUpdateMessage = "No se pudo actualizar,intente nuevamente.";

            public const string LoginWarning = "No se puedo autenticar,usuario y/o contraseña incorrecta.";

            public const string TokenInvalid = "El token es inválido";
            public const string TokenExpire = "El token a expirado.";
            public const string Unauthorized = "El token es inválido y/o esta expirado.";

            public const string WarningExistsRecord = "Ya existe el email y/o organization,ingrese otro.";
            public const string WarningExistsOrganization = "Ya existe la organization y/o organization,ingrese otro.";
            public const string WarningNoExistsOrganization = "No existe la organización ingresada,cree una.";
        }

        /// <summary>
        /// 
        /// </summary>
        public struct ResponseCode
        {
            public const int SuccessCode = 200;
            public const int WarningCode = 400;
            public const int ErrorCode = 500;
            public const int ErrorNoControled = 500;
        }
    }
}
