using System.Net;

namespace Contracts.Exceptions
{
    public class SecurityException : HttpException
    {
        public SecurityException(string message) : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}
