using ABCRetailers.Data;
using ABCRetailers.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Session support for Cart
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// SQL Server DbContext for Authentication
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));

// Named HttpClient for Azure Functions - FIXED
builder.Services.AddHttpClient("Functions", client =>
{
    var baseUrl = builder.Configuration["AzureFunctions:BaseUrl"] ?? "http://localhost:7071/api/";
    client.BaseAddress = new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(100);
    client.DefaultRequestHeaders.Add("User-Agent", "ABCRetailers-MVC");
});

// Register your service that uses the named client
builder.Services.AddScoped<IFunctionsApi, FunctionsApiClient>();

// Allow larger multipart uploads
builder.Services.Configure<FormOptions>(o =>
{
    o.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB
});

builder.Services.AddLogging();

// Azure Storage Services
var storageConnection = builder.Configuration.GetConnectionString("AzureStorage");
if (!string.IsNullOrEmpty(storageConnection))
{
    builder.Services.AddAzureClients(clientBuilder =>
    {
        clientBuilder.AddBlobServiceClient(storageConnection);
        clientBuilder.AddQueueServiceClient(storageConnection);
        clientBuilder.AddTableServiceClient(storageConnection);
    });
}

var app = builder.Build();

// Culture
var culture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();