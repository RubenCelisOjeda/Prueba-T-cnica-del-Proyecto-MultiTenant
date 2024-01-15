namespace pruebaApiAuth.Domain._2.Repository.Users
{
    /// <summary>
    /// Interfaz IUsersApplicacion
    /// </summary>
    public interface IUsersRepository
    {
        public Task<string> Login(pruebaApiAuth.Domain._1.Entities.Users itemRequest);
        public Task<pruebaApiAuth.Domain._1.Entities.Users> ExistsOrganizationForUser(pruebaApiAuth.Domain._1.Entities.Users itemRequest);
        public Task<int> AddUser(pruebaApiAuth.Domain._1.Entities.Users itemRequest);
    }
}
