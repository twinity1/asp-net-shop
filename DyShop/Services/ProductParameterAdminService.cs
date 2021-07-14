using System.Linq;
using DyShop.Areas.Admin.Models;
using DyShop.Data.Entities;

namespace DyShop.Services
{
    public class ProductParameterAdminService
    {
        public void Map(ProductParameterViewModel vm, ProductParameterGroup parameterGroup)
        {
            parameterGroup.Title = vm.Title;

            foreach (var productParameter in parameterGroup.Parameters.ToList())
            {
                var productParameterVm = vm.Parameters.FirstOrDefault(x => x.Id == productParameter.Id);

                if (productParameterVm == null)
                {
                    parameterGroup.Parameters.Remove(productParameter);
                }
                else
                {
                    MapParameter(productParameterVm, productParameter);
                }
            }

            foreach (var productParameterVm in vm.Parameters.Where(x => x.Id == 0))
            {
                var newParam = new ProductParameter
                {
                    Group = parameterGroup,
                };
                
                MapParameter(productParameterVm, newParam);
                
                parameterGroup.Parameters.Add(newParam);
            }
        }

        private void MapParameter(ProductParameterViewModel.ParameterViewModel vm, ProductParameter productParameter)
        {
            productParameter.Title = vm.Title;
        }
    }
}