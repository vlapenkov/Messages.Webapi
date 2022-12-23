using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rk.Messages.Common.Extensions;
using Rk.Messages.Common.Middlewares;
using Rk.Messages.Infrastructure.EFCore;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Webapi.Extensions;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddErrorHandling(builder.Environment);
builder.Services.AddHttpClients(builder.Configuration);

builder.Services.AddDbContext<IAppDbContext, AppDbContext>(
    options => options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLowerCaseNamingConvention()
        .UseLazyLoadingProxies()
);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddDependencies();

builder.Services.AddControllers();
builder.Services.AddSwaggerGeneration();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
);

builder.Services.AddHealthChecks();


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseRouting();

app.UseReverseProxy(builder.Configuration);

app.UseAuthentication();

app.UseMiddleware<LogUserNameMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<LogCorrelationIdMiddleware>();
app.UseProblemDetails();

app.UseAuthorization();
app.UseAuthorization();

app.MapHealthChecks("/hc", new HealthCheckOptions
{
    ResponseWriter = HealthCheckUiExtensions.WriteResponse
});
app.UseSwaggerUi(builder.Configuration, "Api Marketplace V1");

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();