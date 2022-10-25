using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rk.Messages.Infrastructure.EFCore;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Webapi;
using Rk.Messages.Webapi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddErrorHandling(builder.Environment);
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(
    options => options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLowerCaseNamingConvention()
        .UseLazyLoadingProxies()
);

builder.Services.AddDependencies();

builder.Services.AddControllers();
builder.Services.AddSwaggerGeneration();
builder.WebHost.UseTneSerilog();

var app = builder.Build();

app.UseRouting();
app.AddReverseProxy(builder.Configuration);
app.UseProblemDetails();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api для работы с Marketplace V1");
});

app.UseMiddleware<LogUserNameMiddleware>();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();