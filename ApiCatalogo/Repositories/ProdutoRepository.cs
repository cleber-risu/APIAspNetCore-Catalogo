
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository(ApiCatalogoContext _context) : Repositoy<Produto>(_context), IProdutoRepository
{
  public IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId)
  {
    return GetAll().Where(c => c.CategoriaId == categoriaId);
  }
}
