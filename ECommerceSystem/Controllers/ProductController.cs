using ECommerceSystem.Core.IServices;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ProductCreateDTOValidator _productCreateDtoValidator;
        private readonly ProductUpdateDTOValidator _productUpdateDtoValidator;

        public ProductController(IProductService productService, ProductCreateDTOValidator productCreateDtoValidator, ProductUpdateDTOValidator productUpdateDtoValidator)
        {
            _productService = productService;
            _productCreateDtoValidator = productCreateDtoValidator;
            _productUpdateDtoValidator = productUpdateDtoValidator;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products == null) return NotFound();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            var products = await _productService.GetProductsByIdAsync(id);
            if (products == null) return NotFound();
            return Ok(products);
        }
        [HttpPost]

        public async Task<IActionResult> CreateProducts([FromBody] ProductCreateDTO productCreateDTO)
        {
            var validationResult = await _productCreateDtoValidator.ValidateAsync(productCreateDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var createdProducts=await _productService.AddProductsAsync(productCreateDTO);
            return CreatedAtAction(nameof(GetProductsById), new {id=createdProducts.Id},createdProducts);


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducts(int id,ProductUpdateDTO productUpdateDTO)
        {
            var validationResult = await _productUpdateDtoValidator.ValidateAsync(productUpdateDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var updatedProduct=await _productService.UpdateProductsAsync(id,productUpdateDTO);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult>DeleteProducts(int id)
        {
            var deletedProduct=await _productService.DeleteProductsAsync(id);
            if(!deletedProduct) return NotFound();
            return Ok(deletedProduct);
        }
    } 
}
