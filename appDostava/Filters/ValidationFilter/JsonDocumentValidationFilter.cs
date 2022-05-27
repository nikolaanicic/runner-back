using appDostava.Extensions;
using Contracts.Dtos;
using Contracts.Logger;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appDostava.Filters.ValidationFilter
{

    /// <summary>
    /// This filter is used to validate patch request dtos
    /// Every patch dto  has to inherit from PatchDtoValidator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonDocumentValidationFilter<T> : IAsyncActionFilter
        where T : ICanBeValidated,new()
    {

        private ILoggerManager _logger;

        public JsonDocumentValidationFilter(ILoggerManager logger)
        {
            _logger = logger;
        }



        /// <summary>
        /// This method does validation on values in the json patch documents
        /// using the validation attributes set in the dto class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            string operations = "";
            context.HttpContext.Request.Body.Position = 0;

            using(StreamReader reader = new StreamReader(context.HttpContext.Request.Body,Encoding.UTF8))
            {
                operations = await reader.ReadToEndAsync();
            }


            // Deserializes the request body to a list of patch document operations
            var operationList = JsonConvert.DeserializeObject<List<Operation>>(operations);

            // Creates the patch document with the deserialized operation list
            var patchDocument = new JsonPatchDocument(operationList, new DefaultContractResolver());


            // Creates the object so that the patch document can ba applied to it and the new values validated
            ICanBeValidated t = new T();
            patchDocument.ApplyTo(t);


            // Gets the operation paths i.e. class fields that should be validated
            var fields = (from x in patchDocument.Operations select x.path.Substring(1).CapitalizeFirstLetter()).ToList();

            var validationResult = t.IsValid(fields);

            if(validationResult.Count != 0)
            {
                context.Result = new UnprocessableEntityObjectResult(validationResult);
                _logger.LogError(validationResult[0].ErrorMessage);
            }
            else
                await next();
        }


    }
}
