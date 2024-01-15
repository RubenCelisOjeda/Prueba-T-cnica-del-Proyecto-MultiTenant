namespace pruebaAppApi.Application._3.Common
{
    /// <summary>
    /// Clase que representa el response para las apis
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseApiResponse<T>
    {
        #region [Attributes]
        /// <summary>
        /// Codigo de Respuesta
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// Mensaje de respuesta
        /// </summary>
        public string statusText { get; set; }

        /// <summary>
        /// Data de la respuesta
        /// </summary>
        public T Data { get; set; }
        #endregion
    }
}
