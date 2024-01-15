using AutoMapper;
using Microsoft.Extensions.Logging;
using pruebaApiAuth.Application._1.Dto.Organization.AddOrganization.Request;
using pruebaApiAuth.Application._3.Common;
using pruebaApiAuth.Domain._2.Repository.Organization;

namespace pruebaApiAuth.Application._2.ApplicacionService.Organization
{
    /// <summary>
    /// 
    /// </summary>
    public class OrganizationService : IOrganizationService
    {
        #region [Properties]
        private readonly ILogger<OrganizationService> _logger;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;
        #endregion

        #region [Constructor]
        public OrganizationService(ILogger<OrganizationService> logger, IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _logger = logger;
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
        public async Task<BaseApiResponse<int>> AddOrganization(AddOrganizationRequestDto itemRequest)
        {
            try
            {
                _logger.LogInformation("------AddOrganization Started------");

                #region [1.Validamos la organizacion]
                var existsUsermapperRequest = _mapper.Map<pruebaApiAuth.Domain._1.Entities.Organization>(itemRequest);
                var responseExistsOrganization = await _organizationRepository.ExistsOrganization(existsUsermapperRequest.Name);

                if (responseExistsOrganization)
                {
                    return new BaseApiResponse<int>
                    {
                        status = Constant.ResponseCode.SuccessCode,
                        statusText = string.Format(Constant.ResponseMessage.WarningExistsOrganization, "organization"),
                        Data = 1
                    };
                }

                _logger.LogInformation("------ExistsOrganization Successfully------");
                #endregion

                #region [2.Creamos nueva organization]
                var itemRequestMapper = _mapper.Map<pruebaApiAuth.Domain._1.Entities.Organization>(itemRequest);
                itemRequestMapper.SlugTenant = Guid.NewGuid().ToString();

                var response = await _organizationRepository.AddOrganization(itemRequestMapper);
                #endregion

                #region [3.Ejecutamos el script después de crear la base de datos]
                var newDatabaseName = $"{"DB"}_{itemRequest.Name}";
                await _organizationRepository.ExecuteScript("CreateDataBase.sql", newDatabaseName);
                #endregion

                #region [4.Response]
                return new BaseApiResponse<int>
                {
                    status = response > 0 ? Constant.ResponseCode.SuccessCode : Constant.ResponseCode.WarningCode,
                    statusText = response > 0 ? Constant.ResponseMessage.SuccessAddMessage : Constant.ResponseMessage.WarningAddMessage,
                    Data = responseExistsOrganization ? 1 : 0
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
                _logger.LogInformation("------AddOrganization Finished------");
            }
        }
        #endregion

        #region [Functions]

        #endregion
    }
}
