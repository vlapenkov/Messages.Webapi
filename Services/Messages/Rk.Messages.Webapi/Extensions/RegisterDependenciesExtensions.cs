using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Rk.Messages.Webapi.Extensions
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
