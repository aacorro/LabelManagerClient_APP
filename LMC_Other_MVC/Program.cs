using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
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
builder.Services.AddScoped<LMC_InvData_GetInventoryDataRecords_Repo>();
builder.Services.AddScoped<LMC_LabelCounter_GetUploadLabelCounterRecords_Repo>();
builder.Services.AddScoped<LMC_Pallet_GetPalletRecords_Repo>();
builder.Services.AddScoped<LMC_PrintStringLog_GetPrintStringsByPallet_Repo>();
builder.Services.AddScoped<LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo>();
builder.Services.AddScoped<LMC_Pallet_GetNonCollected_Repo>();
builder.Services.AddScoped<LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo>();

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
