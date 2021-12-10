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

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();

            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<OrderDetailDTO, OrderDetail>();

            CreateMap<OrderLog, OrderLogDTO>();
            CreateMap<OrderLogDTO, OrderLog>();
        }
    }
}
