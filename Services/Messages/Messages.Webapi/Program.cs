using Hellang.Middleware.ProblemDetails;
using Messages.Infrastructure.EFCore;
using Messages.Interfaces.Interfaces.DAL;
using Messages.Webapi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Messages.Common.Extensions;
using Serilog;
using Messages.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddErrorHandling(builder.Environment, Log.Logger);
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(
    options => options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLowerCaseNamingConvention()
        .UseLazyLoadingProxies()
);

builder.Services.AddDependencies();

builder.Services.AddControllers();
builder.Services.AddSwaggerGeneration();
builder.WebHost.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                 .ReadFrom.Configuration(hostingContext.Configuration)
                 .Enrich.FromLogContext()
                 .Enrich.WithMachineName()
            );

var app = builder.Build();

app.UseRouting();
app.AddReverseProxy(builder.Configuration);
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api для работы с Marketplace V1");
});

app.UseMiddleware<LogUserNameMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<LogCorrelationIdMiddleware>();
app.UseProblemDetails();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();