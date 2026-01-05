
using ApiCatalogo.Context;
using ApiCatalogo.Models.Entities;
using ApiCatalogo.Models.Pagination;
using ApiCatalogo.Repositories.interfaces;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository(ApiCatalogoContext _context) : Repositoy<Produto>(_context), IProdutoRepository
{
  // public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters)
  // {
  //   return GetAll()
  //           .OrderBy(p => p.Nome)
  //           .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
  //           .Take(produtosParameters.PageSize).ToList();
  // }
  public PagedList<Produto> GetProdutos(ProdutosParameters parameters)
  {
    var produtos = GetAll() // AsQueryable - converte IEnumerable para IQueryable
                    .OrderBy(p => p.Id).AsQueryable();
    var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, parameters.PageNumber, parameters.PageSize);

    return produtosOrdenados;
  }

  public IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId)
  {
    return GetAll().Where(c => c.CategoriaId == categoriaId);
  }
}
