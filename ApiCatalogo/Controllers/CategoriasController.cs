
using System.Collections;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{

  private static List<Categoria> _list = [
    new() { Id = 1, Nome = "Ferramentas", ImagemUrl = "ferragens.png" },
    new() { Id = 2, Nome = "Eletronicos", ImagemUrl = "eletro.png" },
    new() { Id = 3, Nome = "Informatica", ImagemUrl = "info.png" }
  ];

  [HttpGet]
  public ActionResult<IEnumerable<Categoria>> Get()
  {
    return Ok(_list);
  }

  [HttpGet("{id:long}", Name = "ObterCategoria")]
  public ActionResult Get(long id)
  {
    var categoria = _list.SingleOrDefault(c => c.Id == id);

    if (categoria is null) return NotFound("A categoria não existe!");

    return Ok(categoria);
  }

  [HttpPost]
  public ActionResult Post(Categoria categoria)
  {
    if (categoria is null) return BadRequest("Dados invalidos");

    //categoria.Id = DateTime.Now.Ticks;
    _list.Add(categoria);

    return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id }, categoria);
  }

  [HttpPut("{id:int}")]
  public ActionResult Put(int id, Categoria categoria)
  {
    if (id != categoria.Id) return BadRequest("Dados invalidos");

    var categoriaEditada = _list.SingleOrDefault(c => c.Id == id);

    if (categoriaEditada is null) return NotFound("Categoria não encotrando");

    categoriaEditada.Nome = categoria.Nome;
    categoriaEditada.ImagemUrl = categoria.ImagemUrl;
    categoriaEditada.Produtos = categoriaEditada.Produtos;

    return Ok(categoriaEditada);
  }

  [HttpDelete("{id:long}")]
  public ActionResult DeletarCategoria(long id)
  {
    var categoria = _list.SingleOrDefault(c => c.Id == id);

    if (categoria is null) return NotFound("A categoria não existe!");

    _list = _list.Where(c => c.Id != id).ToList();

    return NoContent();
  }
}

