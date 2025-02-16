using DDD_Project.API.Hubs;
using DDD_Project.API.Profiles;
using DDD_Project.API.Services;
using DDD_Project.Domain.Interfaces.Repositories;
using DDD_Project.Domain.Interfaces.Services;
using DDD_Project.Infrastructure.Data;
using DDD_Project.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();

builder.Services.AddScoped<IPhoneService, PhoneService>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddAutoMapper(typeof(CreatePhoneToPhoneMappingProfile), typeof(CreateCountryToCountryMappingProfile), typeof(EditPhoneToPhoneMappingProfile), typeof(EditCountryToCountryMappingProfile));

builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.MapHub<DDDHub>("/DDDHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Privacy}/{id?}");

app.Run();
