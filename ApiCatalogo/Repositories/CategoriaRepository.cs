
using ApiCatalogo.Context;
using ApiCatalogo.Models.Entities;
using ApiCatalogo.Models.Pagination;
using ApiCatalogo.Repositories.interfaces;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository(ApiCatalogoContext _context) : Repositoy<Categoria>(_context), ICategoriaRepository
{
  public PagedList<Categoria> GetCategorias(CategoriasParameters parameters)
  {
    var categorias = GetAll()
                      .OrderBy(c => c.Id).AsQueryable();
    var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categorias, parameters.PageNumber, parameters.PageSize);

    return categoriasOrdenadas;
  }

  public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome parameters)
  {
    var categorias = GetAll().AsQueryable();

    if (!string.IsNullOrEmpty(parameters.Nome))
    {
      categorias = categorias.Where(c => c.Nome!.Contains(parameters.Nome));
    }

    var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias, parameters.PageNumber, parameters.PageSize);

    return categoriasFiltradas;
  }
}

