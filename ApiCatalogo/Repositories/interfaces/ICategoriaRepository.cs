
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories.interfaces;

public interface ICategoriaRepository
{
  IEnumerable<Categoria> GetAll();
  bool Exists(int id);
  Categoria GetById(int id);
  Categoria Create(Categoria categoria);
  Categoria Update(Categoria categoria);
  Categoria Delete(int id);
}

