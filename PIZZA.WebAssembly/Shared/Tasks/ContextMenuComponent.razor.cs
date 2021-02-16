using BlazorContextMenu;
using Microsoft.AspNetCore.Components;
using PIZZA.Models.Database;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        void Navigate()
        {
            NavigationManager.NavigateTo($"task/{currentTask.ID}");
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
            currentTask = e.Data as TaskModel;
            if (currentTask is null) e.PreventShow = true;
        }
    }
}
