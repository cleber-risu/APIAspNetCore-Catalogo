
using ApiCatalogo.Models.Entities;

namespace ApiCatalogo.Models.DTOs.Mappings;

public static class CategoriaDTOMappingExtensions
{
  public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
  {
    if (categoria is null) return null;

    return new CategoriaDTO
    {
      Id = categoria.Id,
      Nome = categoria.Nome,
      ImagemUrl = categoria.ImagemUrl
    };
  }

  public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
  {
    if (categoriaDTO is null) return null;

    return new Categoria
    {
      Id = categoriaDTO.Id,
      Nome = categoriaDTO.Nome,
      ImagemUrl = categoriaDTO.ImagemUrl
    };
  }

  public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
  {
    if (categorias is null) return [];

    return categorias.Select(categoria => new CategoriaDTO
    {
      Id = categoria.Id,
      Nome = categoria.Nome,
      ImagemUrl = categoria.ImagemUrl
    }).ToList();
  }
}
