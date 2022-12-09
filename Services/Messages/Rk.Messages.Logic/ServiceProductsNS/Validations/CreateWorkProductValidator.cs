using FluentValidation;
using Rk.Messages.Logic.ServiceProductsNS.Commands.CreateServiceProduct;

namespace Rk.Messages.Logic.ServiceProductsNS.Validations
{
    public class CreateServiceProductValidator : AbstractValidator<CreateServiceProductCommand>
    {
        public CreateServiceProductValidator()
        {
            RuleFor(x => x.Request)
                 .NotNull()
                 .WithMessage("Запрос не может быть пустым");

            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .WithMessage("Наименование услуги не должно быть пустым")
                .MinimumLength(5)
                .WithMessage("Минимальная длина наименования услуги не менее 5 символов");
        }
    }
}
