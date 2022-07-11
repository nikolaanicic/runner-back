namespace Contracts.Dtos.User.Post
{
    public class RefreshTokenPostDto
    {
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
