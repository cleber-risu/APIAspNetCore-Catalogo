
namespace ApiCatalogo.Models;

public class Categoria
{
  public long Id { get; set; }

  public string? Nome { get; set; }

  public string? ImagemUrl { get; set; }

  public ICollection<Produto> Produtos { get; set; } = [];
}
