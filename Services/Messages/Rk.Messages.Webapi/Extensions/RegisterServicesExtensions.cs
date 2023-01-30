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
using Confluent.Kafka;
using Rk.Messages.Infrastructure.Kafka;
using Rk.Messages.Logic.ProductsNS.Dto;
using Rk.Messages.Logic.ProductsNS.PipelineBehaviour;
using Rk.Messages.Logic.ProductsNS.Queries.GetProductQuery;
using Rk.Messages.Logic.ProductsNS.Validations;
using Rk.Messages.Logic.ServiceProductsNS.Commands.CreateServiceProduct;
using Rk.Messages.Logic.WorkProductsNS.Commands.CreateWorkProduct;
using Rk.Messages.Logic.WorkProductsNS.Validations;
using Rk.Messages.Logic.ServiceProductsNS.Validations;
using RK.Messages.Shared.Contracts;

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

            services.AddTransient<IValidator<CreateSectionCommand>, CreateSectionValidator>();

            services.AddTransient<IValidator<CreateProductCommand>, CreateProductValidator>();

            services.AddTransient<IValidator<CreateWorkProductCommand>, CreateWorkProductValidator>();

            services.AddTransient<IValidator<CreateServiceProductCommand>, CreateServiceProductValidator>();

            services.AddTransient<IValidator<CreateOrganizationRequest>, CreateOrganizationValidator>();

            services.AddMediatR(typeof(CreateSectionCommand).GetTypeInfo().Assembly);
            services.AddScoped(typeof(IPipelineBehavior<GetProductQuery, ProductResponse>), typeof(ProductViewStatisticBehavior));

            services.AddAutoMapper(typeof(ProductsMappingProfile).GetTypeInfo().Assembly);

            services.AddSingleton<KafkaClientHandle>();
            services.AddSingleton<KafkaObjectProducer<Null, ProductViewStatisticEvent>>();
        }
    }
}
