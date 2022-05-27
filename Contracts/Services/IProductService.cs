using Contracts.Dtos.Product.Get;
using Contracts.Dtos.Product.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IProductService
    {

        Task AddProduct(PostProductDto newProduct);
        Task<IEnumerable<GetProductDto>> GetProducts();
        Task RemoveProduct(long id);
    }
}
