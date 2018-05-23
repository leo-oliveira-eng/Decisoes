using System;
using Core.IRepository;
using Core.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services;

namespace DependencyInjection
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services, string connectionString, string logFile) {
            services.AddDbContext<DecisoesContext>(options =>
                options.UseMySql(connectionString));
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            services.AddTransient<IProjetoService, ProjetoService>();
            services.AddTransient<ILogServices>(s => new LogService(logFile));
            services.AddScoped<IAlternativaRepository, AlternativaRepository>();
            services.AddTransient<IAlternativaService, AlternativasService>();
            services.AddScoped<ICriterioRepository, CriterioRepository>();
            services.AddTransient<ICriterioService, CriteriosService>();
            services.AddScoped<IMatrizDeDecisaoRepository, MatrizDeDecisaoRepository>();
            services.AddTransient<IMatrizDeDecisaoService, MatrizDeDecisaoService>();
        }
    }
}
