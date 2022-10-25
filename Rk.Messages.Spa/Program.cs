using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
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
//builder.Services.AddHealthChecksUI()
//     .AddInMemoryStorage();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseMiddleware<LogUserNameMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<LogCorrelationIdMiddleware>();
app.UseProblemDetails();

app.MapHealthChecks("/hc");
//app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messages service V1");
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");



app.MapFallbackToFile("index.html");

app.Run();
