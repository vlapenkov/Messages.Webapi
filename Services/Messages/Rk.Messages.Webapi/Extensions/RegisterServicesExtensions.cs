using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Rk.Messages.Infrastructure.Services;
using Rk.Messages.Interfaces.Services;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;
using Rk.Messages.Logic.ProductsNS.Mappings;
using Rk.Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Rk.Messages.Logic.SectionsNS.Validations;
using Rk.Messages.Infrastructure.EFCore;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using MediatR;
using System.Reflection;

namespace Rk.Messages.Webapi.Extensions
{
    public static class RegisterServicesExtensions
    {

        /// <summary>
        /// Загруза внутренних зависимостей
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependencies(this IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IValidator<CreateSectionCommand>, CreateSectionValidator>();

            services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>();

            services.AddScoped<IValidator<CreateOrganizationRequest>, CreateOrganizationValidator>();

            services.AddMediatR(typeof(CreateSectionCommand).GetTypeInfo().Assembly);

            services.AddAutoMapper(typeof(ProductsMappingProfile).GetTypeInfo().Assembly);
        }
    }
}
