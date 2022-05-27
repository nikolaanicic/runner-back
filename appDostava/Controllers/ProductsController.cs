using appDostava.Filters.CurrentUser;
using appDostava.Filters.ValidationFilter;
using Contracts.Dtos.Product.Post;
using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace appDostava.Controllers
{
    /// <summary>
    /// This controller should provide api calls that work with items
    /// 
    /// 
    /// 1. Items are added by the admins of the app
    /// 2. Items can be seen by the consumers and admins
    /// </summary>


    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("all")]
        [Authorize(Roles = RolesConstants.AdminConsumer)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetProducts());
        }


        [HttpPost("add")]
        [Authorize(Roles = RolesConstants.Admin)]
        [ServiceFilter(typeof(DtoValidationFilter<PostProductDto>))]
        public async Task<IActionResult> CreateProduct([FromBody]PostProductDto newProduct)
        {
            await _productService.AddProduct(newProduct);
            return NoContent();
        }
    }
}
