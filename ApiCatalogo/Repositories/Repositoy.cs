
using System.Linq.Expressions;
using ApiCatalogo.Context;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class Repositoy<T>(ApiCatalogoContext _context) : IRepository<T> where T : class
{

  public async Task<IEnumerable<T>> GetAll()
  {
    return await _context.Set<T>().AsNoTracking().ToListAsync();
  }

  public bool Exists(Expression<Func<T, bool>> predicate)
  {
    return _context.Set<T>().Any(predicate);
  }

  public async Task<T?> Get(Expression<Func<T, bool>> predicate)
  {
    return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
  }

  public T Create(T entity)
  {
    _context.Set<T>().Add(entity);
    return entity;
  }

  public T Update(T entity)
  {
    _context.Set<T>().Update(entity);
    return entity;
  }

  public T Delete(T entity)
  {
    _context.Set<T>().Remove(entity);
    return entity;
  }
}

