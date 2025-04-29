using ECommerceSystem.Core.DTO;
using ECommerceSystem.Core.IServices;
using ECommerceSystem.Service.DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public AuthController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CustomerLoginDTO customerLoginDTO)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var customer = await _customerService.AuthenticateCustomerAsync(customerLoginDTO);
        if(customerLoginDTO.Email=="test@example.com" && customerLoginDTO.Password == "1234")
        {
            return Ok(new { message = "Giriş başarılı", customer });
        }

        if (customer == null)
        {
            return NotFound();  
        }

        return Unauthorized(new { message = "Geçersiz e-posta veya şifre" });
    }
}

