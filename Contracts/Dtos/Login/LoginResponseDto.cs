namespace Contracts.Dtos.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
    }
}
