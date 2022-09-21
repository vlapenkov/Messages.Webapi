using MediatR;
using Messages.Domain;
using Messages.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)

        {
            services.AddDbContext<MessagesDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
             );

            services.AddDbContext<ITransientDbContext, MessagesDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
             ServiceLifetime.Transient
             );

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<MessagesRepository>();
            services.AddSingleton<MessageTypeFactory>();

            services.AddControllers(
                opts =>
                {
                    opts.ModelBinderProviders.Insert(0, new MessageTypeModelBinderProvider());

                }).AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                }); ;
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MessagesDbContext>();

                context.Database.Migrate();

            }
        }
    }
}
