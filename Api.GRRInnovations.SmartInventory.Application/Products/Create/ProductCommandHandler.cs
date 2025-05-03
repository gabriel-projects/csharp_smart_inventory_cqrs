using Api.GRRInnovations.SmartInventory.Domain.Abstractions;
using Api.GRRInnovations.SmartInventory.Domain.Products;
using Api.GRRInnovations.SmartInventory.Domain.Wrappers.Out;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Application.Products.Create
{
    public class ProductCommandHandler : ICommandHandler<ProductCommand<ProductModel>, CreateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CreateProductResponse>> Handle(ProductCommand<ProductModel> request, CancellationToken cancellationToken)
        {
            var wrapperIn = await request.Result();

            if (request.SupplierUid != null && request.SupplierUid != Guid.Empty)
            {
            }

            if (request.CategoryUid != null && request.SupplierUid != Guid.Empty)
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryUid);
                wrapperIn.Category = category;
            }

            var model = await _productRepository.CreateAsync(wrapperIn);

            var response = await CreateProductResponse.From(model).ConfigureAwait(false);
            return response;
        }
    }
}
