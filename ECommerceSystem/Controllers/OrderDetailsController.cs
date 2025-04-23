using ECommerceSystem.Core.DTO;
using ECommerceSystem.Core.IServices;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly OrderDetailsCreateDTOValidator _orderDetailsCreateDtoValidator;
        private readonly OrderDetailUpdateDTOValidator _orderDetailUpdateDtoValidator;
        public OrderDetailsController(IOrderDetailsService orderDetailsService,OrderDetailsCreateDTOValidator orderDetailsCreateDtoValidator,OrderDetailUpdateDTOValidator orderDetailsUpdateDtoValidator)
        {
            _orderDetailsCreateDtoValidator = orderDetailsCreateDtoValidator;
            _orderDetailsService = orderDetailsService;
            _orderDetailUpdateDtoValidator = orderDetailsUpdateDtoValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderdetails=await _orderDetailsService.GetAllOrdersAsync();
            if (orderdetails==null) return NotFound();
            return Ok(orderdetails);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersDetailsById(int id)
        {
            var orderdetails=await _orderDetailsService.GetOrderDetailsByIdAsync(id);
            if (orderdetails==null) return NotFound();
            return Ok(orderdetails);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetails([FromBody] OrderDetailCreateDTO orderDetailCreateDTO)
        {
            var validationResult=await _orderDetailsCreateDtoValidator.ValidateAsync(orderDetailCreateDTO);
            if(!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdOrderdetail = await _orderDetailsService.AddOrderDetailsAsync(orderDetailCreateDTO);
            return CreatedAtAction(nameof(GetOrdersDetailsById), new { id = createdOrderdetail.Id }, createdOrderdetail);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetails(OrderDetailUpdateDTO orderDetailUpdateDTO,int id)
        {
            var validationResult = await _orderDetailUpdateDtoValidator.ValidateAsync(orderDetailUpdateDTO);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedOrderdetail=await _orderDetailsService.UpdateOrderDetailsAsync(orderDetailUpdateDTO,id);
            return Ok(updatedOrderdetail);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var deletedDetail=await _orderDetailsService.DeleteOrderDetailsAsync(id);
            if(!deletedDetail) return NotFound();
            return Ok(deletedDetail);
        }
    }
}
