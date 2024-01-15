using AutoMapper;
using pruebaApiAuth.Application._1.Dto.Organization.AddOrganization.Request;
using pruebaApiAuth.Application._1.Dto.Users.AddUser.Request;
using pruebaApiAuth.Application._1.Dto.Users.Request;
using pruebaApiAuth.Application._1.Dto.Users.Response;

namespace pruebaApiAuth.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region [Users]

            #region [AddUser]

            #region [Request]
            CreateMap<AddUserRequestDto, pruebaApiAuth.Domain._1.Entities.Users>();
            #endregion

            #endregion

            #region [Login]

            #region [Request]
            CreateMap<LoginRequestDto, pruebaApiAuth.Domain._1.Entities.Users>();
            #endregion

            #region [Response]
            CreateMap<pruebaApiAuth.Domain._1.Entities.Users, LoginResponseDto>();
            #endregion

            #endregion

            #endregion

            #region [Organization]

            #region [AddOrganization]

            #region [Request]
            CreateMap<AddOrganizationRequestDto, pruebaApiAuth.Domain._1.Entities.Organization>();
            #endregion

            #endregion

            #endregion
        }
    }
}
