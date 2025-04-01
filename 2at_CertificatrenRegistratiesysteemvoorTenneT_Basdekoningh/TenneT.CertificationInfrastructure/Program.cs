using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using TenneT.CertificationInfrastructure;
using TenneT.CertificationInfrastructure.Logging;
using TenneT.CertificationInfrastructure.Repositories;
using TenneT.CertificationInfrastructure.Storage;
using TenneT.CertificationDomain.Interfaces;
using _2at_CertificatrenRegistratiesysteemvoorTenneT_Basdekoningh.TenneT.CertificationInfrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Azure Blob client
builder.Services.AddSingleton(_ =>
{
    var blobConnectionString = builder.Configuration.GetConnectionString("AzureBlobStorage");
    return new BlobServiceClient(blobConnectionString);
});

// EF Core
builder.Services.AddDbContext<CertificationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency injection
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<ICertificateReviewService, CertificateReviewService>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<IBlobStorageService, AzureBlobStorageService>();
builder.Services.AddScoped<IAuditLogger, AuditLogger>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
