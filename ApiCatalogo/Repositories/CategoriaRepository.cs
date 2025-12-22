
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository(ApiCatalogoContext _context) : Repositoy<Categoria>(_context), ICategoriaRepository
{
}

