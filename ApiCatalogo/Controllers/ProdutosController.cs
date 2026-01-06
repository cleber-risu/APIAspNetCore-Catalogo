
using ApiCatalogo.Models.DTOs;
using ApiCatalogo.Models.Entities;
using ApiCatalogo.Models.Pagination;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AutoMapper;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController(IUnityOfWork _repository, IMapper _mapper) : ControllerBase
{

  [HttpGet("pagination")]
  public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters parameters)
  {
    var produtos = await _repository.ProdutoRepository.GetProdutos(parameters);

    return ObterProdutos(produtos);
  }

  [HttpGet("filter/preco/pagination")]
  public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco parameters)
  {
    var produtos = await _repository.ProdutoRepository.GetProdutosFiltroPreco(parameters);

    return ObterProdutos(produtos);
  }

  private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(PagedList<Produto> produtos)
  {
    var metadata = new
    {
      produtos.TotalCount,
      produtos.PageSize,
      produtos.CurrentPage,
      produtos.TotalPages,
      produtos.HasPrevious,
      produtos.HasNext
    };

    Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

    var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

    return Ok(produtosDto);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
  {
    var produtos = await _repository.ProdutoRepository.GetAll();

    if (produtos is null) return NotFound("Nenhum produto cadastrado no sistema!");

    // var destino = _mapper.Map<Destino>(origem);
    var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

    return Ok(produtosDto);
  }

  [HttpPatch("{id}/UpdatePartial")]
  public async Task<ActionResult<ProdutoDTOUpdateResponse>> Patch(
    int id,
    JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO
    )
  {
    if (patchProdutoDTO is null || id <= 0) return BadRequest();

    var produto = await _repository.ProdutoRepository.Get(p => p.Id == id);

    if (produto is null) return NotFound();

    var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

    patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

    if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
      return BadRequest(ModelState);

    _mapper.Map(produtoUpdateRequest, produto);

    _repository.ProdutoRepository.Update(produto);
    await _repository.Commit();

    return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
  }

  [HttpGet("categoria/{id}")]
  public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorCategoria(int id)
  {
    var produtos = await _repository.ProdutoRepository.GetProdutosPorCategoria(id);

    if (produtos is null) return NotFound();

    var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

    return Ok(produtosDto);
  }

  [HttpGet("{id:int}", Name = "ObterProduto")]
  public async Task<ActionResult<ProdutoDTO>> Get(int id)
  {
    var produto = await _repository.ProdutoRepository.Get(p => p.Id == id);

    if (produto is null) return NotFound("Produto não encontrado");

    var produtoDto = _mapper.Map<ProdutoDTO>(produto);

    return Ok(produtoDto);
  }

  [HttpPost]
  public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDto)
  {
    if (produtoDto is null) return BadRequest();

    var produto = _mapper.Map<Produto>(produtoDto);

    var novoProduto = _repository.ProdutoRepository.Create(produto);
    await _repository.Commit();

    var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

    return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDto.Id }, novoProdutoDto);
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDto)
  {
    var existe = _repository.ProdutoRepository.Exists(p => p.Id == id);

    if (id != produtoDto.Id) return BadRequest();

    if (!existe) return NotFound("Produto não encontrado");

    var produto = _mapper.Map<Produto>(produtoDto);

    var produtoEditado = _repository.ProdutoRepository.Update(produto);
    await _repository.Commit();

    var produtoDtoEditado = _mapper.Map<ProdutoDTO>(produtoEditado);

    return Ok(produtoDtoEditado);
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult<ProdutoDTO>> Delete(int id)
  {
    var produto = await _repository.ProdutoRepository.Get(p => p.Id == id);

    if (produto is null) return NotFound($"O produto de id = {id} não existe.");

    var produtoExcluido = _repository.ProdutoRepository.Delete(produto);
    await _repository.Commit();

    var produtoDtoExcluido = _mapper.Map<ProdutoDTO>(produtoExcluido);

    return Ok(produtoDtoExcluido);
  }
}

