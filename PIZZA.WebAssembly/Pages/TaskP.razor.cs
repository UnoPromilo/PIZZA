using Microsoft.AspNetCore.Components;
using PIZZA.Models.Task;
using PIZZA.WebAssembly.Api.Services;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class TaskP
    {
        [Parameter]
        public string TaskID { get; set; }

        [Inject]
        private ITaskService TaskService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        private TaskModelWithActualStateAndCreator Task = new();

        protected override async Task OnInitializedAsync()
        {
            if(!int.TryParse(TaskID, out int taskID)) NavigationManager.NavigateTo("/tasks");
            Task = await TaskService.Get(taskID);
            if (TaskID != Task.ID.ToString()) NavigationManager.NavigateTo("/tasks");
            await base.OnInitializedAsync();
        }

        //public TaskHistoryModel TaskModel { get; set; } = new();
    }
}
