using AutoMapper;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Entities;

namespace DyShop.Helpers.MapperProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<ClientAddress, OrderViewModel.AddressViewModel>();
            CreateMap<OrderViewModel.AddressViewModel, ClientAddress>();
        }
    }
}