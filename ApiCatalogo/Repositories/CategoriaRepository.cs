
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository(ApiCatalogoContext _context) : ICategoriaRepository
{

  public IEnumerable<Categoria> GetAll()
  {
    return _context.Categorias.AsNoTracking().ToList();
  }

  public bool Exists(int id)
  {
    return _context.Categorias.Any(c => c.Id == id);
  }

  public Categoria? GetById(int id)
  {
    return _context.Categorias.AsNoTracking().FirstOrDefault(c => c.Id == id);
  }

  public Categoria Create(Categoria categoria)
  {
    ArgumentNullException.ThrowIfNull(categoria, nameof(categoria));

    _context.Categorias.Add(categoria);
    _context.SaveChanges();

    return categoria;
  }

  public Categoria Update(Categoria categoria)
  {
    ArgumentNullException.ThrowIfNull(categoria, nameof(categoria));

    _context.Entry(categoria).State = EntityState.Modified;
    _context.SaveChanges();

    return categoria;
  }

  public Categoria Delete(int id)
  {
    var categoria = _context.Categorias.Find(id);

    ArgumentNullException.ThrowIfNull(categoria, nameof(categoria));

    _context.Categorias.Remove(categoria);
    _context.SaveChanges();

    return categoria;
  }
}

