using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Rk.AccountService.WebApi.Extensions
{
    /// <summary>
    /// Методы расширений для подключения Swagger
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Включение генерации сваггер документа
        /// </summary>
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

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Account Api", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            return services;
        }

        /// <summary>
        /// Добавление Swagger UI
        /// </summary>
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, IConfiguration config, string title)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                {
                    var serverUrl = $"{httpRequest.Scheme}://{httpRequest.Headers["Host"]}{config["SUBDIR"]}";
                    swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = serverUrl } };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", title);
            });
            return app;
        }

    }
}
