using Hellang.Middleware.ProblemDetails;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddErrorHandling(builder.Environment, Log.Logger);
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddControllers();

builder.WebHost.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                 .ReadFrom.Configuration(hostingContext.Configuration)
                 .Enrich.FromLogContext()
                 .Enrich.WithMachineName()
            );

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messages service V1");
});

app.UseMiddleware<LogUserNameMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<LogCorrelationIdMiddleware>();
app.UseProblemDetails();

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
