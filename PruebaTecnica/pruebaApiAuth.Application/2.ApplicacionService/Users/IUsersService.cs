using pruebaApiAuth.Application._1.Dto.Users.AddUser.Request;
using pruebaApiAuth.Application._1.Dto.Users.Request;
using pruebaApiAuth.Application._1.Dto.Users.Response;
using pruebaApiAuth.Application._3.Common;

namespace pruebaApiAuth.Application._2.ApplicacionService.Users
{
    /// <summary>
    /// Interface UsersApplicacion
    /// </summary>
    public interface IUsersService
    {
        public Task<BaseApiResponse<LoginResponseDto>> Login(LoginRequestDto itemRequest);
        public Task<BaseApiResponse<int>> AddUser(AddUserRequestDto itemRequest);
    }
}
