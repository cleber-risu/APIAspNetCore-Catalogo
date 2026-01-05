
using ApiCatalogo.Models.Entities;
using ApiCatalogo.Models.Pagination;

namespace ApiCatalogo.Repositories.interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
  PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
}

