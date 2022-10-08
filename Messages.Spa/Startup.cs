using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System;
using System.Collections.Generic;

namespace Messages.Spa
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProblemDetails( options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => !Env.IsDevelopment( );

                options.Map<ValidationApiException>(
                       delegate (ValidationApiException exception)
                       {
                           return new ValidationProblemDetails
                           ( exception.Content.Errors )
                           {

                               Title = exception.Content.Title,
                               Detail = exception.Content.Detail,
                               Instance = exception.Content.Instance,
                               Status = exception.Content.Status,
                               Type = exception.Content.Type
                           };
                       }
                );
            }
        );
            services.AddControllersWithViews( );
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles( configuration =>
             {
                 configuration.RootPath = "ClientApp/dist";
             } );

            services.AddSwaggerGen( );

            services.AddRefitClient<IMessagesServices>( ).ConfigureHttpClient( c => c.BaseAddress = new System.Uri( "http://localhost:5100" ) );
            //.AddHttpMessageHandler<mvc.services.AccessTokenHandler>( )
            //.AddHttpMessageHandler<ErrorMessageHandler>( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails( );
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseSwagger( );


            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint( "/swagger/v1/swagger.json", "My API V1" );
            } );

            app.UseStaticFiles( );

            if (!env.IsDevelopment( ))
            {
                app.UseSpaStaticFiles( );
            }

            app.UseRouting( );

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller}/{action=Index}/{id?}" );
             } );

            app.UseSpa( spa =>
             {
                 // To learn more about options for serving an Angular SPA from ASP.NET Core,
                 // see https://go.microsoft.com/fwlink/?linkid=864501

                 spa.Options.SourcePath = "ClientApp";

                 if (env.IsDevelopment( ))
                 {
                     //      spa.UseAngularCliServer( npmScript: "start" );
                 }
             } );
        }
    }
}
