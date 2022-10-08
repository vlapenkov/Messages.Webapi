using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Messages.Common;
using Messages.Domain;
using Messages.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Messages.Webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;

        }

        public IHostEnvironment Env;
        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProblemDetails(
                options =>
                {
                    options.IncludeExceptionDetails = (ctx, ex) => !Env.IsDevelopment( );

                    options.Map<ValidationException>(

                           delegate (ValidationException exception)
                           {

                               var result = new ValidationProblemDetails( exception.Errors
                .GroupBy( x => x.PropertyName )
                .ToDictionary(
                    x => x.Key,
                    x => x.Select( x => x.ErrorMessage )
                    .ToArray( ) ) )
                               {
                                   Title = "Ошибка",
                                   Type = nameof( ValidationException ),
                                   Status = StatusCodes.Status400BadRequest
                               };

                               return result;
                           }
                    );

                    options.Map<TneErrorException>( exception => new ProblemDetails
                    {
                        Type = "TneErrorException",
                        Title = "Ошибка",
                        Detail = exception.Message,
                        Status = StatusCodes.Status500InternalServerError
                    } );

                    options.Map<Exception>( exception => new ProblemDetails
                    {
                        Type = "TneErrorException",
                        Title = "Ошибка",
                        Detail = exception.Message,
                        Status = StatusCodes.Status500InternalServerError
                    } );


                }

                );



            services.AddDbContext<MessagesDbContext>( options =>
              options.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ) )
             );

            services.AddDbContext<ITransientDbContext, MessagesDbContext>( options =>
              options.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ) ),
             ServiceLifetime.Transient
             );

            services.AddMediatR( Assembly.GetExecutingAssembly( ) );
            services.AddScoped<MessagesRepository>( );
            services.AddSingleton<MessageTypeFactory>( );

            services.AddControllers(
                opts =>
                {
                    opts.ModelBinderProviders.Insert( 0, new MessageTypeModelBinderProvider( ) );

                } ).AddJsonOptions( options =>
                  {
                      options.JsonSerializerOptions.WriteIndented = true;
                  } ); ;
            services.AddSwaggerGen( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails( );
            //if (env.IsDevelopment( ))
            //{
            //    app.UseDeveloperExceptionPage( );
            //}

            app.UseSwagger( );


            app.UseSwaggerUI( c =>
             {
                 c.SwaggerEndpoint( "/swagger/v1/swagger.json", "My API V1" );
             } );

            app.UseRouting( );

            app.UseAuthorization( );


            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers( );
             } );

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>( ).CreateScope( ))
            {
                var context = serviceScope.ServiceProvider.GetService<MessagesDbContext>( );

                context.Database.Migrate( );

            }
        }
    }
}
