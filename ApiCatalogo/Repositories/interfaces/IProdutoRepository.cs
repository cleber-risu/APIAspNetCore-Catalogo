
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories.interfaces;

public interface IProdutoRepository
{
  IEnumerable<Produto> GetAll();
  bool Exists(int id);
  Produto GetById(int id);
  Produto Create(Produto produto);
  Produto Update(Produto produto);
  Produto Delete(int id);
}

