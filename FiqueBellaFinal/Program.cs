using FiqueBellaFinal.Data;
using FiqueBellaFinal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReflectionIT.Mvc.Paging;


var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Configuração de DB
// -------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------------
// Configuração de Services
// -------------------------
builder.Services.AddScoped<RelatorioServices>();
builder.Services.AddScoped<GraficoServices>();

// -------------------------
// Configuração de IOptions
// -------------------------
builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));

// -------------------------
// Autenticação / Authorization
// -------------------------
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    options.Filters.Add(new AuthorizeFilter(policy)); // Aplica Authorize globalmente se quiser
});

// Se for usar cookies (opcional, mas padrão para MVC)
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// -------------------------
// Build App
// -------------------------
var app = builder.Build();

// -------------------------
// Pipeline HTTP
// -------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // obrigatória se usar [Authorize]
app.UseAuthorization();

// -------------------------
// Rotas MVC com Areas
// -------------------------
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// -------------------------
// Classe de exemplo para IOptions
// -------------------------
public class MySettings
{
    public string ExampleSetting { get; set; }
}
