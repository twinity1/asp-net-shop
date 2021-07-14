namespace DyShop.Helpers.Controller
{
    public static class ControllerValidationHelper
    {
        public static bool Validate(this Microsoft.AspNetCore.Mvc.Controller controller, object model)
        {
            controller.ModelState.Clear();
            return controller.TryValidateModel(model);
        }
    }
}