
using System.Linq.Expressions;

namespace ApiCatalogo.Repositories.interfaces;

public interface IRepository<T>
{

  IEnumerable<T> GetAll();
  bool Exists(Expression<Func<T, bool>> predicate);
  T? Get(Expression<Func<T, bool>> predicate);
  T Create(T entity);
  T Update(T entity);
  T Delete(T entity);
}

