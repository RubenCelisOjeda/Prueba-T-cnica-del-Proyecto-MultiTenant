using pruebaApiAuth.Application._1.Dto.Organization.AddOrganization.Request;
using pruebaApiAuth.Application._3.Common;

namespace pruebaApiAuth.Application._2.ApplicacionService.Organization
{
    /// <summary>
    /// Interface IOrganizationService
    /// </summary>
    public interface IOrganizationService
    {
        public Task<BaseApiResponse<int>> AddOrganization(AddOrganizationRequestDto itemRequest);
    }
}
