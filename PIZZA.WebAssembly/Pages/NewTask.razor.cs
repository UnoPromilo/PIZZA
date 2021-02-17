using Microsoft.AspNetCore.Components;
using PIZZA.Models.Task;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using PIZZA.WebAssembly.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class NewTask
    {
        public CreateTaskModel CreateTask { get; set; } = new()
        {
            Deadline = DateTime.Now.AddDays(5),
            Note = "Przygoda została rozpoczęta"
        };

        [Inject]
        private ITaskService TaskService { get; set; }

        [Inject]
        private PopupService PopupService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private async Task CreateTaskFromModel()
        {
            var response = await TaskService.Create(CreateTask);
            if (response.Successful)
            {
                Console.WriteLine($"Task created, ID: {response.ID}");
                NavigationManager.NavigateTo($"/task/{response.ID}");
            }
            else
            {
                PopupAlertModel model = new()
                {
                    Title = "BŁĄD",
                    ContentAsString = "<p>Wystąpił problem podczas dodawania zadania.",
                    Buttons = new List<PopupButton>
                    {
                        new PopupButton
                        {
                            Content = "OK",
                        },
                    }
                };
                PopupService.AddPopupModelToStack(model);

            }
        }
    }
}
