using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhoneCase.Business.Abstract;
using PhoneCase.Business.Concrete;
using PhoneCase.Business.Mappings;
using PhoneCase.Data;
using PhoneCase.Data.Abstract;
using PhoneCase.Data.Concreate.Repositories;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Configurations.Auth;

namespace PhoneCase.API;

public static class DependencyInjection
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services)
    {
        services.Configure<JwtConfig>(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection("JwtConfig"));
        return services;
    }
    public static IServiceCollection AddGeneralServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(CategoryProfile).Assembly);
        return services;
    }
    public static IServiceCollection AddContextServices(this IServiceCollection services)
    {

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"),
            sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); // Retry mekanizması
                    sqlOptions.CommandTimeout(60); // 60 saniye timeout
                }
            );
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
     {
         options.Password.RequireDigit = true;
         options.Password.RequiredLength = 6;
         options.Password.RequireLowercase = true;
         options.Password.RequireUppercase = true;
         options.Password.RequireNonAlphanumeric = true;
         options.User.RequireUniqueEmail = true;
     })
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
        return services;
    }
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        var jwtConfig = (services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection("JwtConfig")).Get<JwtConfig>();


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
                      ValidIssuer = jwtConfig?.Issuer,
                      ValidAudience = jwtConfig?.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig?.Secret ?? ""))
                  };
              });
        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<IImageService, ImageManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<ICategoryTypeService, CategoryTypeManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IFavoriteService, FavoriteManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<ICartService, CartManager>();
        services.AddScoped<IOrderService, OrderManager>();
        return services;
    }
}
