
using ApiCatalogo.Models.Entities;
using ApiCatalogo.Models.Pagination;

namespace ApiCatalogo.Repositories.interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
  Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters);
  Task<PagedList<Categoria>> GetCategoriasFiltroNome(CategoriasFiltroNome parameters);
}

