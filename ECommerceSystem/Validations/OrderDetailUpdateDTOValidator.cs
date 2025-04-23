using ECommerceSystem.Core.DTO;
using FluentValidation;

namespace ECommerceSystem.Validations
{
    public class OrderDetailUpdateDTOValidator:AbstractValidator<OrderDetailUpdateDTO>
    {
        public OrderDetailUpdateDTOValidator()
        {
         
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Adet alanı boş geçilemez");
        }
    }
}
