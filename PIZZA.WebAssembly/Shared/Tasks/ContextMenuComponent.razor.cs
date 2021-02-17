using BlazorContextMenu;
using Microsoft.AspNetCore.Components;
using PIZZA.Models.Database;
using PIZZA.Models.Task;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System.Collections.Generic;

namespace PIZZA.WebAssembly.Shared.Tasks
{
    public partial class ContextMenuComponent
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        PopupService PopupService { get; set; }

        [Inject] ITaskService TaskService { get; set; }

        private TaskModel currentTask;
        private EmployeeWithTaskRole currentEmployee;

        private bool isForUser;

        void Navigate()
        {
            NavigationManager.NavigateTo($"task/{currentTask.ID}");
        }

        void NavigateToEmployee()
        {
            NavigationManager.NavigateTo($"user/{currentEmployee.Employee.ID}");
        }


        void DeleteFromTask()
        {
            TaskService.RemoveUserFormTask(currentEmployee.Task, currentEmployee.Employee.ID);
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
                        OnClick = (sender, args) => DeleteTask(currentTask)
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
        }

        void MenuOnAppearingHandler(MenuAppearingEventArgs e)
        {
            if (e.Data is TaskWithTaskRole taskModel)
            {
                currentTask = taskModel.Task;
                if (currentTask is null) e.PreventShow = true;
                isForUser = true;
            }
            else if(e.Data is TaskModel)
            {
                currentTask = e.Data as TaskModel;
                if (currentTask is null) e.PreventShow = true;
                isForUser = false;
            }
            else if(e.Data is EmployeeWithTaskRole)
            {
                currentEmployee = e.Data as EmployeeWithTaskRole;
                if (currentEmployee is null) e.PreventShow = true;
            }
            else e.PreventShow = true;
        }
    }
}
