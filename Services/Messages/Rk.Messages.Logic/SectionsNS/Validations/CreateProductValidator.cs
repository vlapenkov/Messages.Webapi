using System.Linq;
using FluentValidation;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;

namespace Rk.Messages.Logic.SectionsNS.Validations
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Request)
                .NotNull()
                .WithMessage("Запрос не может быть пустым");

            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .WithMessage("Наименование продукта не должно быть пустым")
                .MinimumLength(5)
                .WithMessage("Минимальная длина продукта не менее 5 символов");

            //RuleFor(x => x.Request).Custom((request, context) =>
            //{
            //    if (request.AttributeValues == null || !request.AttributeValues.Any())
            //    {
            //        context.AddFailure("Атрибуты продукта должны быть указаны");
            //    }
            //});
        }
    }
}
