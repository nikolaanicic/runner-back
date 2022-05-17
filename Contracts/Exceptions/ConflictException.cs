using System.Net;

namespace Contracts.Exceptions
{
    public class ConflictException : HttpException
    {
        public ConflictException(string message) : base(HttpStatusCode.Conflict, message)
        {
        }
    }
}
