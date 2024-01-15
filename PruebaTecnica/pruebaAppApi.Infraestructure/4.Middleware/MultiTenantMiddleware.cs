using Microsoft.AspNetCore.Http;

namespace pruebaAppApi.Infraestructure._4.Middleware
{
    public class MultiTenantMiddleware
    {
        private readonly RequestDelegate _next;

        public MultiTenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Obtener el tenant del path de la URL
            var tenantId = context.Request.Path.Value.Split('/')[1];

            // Guardar el ID del tenant en el contexto
            context.Items["TenantId"] = tenantId;

            // Continuar con la ejecución de la solicitud
            await _next(context);
        }
    }
}
