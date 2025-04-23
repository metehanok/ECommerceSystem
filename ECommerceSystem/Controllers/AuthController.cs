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
        var customer = await _customerService.AuthenticateCustomerAsync(customerLoginDTO);

        if (customer == null)
        {
            return Unauthorized(new { message = "Geçersiz e-posta veya şifre" });
        }
       
        return Ok(new { message = "Giriş başarılı", customer });
    }
}

