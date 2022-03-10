using Api.Core.Interfaces;
using Api.Core.Servicios;
using Api.infraestructura.Datos;
using Api.infraestructura.Filtros;
using Api.infraestructura.Interfaces;
using Api.infraestructura.Repositorios;
using Api.infraestructura.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()); });
            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);//Ruta
            //Auto Mapeos
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //se agrego referencia circular nula.
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExcepcionFiltro>();
            }).AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //las propiedades null las ignora
                option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
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
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absolutUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absolutUri);
            });
            
            //Registrar filtro a nivel global
            services.AddMvc(Option =>
            {
                Option.Filters.Add<ValidacionFiltro>();
            }).AddFluentValidation(Options =>
            {
                Options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });//formato personalizado con fluentvalidator
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("easy");
            //Si trabajamos con un framework desde el front lo siguiente no hace falta
            app.Use((context, next) =>
            {
                context.Items["__CorsMiddlewareInvoked"] = true;
                return next();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            //fin

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
