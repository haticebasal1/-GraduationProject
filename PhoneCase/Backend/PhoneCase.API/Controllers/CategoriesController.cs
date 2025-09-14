using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.API.Controllers.BaseController;
using PhoneCase.Business.Abstract;
using PhoneCase.Shared.Dtos.CategoryDtos;

namespace PhoneCase.API.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : CustomControllerBase
    {
        private readonly ICategoryService _categoryManager;

        public CategoriesController(ICategoryService categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto categoryCreateDto)
        {
            var response = await _categoryManager.AddAsync(categoryCreateDto);
            return CreateResult(response);
        }

        [HttpGet("count/all")]
        public async Task<IActionResult> CountAll()
        {
            var response = await _categoryManager.CountAsync(isDeleted: null);
            return CreateResult(response);
        }

        [HttpGet("count/deleted")]
        public async Task<IActionResult> CountDeleted()
        {
            var response = await _categoryManager.CountAsync(isDeleted: true);
            return CreateResult(response);
        }
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var response = await _categoryManager.CountAsync(isDeleted: false);
            return CreateResult(response);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryManager.GetAllAsync(isDeleted: false);
            return CreateResult(response);
        }
        [HttpGet("getall/deleted")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var response = await _categoryManager.GetAllAsync(isDeleted: true);
            return CreateResult(response);
        }
        [HttpGet("getall/all")]
        public async Task<IActionResult> GetAllAll()
        {
            var response = await _categoryManager.GetAllAsync(isDeleted: null);
            return CreateResult(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryManager.GetAsync(id);
            return CreateResult(response);
        }

        [HttpPut("delete/soft")]
        public async Task<IActionResult> SoftDelete([FromQuery] int id)
        {
            var response = await _categoryManager.SoftDeleteAsync(id);
            return CreateResult(response);
        }
        [HttpDelete("delete/hard")]
        public async Task<IActionResult> HardDelete([FromQuery] int id)
        {
            var response = await _categoryManager.HardDeleteAsync(id);
            return CreateResult(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            var response = await _categoryManager.UpdateAsync(categoryUpdateDto);
            return CreateResult(response);
        }
    }
}