
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models;

[Table("Categorias")]
public class Categoria
{
  [Key]
  public int Id { get; set; }

  [Required]
  [StringLength(80)]
  public string? Nome { get; set; }

  [Required]
  [StringLength(300)]
  public string? ImagemUrl { get; set; }

  [JsonIgnore]
  public ICollection<Produto> Produtos { get; set; } = [];
}
