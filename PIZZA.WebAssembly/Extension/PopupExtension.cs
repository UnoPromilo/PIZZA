using Microsoft.Extensions.DependencyInjection;
using PIZZA.WebAssembly.Service;
using PIZZA.WebAssembly.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
