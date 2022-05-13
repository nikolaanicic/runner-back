using System;
using System.Net;
using System.Text.Json;

namespace Contracts.Exceptions
{
    public class HttpException : Exception
    {
        private int httpStatus;

        public int HttpStatus { get => httpStatus; set => httpStatus = value; }


        public HttpException(HttpStatusCode status,string message):base(message)
        {
            httpStatus = (int)status;
        }


        public override string ToString()
        {
            return JsonSerializer.Serialize(new { Status = HttpStatus, Message = Message });
        }
    }
}
