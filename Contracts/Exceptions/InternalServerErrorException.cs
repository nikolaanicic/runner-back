using System.Net;


namespace Contracts.Exceptions
{
    public class InternalServerErrorException : HttpException
    {
        public InternalServerErrorException(string message) : base(HttpStatusCode.InternalServerError, message)
        {
        }
    }
}
