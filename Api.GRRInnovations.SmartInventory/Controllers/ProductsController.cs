using Api.GRRInnovations.SmartInventory.Domain.Entities;
using Api.GRRInnovations.SmartInventory.Domain.Wrappers.In;
using Api.GRRInnovations.SmartInventory.Domain.Wrappers.Out;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using Api.GRRInnovations.SmartInventory.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.GRRInnovations.SmartInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WrapperInProduct<ProductModel> dto)
        {
            var wrapperIn = await dto.Result();

            if (dto.SupplierUid != null)
            {
            }

            if (dto.CategoryUid != null)
            {
                var category = await _categoryService.GetByIdAsync(dto.CategoryUid);
                wrapperIn.Category = category;
            }

            var model = await _productService.CreateAsync(wrapperIn);

            var response = await WrapperOutProduct.From(model).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpPost(nameof(BulkInsertProductsAsync))]
        public async Task<IActionResult> BulkInsertProductsAsync([FromBody] List<WrapperInProduct<ProductModel>> dtos)
        {
            var products = new List<IProductModel>();

            foreach (var d in dtos)
            {
                var wrapperIn = await d.Result();
                if (d.SupplierUid != null)
                {
                    //var supplier = await _supplierService.GetByIdAsync(d.SupplierUid);
                    //wrapperIn.Supplier = supplier;
                }
                if (d.CategoryUid != null)
                {
                    var category = await _categoryService.GetByIdAsync(d.CategoryUid);
                    wrapperIn.Category = category;
                }

                wrapperIn.Uid = Guid.NewGuid();

                products.Add(wrapperIn);
            }

            var model = await _productService.BulkInsertProductsAsync(products);

            var response = await WrapperOutProduct.From(model).ConfigureAwait(false);
            return new OkObjectResult(response);
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
            var response = await WrapperOutProduct.From(products).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpGet(nameof(GetAllTestLazyLoading))]
        public async Task<IActionResult> GetAllTestLazyLoading()
        {
            var products = await _productService.GetAllAsync(new ProductOptionsPagination()
            {
                PageSize = 9999
            });

            foreach (var product in products)
            {
                //select * from category for each product
                Console.WriteLine($"Category: {product.Category?.Name}");
            }

            var response = await WrapperOutProduct.From(products).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpGet(nameof(GetAllTestSplitQuery))]
        public async Task<IActionResult> GetAllTestSplitQuery()
        {
            var products = await _productService.GetAllSplitQueryAsync(new ProductOptionsPagination()
            {
                PageSize = 9999
            });

            foreach (var product in products)
            {
                //select * from category for each product
                Console.WriteLine($"Category: {product.Category?.Name}");
            }

            var response = await WrapperOutProduct.From(products).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);

            var response = await WrapperOutProduct.From(product).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        //todo: criar endpoint com search usando like para buscar pelo nome do produto
    }
}
