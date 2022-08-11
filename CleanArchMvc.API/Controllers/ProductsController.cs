using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var categories = await _productService.GetProducts();
            if (categories is null)
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var category = await _productService.GetById(id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
        {
            if (productDto is null)
                return BadRequest("Invalid Data");

            await _productService.Add(productDto);

            return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id || productDto is null)
                return BadRequest();

            await _productService.Update(productDto);

            return Ok(productDto);
        }

        [HttpDelete]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product is null)
                return NotFound("Product not found");

            await _productService.Remove(id);
            return Ok(product);
        }
    }
}
