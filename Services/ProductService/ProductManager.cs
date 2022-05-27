using AutoMapper;
using Contracts.Dtos.Product.Get;
using Contracts.Dtos.Product.Post;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductService
{

    /// <summary>
    /// This service class provides functionalities for working with the products in the application
    /// </summary>
    public class ProductManager :ModelServiceBase, IProductService
    {
        public ProductManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper) : base(logger, repository, mapper)
        {
        }


        public async Task AddProduct(PostProductDto newProduct)
        {
            _repository.Products.Create(_mapper.Map<Product>(newProduct));
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<GetProductDto>> GetProducts()
        {
            return _mapper.Map<IEnumerable<GetProductDto>>(await _repository.Products.GetAllAsync(false));
        }


        /// <summary>
        /// This method removes a product
        /// This is maybe not needed, check the specification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveProduct(long id)
        {
            var product = await _repository.Products.GetByIdAsync(id, false);

            if (product == null)
                throw new NotFoundException($"Product with the id:{id} doesn't exist");

            _repository.Products.Delete(product);

            await _repository.SaveAsync();
        }
    }
}
