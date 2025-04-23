using ECommerceSystem.Core.IServices;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly CustomerCreateDTOValidator _customerCreateDtoValidator;
        private readonly CustomerUpdateDTOValidator _customerUpdateDtoValidator;

        public CustomerController(ICustomerService customerService,CustomerCreateDTOValidator customerCreateDtoValidator,CustomerUpdateDTOValidator customerUpdateDtoValidator)
        {
            _customerService = customerService;
            _customerCreateDtoValidator = customerCreateDtoValidator;
            _customerUpdateDtoValidator = customerUpdateDtoValidator;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers=await _customerService.GetAllCustomersAsync();
            if(customers==null) return NotFound();
            return Ok(customers);

        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetCustomersById(int id)
        {
            var customers=await _customerService.GetCustomersByIdAsync(id);
            if(customers==null) return NotFound();
            return Ok(customers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomers([FromBody] CustomerCreateDTO customerCreateDTO)
        {
            var validationResult= await _customerCreateDtoValidator.ValidateAsync(customerCreateDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var createdCustomer= await _customerService.AddCustomersAsync(customerCreateDTO);
            return CreatedAtAction(nameof(GetCustomersById), new { id = createdCustomer.Id }, createdCustomer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id,CustomerUpdateDTO customerUpdateDTO)
        {
            var validationResult=await _customerUpdateDtoValidator.ValidateAsync(customerUpdateDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var updatedCustomer = await _customerService.UpdateCustomersAsync( customerUpdateDTO,id);
            if(!updatedCustomer) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deletedCustomer=await _customerService.DeleteCustomerAsync(id);
            if(!deletedCustomer) return NotFound();
            return Ok(deletedCustomer);
        }
    }
}
