using ECommerceSystem.Service.DTO;
using FluentValidation;

namespace ECommerceSystem.Validations
{
    public class OrderDetailsCreateDTOValidator:AbstractValidator<OrderDetailCreateDTO>
    {
        public OrderDetailsCreateDTOValidator()
        {
            //RuleFor(x=>x.Price).NotEmpty().WithMessage("Fiyat alanı boş geçilemez");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Adet alanı boş geçilemez");
        }
    }
}
