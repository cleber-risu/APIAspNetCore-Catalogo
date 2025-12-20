
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository(ApiCatalogoContext _context) : IProdutoRepository
{

  public IEnumerable<Produto> GetAll()
  {
    return _context.Produtos.AsNoTracking().ToList();
  }

  public bool Exists(int id)
  {
    return _context.Produtos.Any(p => p.Id == id);
  }

  public Produto? GetById(int id)
  {
    return _context.Produtos.AsNoTracking().FirstOrDefault(p => p.Id == id);
  }

  public Produto Create(Produto produto)
  {
    ArgumentNullException.ThrowIfNull(produto, nameof(produto));

    _context.Produtos.Add(produto);
    _context.SaveChanges();

    return produto;
  }

  public Produto Update(Produto produto)
  {
    ArgumentNullException.ThrowIfNull(produto, nameof(produto));

    _context.Entry(produto).State = EntityState.Modified;
    _context.SaveChanges();

    return produto;
  }

  public Produto Delete(int id)
  {
    var produto = _context.Produtos.Find(id);

    ArgumentNullException.ThrowIfNull(produto, nameof(produto));

    _context.Produtos.Remove(produto);
    _context.SaveChanges();

    return produto;
  }
}
