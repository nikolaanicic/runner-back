using System.Net;


namespace Contracts.Exceptions
{
    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}
