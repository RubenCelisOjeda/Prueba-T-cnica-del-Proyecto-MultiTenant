namespace pruebaApiAuth.Application._1.Dto.Users.Response
{
    /// <summary>
    /// Class UsersRequestDto response
    /// </summary>
    public class LoginResponseDto
    {
        public string? AccessToken { get; set; } = string.Empty;
        public List<string> Tenansts { get; set; } = new List<string>();
    }
}
