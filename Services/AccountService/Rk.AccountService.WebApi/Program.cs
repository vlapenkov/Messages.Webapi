using System.Text.Json;
using System.Text.Json.Serialization;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Rk.AccountService.WebApi.Extensions;
using Rk.Messages.Common.Extensions;
using Rk.Messages.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseRkSerilog();
builder.Services.AddHttpContextAccessor();
builder.Services.AddErrorHandling(builder.Environment);
builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddDependencies();
builder.Services.AddSwaggerGeneration();
builder.Services.AddHealthChecks();
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)));


var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LogUserNameMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<LogCorrelationIdMiddleware>();
app.UseProblemDetails();
app.UseReverseProxy(builder.Configuration);
app.MapHealthChecks("/hc", new HealthCheckOptions
{
    ResponseWriter = HealthCheckUiExtensions.WriteResponse
});
app.UseSwaggerUI(builder.Configuration, "Api Marketplace V1");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();