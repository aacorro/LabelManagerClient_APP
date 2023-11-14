using LMC_Other_InventoryData;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<LMC_InventoryData_Repo>();
builder.Services.AddScoped<LMC_InvData_RecordDetail_Repo>();

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
    pattern: "{controller=LMC_InventoryData}/{action=Index}/{id?}");

app.Run();
