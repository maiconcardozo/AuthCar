using AuthCar.API.Swagger;
using AuthCar.Application.DTOs;
using AuthCar.Application.Services;
using AuthCar.Application.Validators;
using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Application;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Infrastructure.Data;
using AuthCar.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.Reflection;

namespace AuthCar.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // =========================================
            // LOCALIZATION
            // =========================================
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "pt-BR" };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(supportedCultures[0]);
                options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
                options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
            });


            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AuthCarDb"));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // =========================================
            // APPLICATION SERVICES
            // =========================================
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IAuthService, AuthService>();

            // =========================================
            // MEDIATR
            // =========================================
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AuthCar.Application.Commands.Usuario.AddUsuarioCommand).Assembly);
            });

            // =========================================
            // VALIDATORS
            // =========================================
            services.AddControllers();
            services.AddTransient<IValidator<UsuarioRequestDTO>, UsuarioValidator>();
            services.AddTransient<IValidator<VeiculoRequestDTO>, VeiculoValidator>();

            // =========================================
            // CONTROLLERS
            // =========================================
            services.AddControllers();

            // =========================================
            // SWAGGER
            // =========================================
            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor(); 

            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                options.EnableAnnotations();
                options.ExampleFilters();

                options.OperationFilter<LocalizedSwaggerOperationFilter>();

                options.SwaggerDoc("usuarios", new OpenApiInfo
                {
                    Title = "Usuários API",
                    Version = "v1",
                    Description = "Endpoints relacionados a usuários"
                });

                options.SwaggerDoc("veiculos", new OpenApiInfo
                {
                    Title = "Veículos API",
                    Version = "v1",
                    Description = "Endpoints relacionados a veículos"
                });

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var controllerName = apiDesc.ActionDescriptor.RouteValues["controller"];
                    return docName switch
                    {
                        "usuarios" => controllerName?.Equals("Usuario", StringComparison.OrdinalIgnoreCase) == true,
                        "veiculos" => controllerName?.Equals("Veiculo", StringComparison.OrdinalIgnoreCase) == true,
                        _ => false
                    };
                });
            });

            services.AddSwaggerExamplesFromAssemblyOf<SucessDetailsExample>();
            services.AddSwaggerExamplesFromAssemblyOf<ProblemDetailsBadRequestExample>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // =========================================
            // MIDDLEWARES
            // =========================================

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/usuarios/swagger.json", "Usuários API");
                c.SwaggerEndpoint("/swagger/veiculos/swagger.json", "Veículos API");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}