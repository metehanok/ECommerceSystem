using ECommerceSystem.Service.DTO;
using FluentValidation;

namespace ECommerceSystem.Validations
{
    public class CustomerUpdateDTOValidator:AbstractValidator<CustomerUpdateDTO>
    {
        public CustomerUpdateDTOValidator()
        {
            RuleFor(x => x.EMail).NotEmpty().WithMessage("E-mail alanı boş geçilemez!")
               .EmailAddress().WithMessage("Geçerli bir e-mail adresi girin");

            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez")
                .MaximumLength(20).WithMessage("Maksimum uzunluk 20 karakter");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez")
                .MaximumLength(10).WithMessage("Maksimum uzunluk 10 karakter");

            RuleFor(x => x.Adress).NotEmpty().WithMessage("Adres alanı boş geçilemez")
                .MaximumLength(100).WithMessage("Adres için maksimum karakter uzunluğu 100");
        }
    }
}
