using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Rk.Messages.Common.Extensions;
using Rk.Messages.Common.Middlewares;
using Rk.Messages.Spa;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddErrorHandling(builder.Environment, Log.Logger);
builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddControllers();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
);
builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAppAuthorization();
builder.Services.AddSwaggerGeneration();

var app = builder.Build();
app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin();
    policyBuilder.AllowAnyMethod();
    policyBuilder.AllowAnyHeader();
});

app.UseStaticFiles();
app.UseRouting();

app.UseReverseProxy(builder.Configuration);

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LogUserNameMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<LogCorrelationIdMiddleware>();
app.UseProblemDetails();

app.MapHealthChecks("/hc", new HealthCheckOptions
{
    ResponseWriter = HealthCheckUiExtensions.WriteResponse
});

app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");
app.UseSwaggerUI(builder.Configuration, "Api gateway");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();