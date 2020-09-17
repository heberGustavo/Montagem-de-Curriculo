using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Montagem_de_Curriculo.Models;
using Rotativa.AspNetCore;

namespace Montagem_de_Curriculo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation(); //Executar ao Atualizar

            /*Conexão do banco*/
            services.AddDbContext<Contexto>(x => x.UseSqlServer(Configuration.GetConnectionString("ConexaoDB")));

            /*Configuração Sessão*/
            services.AddSession(opcoes =>
            {
                //Tempo de duração
                //opcoes.IdleTimeout = TimeSpan.FromDays(1);
                opcoes.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            /*Configuração da Autenticação*/
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opcoes =>
                {
                    opcoes.LoginPath = "/Usuarios/Login";
                });

            /*Para usar as sessões é necessario injetar dependencias da Classe HttpContextAccessor*/
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*Criação do Banco de Dados*/
            using (var escopo = app.ApplicationServices.GetRequiredService<IServiceProvider>().CreateScope())
            {
                var contexto = escopo.ServiceProvider.GetRequiredService<Contexto>();
                contexto.Database.EnsureCreated(); /*Se dar o comando de insert e a tabela não tiver criada, com esse comando ela é criada automaticamente*/
            }

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

            /* Necessarios*/
            app.UseAuthentication();
            app.UseSession();

            /* PARA EXPORTAR PARA PDF
             * 
             * Pacote instalado = Install-Package Rotativa.AspNetCore -Version 1.2.0-beta
             * Link arquivos = https://github.com/webgio/Rotativa;
            */

            RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuarios}/{action=Registro}/{id?}");
            });
        }
    }
}
