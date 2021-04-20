using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZAPNET.DemoFina.DB;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // Adicionar os serviços
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            
            // Adicionando a minha ApplicationContext, para fazer a injeção de dependência, com uso do SQLServer.
            services.AddDbContext<ZAPNETApplicationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddDbContext<ConnectionDB>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("Default")));


            //INJEÇÃO DE DEPENDENCIA
            //ContaDFRepository
            services.AddTransient<ICrudRepository<ContaDF>, ContaDFRepository>();
            //EnderecoRepository
            services.AddTransient<ICrudRepository<Endereco>, EnderecoRepository>();
            //EmpresaRepository
            services.AddTransient<ICrudRepository<Empresa>, EmpresaRepository>();
            //CosifRepository
            services.AddTransient<ICosifRepository, CosifRepository>();
            //RelaContaDFCosifRepository
            services.AddTransient<IRelaContaDFCosifRepository, RelaContaDFCosifRepository>();
            //ModeloDFRepository
            services.AddTransient<ICrudRepository<ModeloDF>, ModeloDFRepository>();

        }

        // Consome os serviços, utilizar os serviços
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
