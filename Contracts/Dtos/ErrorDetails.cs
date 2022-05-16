using System.Text.Json;

namespace Contracts.Dtos
{
    /// <summary>
    /// This class is the error class dto which will be sent to the user
    /// Exceptions thrown in the service layer will be picked up by the exception middleware
    /// And will be translated to an object of this class
    /// </summary>
    public class ErrorDetails
    {

        /// <summary>
        /// Http Status code, depends on the type of the thrown exception
        /// If the exception type is not HttpException than statuscode will be 500
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Message is the message passed in the constructor of the thrown exception
        /// </summary>
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
