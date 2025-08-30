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


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); // Retry mekanizması
            sqlOptions.CommandTimeout(60); // 60 saniye timeout
        }
    );
});

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();


builder.Services.AddAuthentication(options =>
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

builder.Services.AddIdentity<User, IdentityRole>(options =>
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


//Contaniner'a ekleme
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Servisleri ekleme
builder.Services.AddScoped<IImageService, ImageManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryTypeService, CategoryTypeManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IFavoriteService, FavoriteManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();

//AutoMapper'a ekleme
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
