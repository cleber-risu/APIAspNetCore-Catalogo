
using ApiCatalogo.Context;
using ApiCatalogo.Models.Entities;
using ApiCatalogo.Models.Pagination;
using ApiCatalogo.Repositories.interfaces;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository(ApiCatalogoContext _context) : Repositoy<Categoria>(_context), ICategoriaRepository
{
  public async Task<PagedList<Categoria>> GetCategorias(CategoriasParameters parameters)
  {
    var categorias = await GetAll();
    var categoriasOrdenadas = categorias.OrderBy(c => c.Id).AsQueryable();

    var categoriasPaginadas = PagedList<Categoria>.ToPagedList(categoriasOrdenadas, parameters.PageNumber, parameters.PageSize);

    return categoriasPaginadas;
  }

  public async Task<PagedList<Categoria>> GetCategoriasFiltroNome(CategoriasFiltroNome parameters)
  {
    var categorias = await GetAll();

    if (!string.IsNullOrEmpty(parameters.Nome))
    {
      categorias = categorias.Where(c => c.Nome!.Contains(parameters.Nome));
    }

    var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias.AsQueryable(), parameters.PageNumber, parameters.PageSize);

    return categoriasFiltradas;
  }
}

