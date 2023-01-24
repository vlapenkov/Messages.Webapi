using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Rk.Messages.Common.Extensions;
using Rk.Messages.Common.Middlewares;
using RK.Statistic.Infrastructure.ClickHouse;
using RK.Statistic.Interfaces;
using RK.Statistic.Logic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
);
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<IClickHouseConnectionFactory, ClickHouseConnectionFactory>();
builder.Services.AddHostedService<ProductReadEventConsumer>();

var app = builder.Build();
app.UseRouting();
app.UseReverseProxy(builder.Configuration);

app.UseAuthentication();

app.UseMiddleware<LogUserNameMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<LogCorrelationIdMiddleware>();

app.UseAuthorization();

app.MapHealthChecks("/hc", new HealthCheckOptions
{
    ResponseWriter = HealthCheckUiExtensions.WriteResponse
});

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();
