using System.Globalization;
using System.IO;
using System.Reflection;
using Dapper.Core.Contexts.Interfaces;
using Dapper.PostgreSql.Contexts;
using Gomi.Dados;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gomi.Infraestrutura.Interfaces;
using Gomi.Dados.Repositories;
using Gomi.Infraestrutura.Models;
using Gomi.Infraestrutura.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;

namespace Gomi.WebApp
{
    public class Startup
    {
        private bool _iniciouBancoDados;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IDapperContext>(p =>
            {
                var connection = PostgresConnection
                    .Init()
                    .SetarHost("localhost")
                    .SetarUsuario("postgres")
                    .SetarSenha("postgres")
                    .SetarNomeBaseDeDados("bdgomi")
                    .SetarPorta(5433)
                    .Criar();

                if (!_iniciouBancoDados)
                {
                    var dbContext = new PostgresDbContext(connection);
                    dbContext.CreateDatabaseAsync(Assembly.GetAssembly(typeof(Versao))).Wait();
                    dbContext.ExecutarScriptsDaVersao();
                    _iniciouBancoDados = true;
                }

                return new PostgresDbContext(connection);
            });

            services.AddTransient<ICaixaRepository, CaixaRepository>();
            services.AddTransient<ICentroCustoRepository, CentroCustoRepository>();
            services.AddTransient<IContaRepository, ContaRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<IPagarRepository, PagarRepository>();
            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<IPlanoContaRepository, PlanoContaRepository>();
            services.AddTransient<IReceberRepository, ReceberRepository>();
            services.AddTransient<ITransferenciaRepository, TransferenciaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            services.AddSingleton<ICaixaService, CaixaService>();
            services.AddSingleton<ICentroCustoService, CentroCustoService>();
            services.AddSingleton<IContaService, ContaService>();
            services.AddSingleton<IDashboardService, DashboardService>();
            services.AddSingleton<IEmpresaService, EmpresaService>();
            services.AddSingleton<IPagarService, PagarService>();
            services.AddSingleton<IPessoaService, PessoaService>();
            services.AddSingleton<IPlanoContaService, PlanoContaService>();
            services.AddSingleton<IReceberService, ReceberService>();
            services.AddSingleton<ITransferenciaService, TransferenciaService>();
            services.AddSingleton<IUsuarioService, UsuarioService>();
            services.AddTransient<IPessoaService, PessoaService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Inicio/Error");
            }

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ControleAcesso}/{action=Login}/{id?}");
            });
        }
    }
}
