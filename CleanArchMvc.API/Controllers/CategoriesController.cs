using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategories();
            if(categories is null) 
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest("Invalid Data");

            await _categoryService.Add(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", new {id = categoryDTO.Id}, categoryDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id || categoryDTO is null)
                return BadRequest();

            await _categoryService.Update(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category is null)
                return NotFound("Category not found");

            await _categoryService.Remove(id);
            return Ok(category);
        }
    }
}
