using Messages.Webapi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Messages.Infrastructure.EFCore;
using Messages.Interfaces;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Messages.Logic.SectionsNS.Mappings;
using Messages.Logic.SectionsNS.Validations;
using Messages.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddErrorHandling(builder.Environment);
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(
    options => options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLowerCaseNamingConvention()
        .UseLazyLoadingProxies()
);

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IValidator<CreateSectionCommand>, CreateSectionValidator>();
builder.Services.AddMediatR(typeof(CreateSectionCommand).GetTypeInfo().Assembly);
builder.Services.AddAutoMapper(typeof(SectionsMappingProfile).GetTypeInfo().Assembly);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.WebHost.UseTneSerilog();

var app = builder.Build();
app.UseProblemDetails();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api для работы с Marketplace V1");
});
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();