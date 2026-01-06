
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
  public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters parameters)
  {
    var produtos = await GetAll();

    // AsQueryable - converte IEnumerable para IQueryable
    var produtosOrdenados = produtos.OrderBy(p => p.Id).AsQueryable();
    var produtosPaginados = PagedList<Produto>.ToPagedList(produtosOrdenados.AsQueryable(), parameters.PageNumber, parameters.PageSize);

    return produtosPaginados;
  }

  public async Task<PagedList<Produto>> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
  {
    var produtos = await GetAll();

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
    var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos.AsQueryable(), produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

    return produtosFiltrados;
  }

  public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int categoriaId)
  {
    var produtos = await GetAll();
    var produtosCategoria = produtos.Where(c => c.CategoriaId == categoriaId);
    return produtosCategoria;
  }
}
