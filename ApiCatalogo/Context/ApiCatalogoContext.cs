
using ApiCatalogo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context;

public class ApiCatalogoContext(DbContextOptions<ApiCatalogoContext> context) : DbContext(context)
{
  public DbSet<Categoria> Categorias { get; set; }
  public DbSet<Produto> Produtos { get; set; }
}

