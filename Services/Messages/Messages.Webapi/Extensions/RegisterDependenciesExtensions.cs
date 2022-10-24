using FluentValidation;
using MediatR;
using Messages.Infrastructure.EFCore;
using Messages.Interfaces.Interfaces.DAL;
using Messages.Logic.ProductsNS.Commands.CreateProduct;
using Messages.Logic.ProductsNS.Mappings;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Messages.Logic.SectionsNS.Validations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Messages.Webapi.Extensions
{
    public static class RegisterDependenciesExtensions
    {
        /// <summary>
        /// Загруза внутренних зависимостей
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IValidator<CreateSectionCommand>, CreateSectionValidator>();
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>();

            services.AddMediatR(typeof(CreateSectionCommand).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(ProductsMappingProfile).GetTypeInfo().Assembly);
        }
    }
}
