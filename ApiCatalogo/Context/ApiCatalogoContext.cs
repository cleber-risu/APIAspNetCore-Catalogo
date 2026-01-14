
using ApiCatalogo.Models.Auth;
using ApiCatalogo.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context;

public class ApiCatalogoContext(DbContextOptions<ApiCatalogoContext> context) : IdentityDbContext<ApplicationUser>(context)
{
  public DbSet<Categoria> Categorias { get; set; }
  public DbSet<Produto> Produtos { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
  }
}

