using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.API.Controllers.BaseController;
using PhoneCase.Business.Abstract;

namespace PhoneCase.API.Controllers
{
    [Route("categoriestype")]
    [ApiController]
    public class CategoriesTypeController : CustomControllerBase
    {
        private readonly ICategoryTypeService _categoryTypeService;

        public CategoriesTypeController(ICategoryTypeService categoryTypeService)
        {
            _categoryTypeService = categoryTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryTypeService.GetAllTypeAsync();
            return CreateResult(response);
        }

        [HttpGet("{value}")]
        public async Task<IActionResult> GetByValue(int value)
        {
            var response = await _categoryTypeService.GetByValueAsync(value);
            return CreateResult(response);
        }
    }
    
}
