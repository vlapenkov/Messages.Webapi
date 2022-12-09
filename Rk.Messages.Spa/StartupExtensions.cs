using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Refit;
using Rk.Messages.Common.DelegatingHandlers;
using Rk.Messages.Spa.Infrastructure.Services;
using System.Reflection;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Rk.Messages.Spa
{
    public static class StartupExtensions
    {
        /// <summary>
        /// Обработка ошибок рефит на фронте
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        public static void AddErrorHandling(this IServiceCollection services, IHostEnvironment env, global::Serilog.ILogger logger)
        {

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => !env.IsDevelopment();

                options.OnBeforeWriteDetails = (ctx, details) =>
                {
                    // Set the CorrelationId here                   
                    details.Extensions["correlationId"] = ctx.Items["X-Correlation-ID"];
                    //logger.Error(details.Detail);
                    //logger.Error(details.Extensions.FirstOrDefault().Value.ToString());
                };

                options.Map<ValidationApiException>(
                                   delegate (ValidationApiException exception)
                                   {
                                       return new ValidationProblemDetails(exception.Content.Errors)
                                       {

                                           Title = exception.Content.Title,
                                           Detail = exception.Content.Detail,
                                           Instance = exception.Content.Instance,
                                           Status = exception.Content.Status,
                                           Type = exception.Content.Type,

                                       };
                                   }
                            );
            });
        }

        /// <summary>
        /// Регистрация обработчиков и клиентов Http
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<AuthHeaderPropagationHandler>();

            services.AddTransient<CorrelationIdDelegatingHandler>();
           

            Uri messagesUri = new Uri(config["Services:Messages:BaseUrl"]);

            Uri fileStoreUri = new Uri(config["Services:FileStore:BaseUrl"]);

            services.AddRefitClient<ISectionsServices>(messagesUri)

                    .AddRefitClient<IProductsService>(messagesUri)

                    .AddRefitClient<IShoppingCartService>(messagesUri)

                    .AddRefitClient<IOrdersService>(messagesUri)

                    .AddRefitClient<IOrganizationsService>(messagesUri)

                    .AddRefitClient<INewsService>(messagesUri)

                    .AddRefitClient<IRegionsService>(messagesUri)

                    .AddRefitClient<IWorkProductsService>(messagesUri)

                    .AddRefitClient<IFileStoreService>(fileStoreUri)

                    .AddRefitClient<IProductsPrepareService>(fileStoreUri);

            return services;
        }

        /// <summary>
        /// Обертка для регистрации Refit клиента
        /// </summary>
        /// <typeparam name="IService">интерфейс сервиса</typeparam>
        /// <param name="services">коллекция сервисов для DI</param>
        /// <param name="uri">baseuri для сервиса </param>
        /// <returns></returns>
        private static IServiceCollection AddRefitClient<IService>(this IServiceCollection services, Uri uri) {

            services.AddRefitClient(typeof(IService))
              .ConfigureHttpClient(c => c.BaseAddress = uri)
              .AddHttpMessageHandler<AuthHeaderPropagationHandler>()
               .AddHttpMessageHandler<CorrelationIdDelegatingHandler>();
            return services;
        }

        /// <summary>
        /// Добавление генерации swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGeneration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Аутентификация",
                    Description = "Вводите **_только_** JWT Bearer токен.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.SupportNonNullableReferenceTypes();
                //  c.UseDateOnlyTimeOnlyStringConverters();
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Messages SPA Api", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            return services;
        }

        /// <summary>
        /// добавление генрации swagger и включение SwaggerUI
        /// </summary>
        /// <param name="app"></param>
        /// <param name="config"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, WebApplicationBuilder builder, string title)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                {
                    var schema = httpRequest.Scheme;
                    var host = httpRequest.Headers["Host"];
                    var subDir = builder.Configuration["SUBDIR"] ?? "";
                    var serverUrl = $"{schema}://{host}{subDir}";
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = serverUrl } };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", title);
            });
            return app;
        }


        /// <summary>
        /// Добавляем авторизацию
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
            return services;
        }

    }
}
