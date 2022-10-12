using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Messages.Infrastructure.EFCore;
using Messages.Interfaces;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Messages.Logic.SectionsNS.Mappings;
using Messages.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

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
            services.AddHttpContextAccessor();

            services.AddErrorHandling(Env);


            services.AddDbContext<AppDbContext>(
               options => options
               .UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
               .UseLowerCaseNamingConvention()
               .UseLazyLoadingProxies()
          );

            services.AddScoped<IAppDbContext,AppDbContext>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddMediatR(typeof(CreateSectionCommand).GetTypeInfo().Assembly);

            services.AddAutoMapper(typeof(SectionsMappingProfile).GetTypeInfo().Assembly);

            services.AddControllers();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();
            //if (env.IsDevelopment( ))
            //{
            //    app.UseDeveloperExceptionPage( );
            //}

            app.UseSwagger();


            app.UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api для работы с Marketplace V1");
             });

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
           
        }
    }
}
