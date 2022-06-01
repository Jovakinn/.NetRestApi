using RESTApi.Models;
using RESTApi.Repo;
using RESTApi.Service;
using RESTApi.Service.implementations;
using RESTApi.Service.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddTransient<IRepairService, RepairService>();
builder.Services.AddTransient<IBaseRepository<Document>, BaseRepository<Document>>();
builder.Services.AddTransient<IBaseRepository<Car>, BaseRepository<Car>>();
builder.Services.AddTransient<IBaseRepository<Worker>, BaseRepository<Worker>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();