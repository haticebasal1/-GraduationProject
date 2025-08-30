using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.API.Controllers.BaseController;
using PhoneCase.Business.Abstract;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.API.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : CustomControllerBase
    {
        private readonly IProductService _productManager;
        private readonly ICategoryService _categoryManager;

        public ProductsController(IProductService productManager, ICategoryService categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto productCreateDto)
        {
            var response = await _productManager.AddAsync(productCreateDto);
            return CreateResult(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateDto productUpdateDto)
        {
            var response = await _productManager.UpdateAsync(productUpdateDto);
            return CreateResult(response);
        }
        [HttpDelete("delete/hard")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var response = await _productManager.HardDeletedAsync(id);
            return CreateResult(response);
        }
        [HttpPut("delete/soft")]
        public async Task<IActionResult> SoftDelete([FromQuery] int id)
        {
            var response = await _productManager.SoftDeletedAsync(id);
            return CreateResult(response);
        }
        [HttpPut("isHome")]
        public async Task<IActionResult> UpdateIsHome([FromQuery] int id)
        {
            var response = await _productManager.UpdateIsHomeAsync(id);
            return CreateResult(response);
        }
        [HttpPut("delete/bycategoryId")]
        public async Task<IActionResult> SoftDeletedByCategoryId([FromQuery] int categoryId)
        {
            var response = await _productManager.SoftDeletedByCategoryIdAsync(categoryId);
            return CreateResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeCategories = false)
        {
            var response = await _productManager.GetAsync(id, includeCategories);
            return CreateResult(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeCategories = false, [FromQuery] int? categoryId = null)
        {
            var response = await _productManager.GetAllAsync(includeCategories, categoryId);
            return CreateResult(response);
        }
        [HttpGet("deleted")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var response = await _productManager.GetAllDeletedAsync();
            return CreateResult(response);
        }
        [HttpGet("home")]
        public async Task<IActionResult> GetAllHomePage()
        {
            var response = await _productManager.GetAllHomePageAsync();
            return CreateResult(response);
        }
        [HttpGet("count")]
        public async Task<IActionResult> Count([FromQuery] int? categoryId = null)
        {
            var response = await _productManager.CountAsync(false , categoryId);
            return CreateResult(response);
        }
        [HttpGet("admin/count")]
        public async Task<IActionResult> Count([FromQuery] bool? isdeleted = null, [FromQuery] int? categoryId = null)
        {
            var response = await _productManager.CountAsync(isdeleted, categoryId);
            return CreateResult(response);
        }
        [HttpGet("admin/categories-with-product-count")]
        public async Task<IActionResult> GetCategoriesWithProductCount()
        {
            var categoriesResponse = await _categoryManager.GetAllAsync();
            var categoryDtos = new List<CategoryDto>();
            foreach (var category in categoriesResponse.Data)
            {
                var productCount = await _productManager.CountAsync(null!, category.Id);
                categoryDtos.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    ProductCount = category.ProductCount
                });
            }
            var response = new ResponseDto<List<CategoryDto>>
            {
                Data = categoryDtos,
                IsSuccessful = true
            };
            return CreateResult(response);

        }
    }
}
