using ECommerceSystem.Service.DTO;
using FluentValidation;

namespace ECommerceSystem.Validations
{
    public class OrderUpdateDTOValidator:AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateDTOValidator()
        {
            
            RuleFor(x => x.TotalAmount).NotEmpty();
        }
    }
}
