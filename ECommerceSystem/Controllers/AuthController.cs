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
        Console.WriteLine($"Login attempt: Email={customerLoginDTO?.Email}, Password={customerLoginDTO?.Password}");

        if (customerLoginDTO == null)
        {
            return BadRequest("İstek verisi eksik.");
        }

        if (string.IsNullOrEmpty(customerLoginDTO.Email) || string.IsNullOrEmpty(customerLoginDTO.Password))
        {
            return BadRequest("E-posta ve şifre gerekli.");
        }

        // Sadece test için:
        if (customerLoginDTO.Email == "test@example.com" && customerLoginDTO.Password == "1234")
        {
            return Ok(new { message = "Giriş başarılı", customer = customerLoginDTO });
        }

        var customer = await _customerService.AuthenticateCustomerAsync(customerLoginDTO);

        if (customer == null)
        {
            return Unauthorized(new { message = "Geçersiz e-posta veya şifre" });
        }

        return Ok(new { message = "Giriş başarılı", customer });
    }
}

