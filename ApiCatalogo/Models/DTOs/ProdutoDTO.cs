
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Models.DTOs;

public class ProdutoDTO
{
  public int Id { get; set; }

  public string? Nome { get; set; }

  [Required]
  [StringLength(300)]
  public string? Descricao { get; set; }

  [Required]
  public decimal Preco { get; set; }

  [Required]
  [StringLength(300)]
  public string? ImagemUrl { get; set; }

  public int CategoriaId { get; set; }

}

