using Compras.data.Db;
using Compras.data.Repositorios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ComprasContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme =
   CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
 .AddCookie()
 .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
 {
     options.ClientId = builder.Configuration["GoogleKeys:ClientId"]!;
     options.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"]!;
 }); 

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
