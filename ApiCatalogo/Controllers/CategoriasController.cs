
using System.Collections;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController(ICategoriaRepository repository) : ControllerBase
{

  private readonly ICategoriaRepository _repository = repository;

  private static List<Categoria> _list = [
    new() { Id = 1, Nome = "Ferramentas", ImagemUrl = "ferragens.png" },
    new() { Id = 2, Nome = "Eletronicos", ImagemUrl = "eletro.png" },
    new() { Id = 3, Nome = "Informatica", ImagemUrl = "info.png" }
  ];

  [HttpGet]
  public ActionResult<IEnumerable<Categoria>> Get()
  {
    var categorias = _repository.GetAll();
    return Ok(categorias);
  }

  [HttpGet("{id:int}", Name = "ObterCategoria")]
  public ActionResult Get(int id)
  {
    var categoria = _repository.GetById(id);

    if (categoria is null) return NotFound($"A categoria com id = {id} não existe!");

    return Ok(categoria);
  }

  [HttpPost]
  public ActionResult Post(Categoria categoria)
  {
    if (categoria is null) return BadRequest("Dados invalidos");

    var categoriaCriada = _repository.Create(categoria);

    return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.Id }, categoriaCriada);
  }

  [HttpPut("{id:int}")]
  public ActionResult Put(int id, Categoria categoriaParaEdicao)
  {
    var categoria = _repository.GetById(id);

    if (categoria is null) return NotFound($"A categoria com id = {id} não existe!");

    if (id != categoriaParaEdicao.Id) return BadRequest("Dados invalidos");

    var categoriaEditada = _repository.Update(categoriaParaEdicao);

    return Ok(categoriaEditada);
  }

  [HttpDelete("{id:int}")]
  public ActionResult DeletarCategoria(int id)
  {
    var existe = _repository.Exists(id);

    if (!existe) return NotFound($"A categoria com id = {id} não existe!");

    var categoriaExcluida = _repository.Delete(id);

    return Ok(categoriaExcluida);
  }
}

