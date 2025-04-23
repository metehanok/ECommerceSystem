using ECommerceSystem.Service.DTO;
using FluentValidation;

namespace ECommerceSystem.Validations
{
    public class OrderCreateDTOValidator:AbstractValidator<OrderCreateDTO>
    {
        public OrderCreateDTOValidator()
        {
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Sipariş tarih alanı boş geçilemez");
            
            
        }
    }
}
