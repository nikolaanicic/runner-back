using System.Net;


namespace Contracts.Exceptions
{
    public class NotFoundException : HttpException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}
