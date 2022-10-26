using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Rk.FileStore.Infrastructure.EFCore;
using Rk.FileStore.Interfaces.Interfaces;
using Rk.FileStore.Webapi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddErrorHandling(builder.Environment);
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(
    options => options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLowerCaseNamingConvention()
        .UseLazyLoadingProxies()
);

builder.Services.RegisterDependencies();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
);

var app = builder.Build();
app.UseProblemDetails();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api для работы с FileStore V1");
});
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
