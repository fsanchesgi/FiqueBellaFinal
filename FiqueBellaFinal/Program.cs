using FiqueBellaFinal.Data; // mantido apenas uma vez
using FiqueBellaFinal.Areas.Admin.Services;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Diagnostics; // se usar Activity

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Serviços customizados
builder.Services.AddScoped<RelatorioServices>();
builder.Services.AddScoped<GraficoServices>();

// Controllers com views e runtime compilation
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// ReflectionIT.Mvc.Paging atualizado
builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap5";
});

var app = builder.Build();

// --- TESTE DE CONEXÃO COM O BANCO ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        if (!db.Database.CanConnect())
        {
            Console.WriteLine("Não foi possível conectar ao banco de dados.");
        }
        else
        {
            Console.WriteLine("Conexão com o banco de dados OK!");
        }

        // Aplica migrations automaticamente (se houver)
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro de conexão ou migração: " + ex.Message);
        throw; // mantém a exceção para aparecer no log do Railway
    }
}
// --- FIM DO TESTE ---

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

ap
