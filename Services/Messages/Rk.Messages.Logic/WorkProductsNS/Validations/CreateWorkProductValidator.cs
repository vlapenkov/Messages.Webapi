using FluentValidation;
using Rk.Messages.Logic.WorkProductsNS.Commands.CreateWorkProduct;

namespace Rk.Messages.Logic.WorkProductsNS.Validations
{
    public class CreateWorkProductValidator : AbstractValidator<CreateWorkProductCommand>
    {
        public CreateWorkProductValidator()
        {
            RuleFor(x => x.Request)
                 .NotNull()
                 .WithMessage("Запрос не может быть пустым");

            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .WithMessage("Наименование работы не должно быть пустым")
                .MinimumLength(5)
                .WithMessage("Минимальная длина наименования работы не менее 5 символов");
        }
    }
}
