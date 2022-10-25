using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Rk.FileStore.Infrastructure.EFCore;
using Rk.FileStore.Interfaces.Interfaces;
using Rk.FileStore.Webapi;

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
builder.WebHost.UseTneSerilog();

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
