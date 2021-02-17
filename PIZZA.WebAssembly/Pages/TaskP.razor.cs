using Microsoft.AspNetCore.Components;
using PIZZA.Models.Database;
using PIZZA.Models.Task;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class TaskP
    {
        [Parameter]
        public string TaskID { get; set; }

        [Inject]
        private PopupService PopupService { get; set; }

        [Inject]
        private ITaskService TaskService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        private TaskModelWithActualStateAndCreator TaskModel = new();

        private IList<EmployeeWithTaskRole> Participants;

        private IList<TaskStateModel> History;

        private IList<TaskNoteModel> Notes;

        protected override async Task OnInitializedAsync()
        {
            if(!int.TryParse(TaskID, out int taskID)) NavigationManager.NavigateTo("/tasks");
            
            await LoadTask(taskID);
            if (TaskID != TaskModel.ID.ToString()) NavigationManager.NavigateTo("/tasks");
            List<Task> tasks = new()
            {
                LoadParticipants(taskID),
                LoadNotes(taskID),
                LoadHistory(taskID),
            };
            await Task.WhenAll(tasks);
            await base.OnInitializedAsync();
        }

        private async Task LoadTask(int taskID)
        {
            TaskModel = await TaskService.Get(taskID);

        }
        private async Task LoadParticipants(int taskID)
        {
            Participants = await TaskService.GetUsersInTask(taskID);
        }

        private async Task LoadHistory(int taskID)
        {
            History = await TaskService.GetTaskStateHistory(taskID);
        }

        private async Task LoadNotes(int taskID)
        {
            Notes = await TaskService.GetTaskNotesForTask(taskID);
        }

        private async Task SaveTaskModel()
        {
            await TaskService.Update(TaskModel);
            await TaskService.CreateTaskState(new NewTaskStateModel
            {
                NewTaskState = TaskModel.TaskState,
                Task = TaskModel.ID,
                Note = $"Changed to {TaskModel.TaskState}"
            });
            List<Task> tasks = new()
            {
                LoadParticipants(TaskModel.ID),
                LoadNotes(TaskModel.ID),
                LoadHistory(TaskModel.ID),
            };
            await Task.WhenAll(tasks);
            StateHasChanged();
        }

        void Delete()
        {
            PopupAlertModel model = new()
            {
                Title = "USUWANIE",
                ContentAsString = "<p>Czy na pewno chcesz usunąć?</br> Uwaga, tej operacji nie można cofnąć!</p>",
                Buttons = new List<PopupButton>
            {
                new PopupButton
                {
                    Content = "Tak",
                    OnClick = (sender, args) => DeleteTask(TaskModel)
                },

                new PopupButton
                {
                    Content = "Nie"
                }
            }
            };

            PopupService.AddPopupModelToStack(model);
        }

        void DeleteTask(TaskModel task)
        {
            TaskService.Delete(task.ID);
            NavigationManager.NavigateTo("/tasks");
        }

    }
}
