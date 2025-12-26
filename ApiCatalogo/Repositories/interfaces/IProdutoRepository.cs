
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories.interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
  IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId);
}

