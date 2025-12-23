
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController(IUnityOfWork _repository) : ControllerBase
{

  [HttpGet]
  public ActionResult<IEnumerable<Produto>> Get()
  {
    var produtos = _repository.ProdutoRepository.GetAll();

    if (produtos is null) return NotFound("Nenhum produto cadastrado no sistema!");

    return Ok(produtos);
  }

  [HttpGet("{id:int}", Name = "ObterProduto")]
  public ActionResult<Produto> Get(int id)
  {
    var produto = _repository.ProdutoRepository.Get(p => p.Id == id);

    if (produto is null) return NotFound("Produto não encontrado");

    return Ok(produto);
  }

  [HttpPost]
  public ActionResult Post(Produto produto)
  {
    if (produto is null) return BadRequest();

    var novoProduto = _repository.ProdutoRepository.Create(produto);
    _repository.Commit();

    return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.Id }, novoProduto);
  }

  [HttpPut("{id:int}")]
  public ActionResult Put(int id, Produto produtoParaEdicao)
  {
    var existe = _repository.ProdutoRepository.Exists(p => p.Id == id);

    if (id != produtoParaEdicao.Id) return BadRequest();

    if (!existe) return NotFound("Produto não encontrado");

    var produtoEditado = _repository.ProdutoRepository.Update(produtoParaEdicao);
    _repository.Commit();

    return Ok(produtoEditado);
  }

  [HttpDelete("{id:int}")]
  public ActionResult Delete(int id)
  {
    var produto = _repository.ProdutoRepository.Get(p => p.Id == id);

    if (produto is null) return NotFound($"O produto de id = {id} não existe.");

    var produtoExcluido = _repository.ProdutoRepository.Delete(produto);
    _repository.Commit();

    return Ok(produtoExcluido);
  }
}

