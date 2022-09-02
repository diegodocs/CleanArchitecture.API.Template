using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Api.Common.Core.Authentication;
using Api.Common.WebServer.Server;
using Api.Template.ApplicationService.InjectionModules;
using Api.Template.DbEFCore.Repositories;
using Api.Template.Domain.InjectionModules;
using Api.Template.Infrastructure.InjectionModules;
using Api.Template.WebApi.Server.Middlewares;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Api.Template.WebApi.Server;

public class Startup
{
    public Startup(
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        Configuration = configuration;

        var builder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
            .AddEnvironmentVariables();

        Configuration = builder.Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();

        Log.Information($"Starting up: {Assembly.GetEntryAssembly()?.GetName()}");
        Log.Information($"Environment: {environment.EnvironmentName}");
    }

    private static IEnumerable<int> Versions => new[] { 1 };

    private IConfiguration Configuration { get; }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        // IoC Container Module Registration
        builder.RegisterModule(new IoCModuleApplicationService());
        builder.RegisterModule(new IoCModuleInfrastructure());
        builder.RegisterModule(new IoCAutoMapperModule());
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddMvc(options => options.Filters.Add(typeof(ValidateModelAttribute)))
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

        var connectionStringApp = Configuration.GetConnectionString("ApiDbConnection");
        services.AddDbContext<EfCoreDbContext>(options => { options.UseSqlServer(connectionStringApp); });

        //Config Swagger
        services.AddSwaggerGen(c =>
        {
            Versions.ToList()
                .ForEach(v =>
                    c.SwaggerDoc($"v{v}",
                        new OpenApiInfo
                        {
                            Title = $"Api.Template.WebApi: v{v}",
                            Version = $"v{v}"
                        }));

            c.OperationFilter<RemoveVersionFromParameter>();
            c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        services.AddCors(config =>
        {
            var policy = new CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;
            config.AddPolicy("policy", policy);
        });

        var signingConfigurations = new SigningConfigurations();
        services.AddSingleton(signingConfigurations);

        var tokenConfigurations = new TokenConfigurations();
        services.AddSingleton(tokenConfigurations);

        services.AddAuthentication(
            authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = tokenConfigurations.Audience,
                ValidIssuer = tokenConfigurations.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingConfigurations.Key,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        context.Response.Headers.Add("Token-Expired", "true");
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
        });

        services.AddMemoryCache();
        services.AddApplicationInsightsTelemetry();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.UseMiddleware<ApiResponseMiddleware>();
        loggerFactory.AddSerilog();
        app.UseMiddleware<SerilogMiddleware>();


        app.UseCors(builder =>
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

        app.UseSwagger();
        app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.Template.WebApi Documentation"));
        app.UseAuthentication();
        app.UseRouting();

        //Execute Database Migration
        UpdateDatabaseUsingEfCore(app);
    }

    private void UpdateDatabaseUsingEfCore(IApplicationBuilder app)
    {
        Log.Information("Starting: Database Migration");

        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
        {
            serviceScope?.ServiceProvider
                .GetRequiredService<EfCoreDbContext>()
                .Database
                .Migrate();
        }

        Log.Information("Ending: Database Migration");
    }
}