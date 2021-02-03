using Microsoft.Extensions.DependencyInjection;
using PIZZA.WebAssembly.Service;

namespace PIZZA.WebAssembly.Extension
{
    public static class PopupExtension
    {
        public static IServiceCollection AddPopupService(this IServiceCollection serviceDescriptors, string defaultPopupID = null)
        {
            PopupService popupService = new PopupService(defaultPopupID);
            serviceDescriptors.AddSingleton(popupService);
            return serviceDescriptors;
        }
    }
}
