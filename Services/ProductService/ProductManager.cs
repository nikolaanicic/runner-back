﻿using AutoMapper;
using Contracts.Dtos.Product.Get;
using Contracts.Dtos.Product.Post;
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
    }
}