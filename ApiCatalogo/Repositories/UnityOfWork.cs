
using ApiCatalogo.Context;
using ApiCatalogo.Repositories.interfaces;

namespace ApiCatalogo.Repositories;

public class UnityOfWork(ApiCatalogoContext _context) : IUnityOfWork
{

  private IProdutoRepository? _produtoRepository;

  private ICategoriaRepository? _categoriaRepository;

  public IProdutoRepository ProdutoRepository
  {
    get
    {
      return _produtoRepository ??= new ProdutoRepository(_context);
    }
  }

  public ICategoriaRepository CategoriaRepository
  {
    get
    {
      return _categoriaRepository ??= new CategoriaRepository(_context);
    }
  }

  public void Commit()
  {
    _context.SaveChanges();
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}

