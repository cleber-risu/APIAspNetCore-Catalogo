
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController(IProdutoRepository _repository) : ControllerBase
{

  [HttpGet]
  public ActionResult<IEnumerable<Produto>> Get()
  {
    var produtos = _repository.GetAll();

    if (produtos is null) return NotFound("Nenhum produto cadastrado no sistema!");

    return Ok(produtos);
  }

  [HttpGet("{id:int}", Name = "ObterProduto")]
  public ActionResult<Produto> Get(int id)
  {
    var produto = _repository.GetById(id);

    if (produto is null) return NotFound("Produto não encontrado");

    return Ok(produto);
  }

  [HttpPost]
  public ActionResult Post(Produto produto)
  {
    if (produto is null) return BadRequest();

    var novoProduto = _repository.Create(produto);

    return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.Id }, novoProduto);
  }

  [HttpPut("{id:int}")]
  public ActionResult Put(int id, Produto produtoParaEdicao)
  {
    var existe = _repository.Exists(id);

    if (!existe) return NotFound("Produto não encontrado");

    if (id != produtoParaEdicao.Id) return BadRequest();

    var produtoEditado = _repository.Update(produtoParaEdicao);

    return Ok(produtoEditado);
  }

  [HttpDelete("{id:int}")]
  public ActionResult Delete(int id)
  {
    var existe = _repository.Exists(id);

    if (!existe) return NotFound($"O produto de id = {id} não existe.");

    var produtoExcluido = _repository.Delete(id);

    return Ok(produtoExcluido);
  }
}

