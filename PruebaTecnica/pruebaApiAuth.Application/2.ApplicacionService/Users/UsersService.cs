using AutoMapper;
using Microsoft.Extensions.Logging;
using pruebaApiAuth.Application._1.Dto.Users.AddUser.Request;
using pruebaApiAuth.Application._1.Dto.Users.Request;
using pruebaApiAuth.Application._1.Dto.Users.Response;
using pruebaApiAuth.Application._3.Common;
using pruebaApiAuth.Domain._2.Repository.Organization;
using pruebaApiAuth.Domain._2.Repository.Users;

namespace pruebaApiAuth.Application._2.ApplicacionService.Users
{
    /// <summary>
    /// Class UsersApplicacion
    /// </summary>
    public class UsersService : IUsersService
    {
        #region [Properties]
        private readonly ILogger<UsersService> _logger;
        private readonly IUsersRepository _usersRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;
        #endregion

        #region [Constructor]
        public UsersService(ILogger<UsersService> logger, IUsersRepository usersRepository, IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<LoginResponseDto>> Login(LoginRequestDto itemRequest)
        {
            BaseApiResponse<LoginResponseDto> baseResponse = null;
            string token = string.Empty;

            try
            {
                _logger.LogInformation($"------Login Started------");

                #region [1.Login]
                var itemRequestMapper = _mapper.Map<pruebaApiAuth.Domain._1.Entities.Users>(itemRequest);

                var responseLoginIn = await _usersRepository.Login(itemRequestMapper);

                if (!string.IsNullOrEmpty(responseLoginIn) && responseLoginIn.Length > 0)
                {
                    #region [2.Verifica el password]
                    var isValid = PasswordHelper.VerifyPassword(itemRequest.Password, responseLoginIn);

                    _logger.LogInformation("------VerifyPassword Successfully-----");

                    if (isValid)
                    {
                        //5.Genera el token
                        token = await JwtGenerator.GenerateToken(itemRequest.Email);
                        _logger.LogInformation("------GenerateTokenRebecca Successfully-----");

                        //6.Response
                        baseResponse = new BaseApiResponse<LoginResponseDto>();
                        baseResponse.Data = new LoginResponseDto();
                        baseResponse.Data.AccessToken = token;
                        baseResponse.status = Constant.ResponseCode.SuccessCode;
                        baseResponse.statusText = Constant.ResponseMessage.SuccessMessage;

                    }
                    else
                    {
                        baseResponse = new BaseApiResponse<LoginResponseDto>();
                        baseResponse.status = Constant.ResponseCode.WarningCode;
                        baseResponse.statusText = Constant.ResponseMessage.LoginWarning;
                        _logger.LogWarning("------VerifyPassword UnSuccessfully-----");
                    }
                    #endregion
                }
                else
                {
                    baseResponse = new BaseApiResponse<LoginResponseDto>();
                    baseResponse.status = Constant.ResponseCode.WarningCode;
                    baseResponse.statusText = Constant.ResponseMessage.LoginWarning;

                    _logger.LogWarning("------Login UnSuccessfully------");
                }
                #endregion

                _logger.LogInformation("------Login Finished------");
            }
            catch (Exception ex)
            {
                baseResponse = new BaseApiResponse<LoginResponseDto>();
                baseResponse.status = Constant.ResponseCode.ErrorCode;
                baseResponse.statusText = Constant.ResponseMessage.ErrorMessage;

                _logger.LogError(ex.Message);
            }
            return baseResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<int>> AddUser(AddUserRequestDto itemRequest)
        {
            try
            {
                _logger.LogInformation("------AddUser Started------");

                #region [1.Validamos el usuario]
                var existsUsermapperRequest = _mapper.Map<pruebaApiAuth.Domain._1.Entities.Users>(itemRequest);
                var responseExistsUser = await _usersRepository.ExistsOrganizationForUser(existsUsermapperRequest);

                if (responseExistsUser != null)
                {
                    if (!string.IsNullOrEmpty(responseExistsUser.Email) && responseExistsUser.IdOrganization > 0)
                    {
                        return new BaseApiResponse<int>
                        {
                            status = Constant.ResponseCode.WarningCode,
                            statusText = string.Format(Constant.ResponseMessage.WarningExistsRecord, "email"),
                        };
                    }
                }

                _logger.LogInformation("------ExistsUser Successfully------");
                #endregion

                #region [2.Validamos la organizacion]
                var responseExistsOrganization = await _organizationRepository.ExistsOrganization(itemRequest.IdOrganization);
                if (!responseExistsOrganization)
                {
                    return new BaseApiResponse<int>
                    {
                        status = Constant.ResponseCode.SuccessCode,
                        statusText = string.Format(Constant.ResponseMessage.WarningNoExistsOrganization, "organization"),
                    };
                }

                _logger.LogInformation("------ExistsUser Successfully------");
                #endregion

                #region [3.Encryptamos la contraseña]
                var passwordHash = PasswordHelper.EncryptPassword(itemRequest.Password);
                _logger.LogInformation("------EncryptPassword Successfully------");
                #endregion

                #region [4.Creamos el nuevo usuario]
                itemRequest.Password = passwordHash;
                var itemRequestMapper = _mapper.Map<pruebaApiAuth.Domain._1.Entities.Users>(itemRequest);
                var response = await _usersRepository.AddUser(itemRequestMapper);

                return new BaseApiResponse<int>
                {
                    status = response > 0 ? Constant.ResponseCode.SuccessCode : Constant.ResponseCode.WarningCode,
                    statusText = response > 0 ? Constant.ResponseMessage.SuccessAddMessage : Constant.ResponseMessage.WarningAddMessage,
                };
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BaseApiResponse<int>
                {
                    status = Constant.ResponseCode.ErrorCode,
                    statusText = Constant.ResponseMessage.ErrorMessage
                };
            }
            finally
            {
                _logger.LogInformation("------AddUser Finished------");
            }

        }
        #endregion
    }
}
