
using System.Text;
using ApiCatalogo.Context;
using ApiCatalogo.Models.Auth;
using ApiCatalogo.Models.DTOs.Mappings;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiCatalogoContext>(options =>
  options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                  .AddEntityFrameworkStores<ApiCatalogoContext>()
                    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

var secretKey = builder.Configuration["JWT:SecretKey"]
                ?? throw new ArgumentException("Invalid secret key!!!");

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // define o jwt como autenticação padrão
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // tenta obter o token para acessar a rota
}).AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ClockSkew = TimeSpan.Zero,
    ValidAudience = builder.Configuration["JT:ValidAudience"],
    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
  };
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repositoy<>));
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

// builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile).Assembly);
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<DomainToDTOMappingProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
