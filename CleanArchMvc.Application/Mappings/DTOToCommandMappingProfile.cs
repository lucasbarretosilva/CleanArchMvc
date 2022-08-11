using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commands;

namespace CleanArchMvc.Application.Mappings
{
    internal class DTOToCommandMappingProfile : Profile
    {

        public DTOToCommandMappingProfile()
        {
            CreateMap<ProductCreateCommand, ProductDTO>().ReverseMap();
            CreateMap<ProductUpdateCommand, ProductDTO>().ReverseMap();
            CreateMap<ProductRemoveCommand, ProductDTO>().ReverseMap();

        }
    }
}
