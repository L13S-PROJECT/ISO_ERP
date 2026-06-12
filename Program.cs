using ISO_ERP.Components;
using Microsoft.EntityFrameworkCore;
using Photino.Blazor;
using QuestPDF.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;
QuestPDF.Settings.EnableDebugging = true;
QuestPDF.Settings.UseEnvironmentFonts = true;

//QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

builder.Services.AddDbContextFactory<ISO_ERP.Data.AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    ));

builder.Services.AddScoped<ISO_ERP.Services.CategoryService>();
builder.Services.AddScoped<ISO_ERP.Services.ProductService>();
builder.Services.AddScoped<ISO_ERP.Services.DetailService>();
builder.Services.AddScoped<ISO_ERP.Services.ProductionService>();
builder.Services.AddScoped<ISO_ERP.Services.InspectorService>();
builder.Services.AddScoped<ISO_ERP.Services.ProductionPdfService>();
builder.Services.AddScoped<ISO_ERP.Services.ProductionExcelService>();
builder.Services.AddScoped<ISO_ERP.Services.AuthService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
