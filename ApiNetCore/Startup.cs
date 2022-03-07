using Api.Core.Interfaces;
using Api.Core.Servicios;
using Api.infraestructura.Datos;
using Api.infraestructura.Filtros;
using Api.infraestructura.Repositorios;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Auto Mapeos
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //se agrego referencia circular nula.
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            //Apicontroller no valida, se hace manual.
            .ConfigureApiBehaviorOptions(option => { 
                //option.SuppressModelStateInvalidFilter = true; 
            });

            //conexion a la base datos
            services.AddDbContext<SocialApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SocialApi"))
            );
            //Dependencias
            services.AddTransient<IPublicacionServicio, PublicacionServicio>();
            //Registro base repositorio
            services.AddScoped(typeof(IRepositorio<>), typeof(BaseRepositorio<>));
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            
            //Registrar filtro a nivel global
            services.AddMvc(Option =>
            {
                Option.Filters.Add<ValidacionFiltro>();
            }).AddFluentValidation(Options =>
            {
                Options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
