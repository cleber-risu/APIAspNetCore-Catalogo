
using System.Collections;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController(IUnityOfWork _repository) : ControllerBase
{

  [HttpGet]
  public ActionResult<IEnumerable<CategoriaDTO>> Get()
  {
    var categorias = _repository.CategoriaRepository.GetAll();

    if (categorias is null) return NotFound("A lista de categorias está vazia.");

    var categoriasDTO = categorias.ToCategoriaDTOList();

    return Ok(categoriasDTO);
  }

  [HttpGet("{id:int}", Name = "ObterCategoria")]
  public ActionResult<CategoriaDTO> Get(int id)
  {
    var categoria = _repository.CategoriaRepository.Get(c => c.Id == id);

    if (categoria is null) return NotFound($"A categoria com id = {id} não existe!");

    var categoriaDTO = categoria.ToCategoriaDTO();

    return Ok(categoriaDTO);
  }

  [HttpPost]
  public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO)
  {
    if (categoriaDTO is null) return BadRequest("Dados invalidos");

    var categoria = categoriaDTO.ToCategoria()!;

    var categoriaCriada = _repository.CategoriaRepository.Create(categoria);
    _repository.Commit();

    var novaCategoriaDTO = categoriaCriada.ToCategoriaDTO();

    return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDTO!.Id }, novaCategoriaDTO);
  }

  [HttpPut("{id:int}")]
  public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
  {
    if (id != categoriaDTO.Id) return BadRequest("Dados invalidos");

    var existe = _repository.CategoriaRepository.Exists(c => c.Id == id);

    if (!existe) return NotFound($"A categoria com id = {id} não existe!");

    var categoria = categoriaDTO.ToCategoria()!;

    var categoriaEditada = _repository.CategoriaRepository.Update(categoria);
    _repository.Commit();

    var editadaCategoriaDTO = categoriaEditada.ToCategoriaDTO();

    return Ok(editadaCategoriaDTO);
  }

  [HttpDelete("{id:int}")]
  public ActionResult<CategoriaDTO> Delete(int id)
  {
    var categoria = _repository.CategoriaRepository.Get(c => c.Id == id);

    if (categoria is null) return NotFound($"A categoria com id = {id} não existe!");

    var categoriaExcluida = _repository.CategoriaRepository.Delete(categoria);
    _repository.Commit();

    var excluidaCategoriaDTO = categoriaExcluida.ToCategoriaDTO();

    return Ok(excluidaCategoriaDTO);
  }
}

