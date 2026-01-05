
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

  public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
  {
    var produtos = GetAll().AsQueryable();

    if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
    {
      if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
      {
        produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
      }
      else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
      {
        produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
      }
      else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
      {
        produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
      }
    }
    var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

    return produtosFiltrados;
  }

  public IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId)
  {
    return GetAll().Where(c => c.CategoriaId == categoriaId);
  }
}
