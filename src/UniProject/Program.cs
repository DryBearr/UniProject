using Services;
using Repository;
using Repository.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Contollers;
using DTOs;
using CustomFilters;
using CustomMiddlwares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ServiceLayerMapper), typeof(ControllerLayerMapper));


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new CustomExceptionFilter());
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();