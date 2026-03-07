using AuthCar.API.Middleware;
using AuthCar.API.Swagger;
using AuthCar.Application.DTOs;
using AuthCar.Application.DTOs.Auth;
using AuthCar.Application.Validators;
using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Domain.Interfaces;
using AuthCar.Infrastructure.Data;
using AuthCar.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
            // CONFIGURATION & ENVIRONMENT
            // =========================================
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            if (envName.Equals("Development", StringComparison.OrdinalIgnoreCase))
            {
                DotNetEnv.Env.Load("./Config/.env");
            }

            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{envName}.json", optional: true)
              .AddEnvironmentVariables()
              .Build();

            var jwtSection = builder.GetSection("JwtSettings");
            if (!string.IsNullOrEmpty(jwtIssuer)) jwtSection["Issuer"] = jwtIssuer;
            if (!string.IsNullOrEmpty(jwtAudience)) jwtSection["Audience"] = jwtAudience;
            if (!string.IsNullOrEmpty(jwtKey)) jwtSection["SecretKey"] = jwtKey;

            // =========================================
            // JWT SETTINGS
            // =========================================
            services.Configure<JwtSettings>(builder.GetSection("JwtSettings"));
            services.AddSingleton<IJwtSettings>(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AuthCarDb"));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
            services.AddTransient<IValidator<AuthRequestDTO>, AuthValidator>();

            // =========================================
            // AUTENTICAÇÃO JWT
            // =========================================
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var secretKey = jwtSection["SecretKey"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey))
                };
            });

            services.AddAuthorization();

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

                options.OperationFilter<StandardResponsesOperationFilter>();

                options.SwaggerDoc("auth", new Microsoft.OpenApi.OpenApiInfo
                {
                    Title = "Autenticação API",
                    Version = "v1",
                    Description = "Endpoints relacionados à autenticação"
                });

                options.SwaggerDoc("usuarios", new Microsoft.OpenApi.OpenApiInfo
                {
                    Title = "Usuários API",
                    Version = "v1",
                    Description = "Endpoints relacionados a usuários"
                });

                options.SwaggerDoc("veiculos", new Microsoft.OpenApi.OpenApiInfo
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
                        "auth" => controllerName?.Equals("Auth", StringComparison.OrdinalIgnoreCase) == true,
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
            // SEED DO BANCO IN-MEMORY
            // =========================================
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            // =========================================
            // MIDDLEWARES
            // =========================================

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/auth/swagger.json", "Autenticação API");
                c.SwaggerEndpoint("/swagger/usuarios/swagger.json", "Usuários API");
                c.SwaggerEndpoint("/swagger/veiculos/swagger.json", "Veículos API");
                c.RoutePrefix = string.Empty;
            });



            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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