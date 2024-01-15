namespace pruebaApiAuth.Domain._2.Repository.Organization
{
    public interface IOrganizationRepository
    {
        public Task<bool> ExistsOrganization(int idOrganizacion);
        public Task<bool> ExistsOrganization(string name);
        public Task<int> AddOrganization(pruebaApiAuth.Domain._1.Entities.Organization itemRequest);
        public Task ExecuteScript(string scriptFileName, string databaseName);
    }
}
