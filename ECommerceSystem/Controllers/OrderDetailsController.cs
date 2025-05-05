using ECommerceSystem.Core.DTO;
using ECommerceSystem.Core.IServices;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Service.Services;
using ECommerceSystem.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;  // LINQ metodlarını kullanabilmek için


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
            if (orderdetails==null) return NotFound($"ID'si {id} olan sipariş detayı bulunamadı.");
            return Ok(orderdetails);
        }
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
        {
            var details=await _orderDetailsService.GetOrderDetailsByOrderId(orderId);
            if (details == null || !details.Any())
                return NotFound($"OrderId'si {orderId} olan detay bulunamadı.");

            return Ok(details);

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
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrderDetailsByCustomerId(int customerId)
        {
            var orderDetails = await _orderDetailsService.GetOrderDetailsByCustomerIdAsync(customerId); ;
            if (orderDetails == null)
            {
                return NotFound();
            }

            // OrderDetailReadDTO olarak dönüyoruz
            var orderDetailsDto = orderDetails.Select(o => new OrderDetailReadDTO
            {
                Id = o.Id,
                ProductName = o.ProductName,
                Quantity = o.Quantity,
                Price = o.Price,
                TotalPrice = o.TotalPrice, // Hesaplanan toplam fiyat
                ProductId = o.ProductId,
                OrderId = o.OrderId
            }).ToList();

            return Ok(orderDetailsDto);
        }

    }
}
