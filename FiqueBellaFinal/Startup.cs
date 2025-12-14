using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using FiqueBellaFinal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using System;
using System.Threading;

namespace FiqueBellaFinal
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // 🔹 DbContext SQL Server com timeout aumentado
            var connectionString = Configuration.GetConnectionString("DefaultConnection") 
                                   ?? "Server=tramway.proxy.rlwy.net,32176;Database=FiqueBellaDB;User Id=sa;Password=FiqueBella@2025;TrustServerCertificate=True;Encrypt=True;Connect Timeout=120;";

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // 🔹
