
namespace ApiCatalogo.Repositories.interfaces;

public interface IUnityOfWork
{
  IProdutoRepository ProdutoRepository { get; }
  ICategoriaRepository CategoriaRepository { get; }

  Task Commit();
}

