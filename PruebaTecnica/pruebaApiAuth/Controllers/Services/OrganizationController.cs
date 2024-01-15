using Microsoft.AspNetCore.Mvc;
using pruebaApiAuth.Application._1.Dto.Organization.AddOrganization.Request;
using pruebaApiAuth.Application._2.ApplicacionService.Organization;
using pruebaApiAuth.Controllers.Base;

namespace pruebaApiAuth.Controllers.Services
{
    [Route("organization")]
    [ApiController]
    public class OrganizationController : BaseController
    {
        #region [Properties]
        private readonly IOrganizationService _organizationService;
        #endregion

        #region [Constructor]
        public OrganizationController(IOrganizationService organizationService) => _organizationService = organizationService;
        #endregion

        #region [Apis]
        [HttpPost("addOrganization")]
        public async Task<IActionResult> AddOrganization([FromBody] AddOrganizationRequestDto itemRequest)
        {
            var response = await _organizationService.AddOrganization(itemRequest);
            return Ok(response);
        }
        #endregion
    }
}
