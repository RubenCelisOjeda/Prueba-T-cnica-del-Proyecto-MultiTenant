using Microsoft.AspNetCore.Mvc;
using pruebaApiAuth.Application._1.Dto.Users.AddUser.Request;
using pruebaApiAuth.Application._1.Dto.Users.Request;
using pruebaApiAuth.Application._2.ApplicacionService.Users;
using pruebaApiAuth.Controllers.Base;

namespace pruebaApiAuth.Controllers.Services
{
    [Route("users")]
    [ApiController]
    public class UsersController : BaseController
    {
        #region [Properties]
        private readonly IUsersService _usersService;
        #endregion

        #region [Constructor]
        public UsersController(IUsersService usersService) => _usersService = usersService;

        #endregion

        #region [Apis]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto itemRequest)
        {
            var response = await _usersService.Login(itemRequest);
            return Ok(response);
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto itemRequest)
        {
            var response = await _usersService.AddUser(itemRequest);
            return Ok(response);
        }
        #endregion
    }
}
