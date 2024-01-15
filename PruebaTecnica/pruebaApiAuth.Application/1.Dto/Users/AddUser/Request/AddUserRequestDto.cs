namespace pruebaApiAuth.Application._1.Dto.Users.AddUser.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class AddUserRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdOrganization { get; set; }
    }
}
