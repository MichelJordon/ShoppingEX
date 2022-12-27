using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastucture.Meppers;
public class ServicesProfile:Profile
{
    public ServicesProfile()
    {
        CreateMap<Customer,GetCustomerDto>().ReverseMap();
        CreateMap<Customer, AddCustomerDto>().ReverseMap();
        CreateMap<GetCustomerDto, AddCustomerDto>().ReverseMap();
    }
}