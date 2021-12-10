using AutoMapper.Configuration;
using Backend.Application.Dto;
using Backend.Core.Entities;

namespace Backend.Application.Mappers
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<OrderDetail, OrderPostDTO> ();
            CreateMap<OrderPostDTO, OrderDetail>();

            CreateMap<Order, OrdetListDTO>();
            CreateMap<OrdetListDTO, Order>();
        }
    }
}
