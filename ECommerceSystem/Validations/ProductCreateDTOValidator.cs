using ECommerceSystem.Service.DTO;
using FluentValidation;

namespace ECommerceSystem.Validations
{
    public class ProductCreateDTOValidator:AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateDTOValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Ürün adı boş geçilemez");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stok alanı boş geçilemez");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat alanı boş geçilemez");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100).WithMessage("Açıklama alanı boş geçilemez ve uzunluğu maksimum 100 karakter olabilir");

        }
    }
}
