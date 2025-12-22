
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository(ApiCatalogoContext _context) : Repositoy<Produto>(_context), IProdutoRepository
{
}
