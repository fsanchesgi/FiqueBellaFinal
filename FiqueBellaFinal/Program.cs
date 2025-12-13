var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços
builder.Services.AddControllersWithViews();

// Registra seu repositório
builder.Services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // importante para wwwroot

app.UseRouting();
app.UseAuthorization();

// Configura rotas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
