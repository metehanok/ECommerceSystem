using ECommerceSystem.Service.DTO;
using FluentValidation;

namespace ECommerceSystem.Validations
{
    public class CategoryCreateDTOValidator:AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez");
        }
    }
}
