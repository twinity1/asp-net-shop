using AutoMapper;
using DyShop.Areas.Shop.Models;
using DyShop.Data.Entities;

namespace DyShop.Helpers.MapperProfiles
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<CartCheckoutViewModel.AddressViewModel, ClientAddress>();
        }
    }
}