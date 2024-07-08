using CMTools_2_0.Services;
using LayoutLibrary.Models;
using CMTools_2_0.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<ILayoutConfig>(new LayoutConfig
{
    Sidebar = new SidebarConfig
    {
        Width = "150px",
        StartColor = "rgba(5, 80, 101, 1)",
        EndColor = "rgba(5, 80, 101, 1)",
        Visible = true,
        Fixed = true
    },
    Topbar = new TopbarConfig
    {
        Height = "0px",
        BackgroundColor = "lightyellow",
        Visible = false,
        Fixed = true
    },
    Footer = new FooterConfig
    {
        Height = "100px",
        BackgroundColor = "gray",
        Visible = false,
        Fixed = true
    }
});

builder.Services.AddSingleton<LayoutStyleConfig>();
//builder.Services.AddSingleton<ErrorHandlingService>();
builder.Services.AddScoped<ErrorHandlingService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddQuickGridEntityFrameworkAdapter();;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Global error handling
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        context.Response.Redirect($"/Error?message={Uri.EscapeDataString(ex.Message)}");
    }
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapRazorComponents<App>();

app.Run();
