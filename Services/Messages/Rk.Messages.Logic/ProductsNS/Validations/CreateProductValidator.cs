using System.Linq;
using FluentValidation;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;

namespace Rk.Messages.Logic.ProductsNS.Validations
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
                .WithMessage("Наименование товара не должно быть пустым")
                .MinimumLength(5)
                .WithMessage("Минимальная длина наименования товара не менее 5 символов");

           

            RuleFor(x => x.Request).Custom((request, context) =>
            {
                if (request.AttributeValues.Any())
                {
                   var attributeDoubles = request.AttributeValues.GroupBy(x => x.AttributeId)
                    .ToDictionary(group => group.Key, group => group.Count())
                    .Where(item => item.Value > 1);

                    if (attributeDoubles.Any())
                        context.AddFailure($"При создании товара дважды указан атрибут с Id ={attributeDoubles.First().Key}.");
                }
            });
        }
    }
}
