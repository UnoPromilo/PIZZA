using Microsoft.AspNetCore.Components;

namespace PIZZA.WebAssembly.Pages.Instalation
{
    public partial class InstalationSection
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Description { get; set; }
    }
}
