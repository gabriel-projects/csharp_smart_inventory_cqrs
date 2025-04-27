using Api.GRRInnovations.SmartInventory.Domain.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using Api.GRRInnovations.SmartInventory.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IProductModel> CreateAsync(IProductModel dto)
        {
            return await _productRepository.CreateAsync(dto);
        }

        public async Task<List<IProductModel>> BulkInsertProductsAsync(List<IProductModel> dtos)
        {
            return await _productRepository.BulkInsertProductsAsync(dtos);
        }

        public async Task<List<IProductModel>> GetAllAsync(ProductOptionsPagination productOptions)
        {
             return await _productRepository.GetAllAsync(productOptions);
        }

        public async Task<List<IProductModel>> GetAllSplitQueryAsync(ProductOptionsPagination productOptions)
        {
            return await _productRepository.GetAllSplitQueryAsync(productOptions);
        }

        public Task<IProductModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
