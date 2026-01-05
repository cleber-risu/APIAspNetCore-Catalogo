
using ApiCatalogo.Models.Entities;
using AutoMapper;

namespace ApiCatalogo.Models.DTOs.Mappings;

public class DomainToDTOMappingProfile : Profile
{
  public DomainToDTOMappingProfile()
  {
    CreateMap<Produto, ProdutoDTO>().ReverseMap();
    CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
    CreateMap<Produto, ProdutoDTOUpdateResponse>().ReverseMap();
  }
}

