using Api.GRRInnovations.SmartInventory.Domain.Abstractions;
using Api.GRRInnovations.SmartInventory.Domain.Products;
using Api.GRRInnovations.SmartInventory.Domain.Wrappers.In;
using Api.GRRInnovations.SmartInventory.Domain.Wrappers.Out;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using Api.GRRInnovations.SmartInventory.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection;
using System.Threading;
using Api.GRRInnovations.SmartInventory.Application.Products.Create;
using Api.GRRInnovations.SmartInventory.Extensions;
using Api.GRRInnovations.SmartInventory.Infrastructure;
using Api.GRRInnovations.SmartInventory.Application.Products.Get;

namespace Api.GRRInnovations.SmartInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISender _sender;

        public ProductsController(IProductService productService, ICategoryService categoryService, ISender sender)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sender = sender;
        }

        [HttpPost]
        public async Task<IResult> Create([FromBody] ProductCommand<ProductModel> command)
        {
            Result<CreateProductResponse> result = await _sender.Send(command);
            return result.Match(Results.Ok, CustomResults.Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? name,
            [FromQuery] Guid? categoryId,
            [FromQuery] Guid? supplierId,
            [FromQuery] EOrderByType orderBy = EOrderByType.Name,
            [FromQuery] EOrderByDirection orderDirection = EOrderByDirection.Ascending,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var options = new ProductOptionsPagination();

            if (name != null)
                options.FilterNames = new List<string> { name };

            if (categoryId != null)
                options.FilterCategoriesUids = new List<Guid> { categoryId.Value };

            if (supplierId != null)
                options.FilterSupplierUids = new List<Guid> { supplierId.Value };

            options.OrderBy = orderBy;
            options.OrderDirection = orderDirection;

            options.Page = page;
            options.PageSize = pageSize;

            options.AsNoTracking = true;

            var products = await _productService.GetAllAsync(options);

            //todo: mudar para pagination result
            var response = await ProductResponse.From(products).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);

            var response = await ProductResponse.From(product).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        //todo: criar endpoint com search usando like para buscar pelo nome do produto
    }
}
