using System.Net;

namespace Contracts.Exceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }
    }
}
