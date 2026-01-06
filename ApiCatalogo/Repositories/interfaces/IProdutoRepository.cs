
using ApiCatalogo.Models.Entities;
using ApiCatalogo.Models.Pagination;

namespace ApiCatalogo.Repositories.interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
  // IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
  Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);
  Task<PagedList<Produto>> GetProdutosFiltroPreco(ProdutosFiltroPreco paramenters);
  Task<IEnumerable<Produto>> GetProdutosPorCategoria(int categoriaId);
}

