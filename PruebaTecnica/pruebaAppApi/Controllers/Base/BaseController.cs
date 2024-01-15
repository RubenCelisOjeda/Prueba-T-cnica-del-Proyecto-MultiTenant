using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pruebaAppApi.Controllers.Base
{
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {

    }
}
