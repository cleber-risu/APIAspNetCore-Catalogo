
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories.interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
  // IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
  PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
  IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId);
}

