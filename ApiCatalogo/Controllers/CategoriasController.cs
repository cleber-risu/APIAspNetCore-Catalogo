
using ApiCatalogo.Models.DTOs;
using ApiCatalogo.Models.DTOs.Mappings;
using ApiCatalogo.Models.Pagination;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController(IUnityOfWork _repository) : ControllerBase
{
  [HttpGet("pagination")]
  public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters parameters)
  {
    var categorias = await _repository.CategoriaRepository.GetCategorias(parameters);
    return ObterCategorias(categorias);
  }

  [HttpGet("filter/nome/pagination")]
  public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasFiltradas([FromQuery] CategoriasFiltroNome categoriasFiltro)
  {
    var categoriasFiltradas = await _repository.CategoriaRepository.GetCategoriasFiltroNome(categoriasFiltro);
    return ObterCategorias(categoriasFiltradas);
  }

  private ActionResult<IEnumerable<CategoriaDTO>> ObterCategorias(PagedList<Models.Entities.Categoria> categorias)
  {
    var metadata = new
    {
      categorias.TotalCount,
      categorias.PageSize,
      categorias.CurrentPage,
      categorias.TotalPages,
      categorias.HasPrevious,
      categorias.HasNext
    };

    Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

    var categoriasDto = categorias.ToCategoriaDTOList();

    return Ok(categoriasDto);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
  {
    var categorias = await _repository.CategoriaRepository.GetAll();

    if (categorias is null) return NotFound("A lista de categorias está vazia.");

    var categoriasDTO = categorias.ToCategoriaDTOList();

    return Ok(categoriasDTO);
  }

  [HttpGet("{id:int}", Name = "ObterCategoria")]
  public async Task<ActionResult<CategoriaDTO>> Get(int id)
  {
    var categoria = await _repository.CategoriaRepository.Get(c => c.Id == id);

    if (categoria is null) return NotFound($"A categoria com id = {id} não existe!");

    var categoriaDTO = categoria.ToCategoriaDTO();

    return Ok(categoriaDTO);
  }

  [HttpPost]
  public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDTO)
  {
    if (categoriaDTO is null) return BadRequest("Dados invalidos");

    var categoria = categoriaDTO.ToCategoria()!;

    var categoriaCriada = _repository.CategoriaRepository.Create(categoria);
    await _repository.Commit();

    var novaCategoriaDTO = categoriaCriada.ToCategoriaDTO();

    return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDTO!.Id }, novaCategoriaDTO);
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDTO)
  {
    if (id != categoriaDTO.Id) return BadRequest("Dados invalidos");

    var existe = _repository.CategoriaRepository.Exists(c => c.Id == id);

    if (!existe) return NotFound($"A categoria com id = {id} não existe!");

    var categoria = categoriaDTO.ToCategoria()!;

    var categoriaEditada = _repository.CategoriaRepository.Update(categoria);
    await _repository.Commit();

    var editadaCategoriaDTO = categoriaEditada.ToCategoriaDTO();

    return Ok(editadaCategoriaDTO);
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult<CategoriaDTO>> Delete(int id)
  {
    var categoria = await _repository.CategoriaRepository.Get(c => c.Id == id);

    if (categoria is null) return NotFound($"A categoria com id = {id} não existe!");

    var categoriaExcluida = _repository.CategoriaRepository.Delete(categoria);
    await _repository.Commit();

    var excluidaCategoriaDTO = categoriaExcluida.ToCategoriaDTO();

    return Ok(excluidaCategoriaDTO);
  }
}

