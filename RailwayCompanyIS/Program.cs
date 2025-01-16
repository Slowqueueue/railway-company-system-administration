using RailwayCompanyIS.Data.ServiceSpecification;
using RailwayCompanyIS.Data;
using RailwayCompanyIS.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddScoped<ICity, CityService>();
builder.Services.AddScoped<ICarrier, CarrierService>();
builder.Services.AddScoped<IContact, ContactService>();
builder.Services.AddScoped<IVehicle, VehicleService>();
builder.Services.AddScoped<IDeparture, DepartureService>();
builder.Services.AddScoped<IPaymentCategory, PaymentCategoryService>();
builder.Services.AddScoped<IDistance, DistanceService>();
builder.Services.AddScoped<IPriceManager, PriceService>();
builder.Services.AddScoped<IDateOfArrival, DateOfArrivalService>();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
