
using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController(IUnityOfWork _repository, IMapper _mapper) : ControllerBase
{

  [HttpGet]
  public ActionResult<IEnumerable<ProdutoDTO>> Get()
  {
    var produtos = _repository.ProdutoRepository.GetAll();

    if (produtos is null) return NotFound("Nenhum produto cadastrado no sistema!");

    // var destino = _mapper.Map<Destino>(origem);
    var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

    return Ok(produtosDto);
  }

  [HttpGet("categoria/{id}")]
  public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int id)
  {
    var produtos = _repository.ProdutoRepository.GetProdutosPorCategoria(id);

    if (produtos is null) return NotFound();

    var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

    return Ok(produtosDto);
  }

  [HttpGet("{id:int}", Name = "ObterProduto")]
  public ActionResult<ProdutoDTO> Get(int id)
  {
    var produto = _repository.ProdutoRepository.Get(p => p.Id == id);

    if (produto is null) return NotFound("Produto não encontrado");

    var produtoDto = _mapper.Map<ProdutoDTO>(produto);

    return Ok(produtoDto);
  }

  [HttpPost]
  public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
  {
    if (produtoDto is null) return BadRequest();

    var produto = _mapper.Map<Produto>(produtoDto);

    var novoProduto = _repository.ProdutoRepository.Create(produto);
    _repository.Commit();

    var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

    return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDto.Id }, novoProdutoDto);
  }

  [HttpPut("{id:int}")]
  public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
  {
    var existe = _repository.ProdutoRepository.Exists(p => p.Id == id);

    if (id != produtoDto.Id) return BadRequest();

    if (!existe) return NotFound("Produto não encontrado");

    var produto = _mapper.Map<Produto>(produtoDto);

    var produtoEditado = _repository.ProdutoRepository.Update(produto);
    _repository.Commit();

    var produtoDtoEditado = _mapper.Map<ProdutoDTO>(produtoEditado);

    return Ok(produtoDtoEditado);
  }

  [HttpDelete("{id:int}")]
  public ActionResult<ProdutoDTO> Delete(int id)
  {
    var produto = _repository.ProdutoRepository.Get(p => p.Id == id);

    if (produto is null) return NotFound($"O produto de id = {id} não existe.");

    var produtoExcluido = _repository.ProdutoRepository.Delete(produto);
    _repository.Commit();

    var produtoDtoExcluido = _mapper.Map<ProdutoDTO>(produtoExcluido);

    return Ok(produtoDtoExcluido);
  }
}

