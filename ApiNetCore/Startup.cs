using Api.Core.Interfaces;
using Api.Core.PersonalizadasEntidades;
using Api.Core.Servicios;
using Api.infraestructura.Datos;
using Api.infraestructura.Filtros;
using Api.infraestructura.Interfaces;
using Api.infraestructura.Repositorios;
using Api.infraestructura.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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
            services.AddSwaggerGen(c => 
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title="SOCIAL API", Version="1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                };
            });

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

            services.Configure<PaginacionOpciones>(Configuration.GetSection("Paginacion"));

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialMedia V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
