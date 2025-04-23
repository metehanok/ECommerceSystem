using ECommerceSystem.Core.IServices;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly OrderCreateDTOValidator _orderCreateDtoValidator;
        private readonly OrderUpdateDTOValidator _orderUpdateDtoValidator;

        public OrderController(IOrderService orderService,OrderCreateDTOValidator orderCreateDtoValidator,OrderUpdateDTOValidator orderUpdateDtoValidator)
        {
            _orderService = orderService;
            _orderCreateDtoValidator = orderCreateDtoValidator;
            _orderUpdateDtoValidator = orderUpdateDtoValidator;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders=await _orderService.GetAllOrdersAsync();
            if(orders == null) return NotFound();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(int id)
        {
            var orders=await _orderService.GetOrdersByIdAsync(id);
            if(orders == null) return NotFound();
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrders([FromBody] OrderCreateDTO orderCreateDTO)
        {
            var validationResult=await _orderCreateDtoValidator.ValidateAsync(orderCreateDTO);
            if(!validationResult.IsValid) 
                return BadRequest(validationResult.Errors);
            var createdOrder=await _orderService.AddOrdersAsync(orderCreateDTO);
            return CreatedAtAction(nameof(GetOrdersById), new {id=createdOrder.Id},createdOrder);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrders(OrderUpdateDTO orderUpdateDTO,int id)
        {
            var validationResult = await _orderUpdateDtoValidator.ValidateAsync(orderUpdateDTO);
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var updatedOrder=await _orderService.UpdateOrdersAsync(orderUpdateDTO,id);
            return NoContent();
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOrders(int id)
        {
            var deletedOrder=await _orderService.DeleteOrdersAsync(id);
            if(!deletedOrder) return NotFound();
            return Ok(deletedOrder);

        }
    }
}
