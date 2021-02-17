using Microsoft.AspNetCore.Components;
using PIZZA.Models.Task;
using PIZZA.WebAssembly.Api.Services;
using PIZZA.WebAssembly.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class Tasks
    {
        private TaskSearchOptions TaskSearchOptions { get; set; } = new();

        [Inject]
        private ITaskService taskService { get; set; }

        private OrderBy actualOrder = OrderBy.Unordered;
        private bool isAscending = false;
        private bool loading = false;

        private List<TaskModelWithActualStateAndCreator> tasks;
        private string queryString;
        private CancellationTokenSource QueryCancellationTokenSource = new CancellationTokenSource();

        private bool ShowOnlyNotFinished
        {
            get => TaskSearchOptions.ShowOnlyNotFinished;
            set
            {
                TaskSearchOptions.ShowOnlyNotFinished = value;
                _ = Query(QueryString);
            }
        }

        private bool ShowUnassignedToMe
        {
            get => TaskSearchOptions.ShowUnassignedToMe;
            set
            {
                TaskSearchOptions.ShowUnassignedToMe = value;
                _ = Query(QueryString);
            }
        }

        private string QueryString
        {
            get => queryString;
            set
            {
                queryString = value;
                _ = Query(value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await Query("");
        }

        private async Task Query(string query)
        {
            QueryCancellationTokenSource.Cancel();
            QueryCancellationTokenSource = new();
            loading = true;
            StateHasChanged();
            try
            {
                tasks = await taskService.GetTasks(QueryCancellationTokenSource.Token, query, TaskSearchOptions);
                Order(actualOrder, isAscending);
            }
            finally
            {
                loading = false;
                _ = InvokeAsync(StateHasChanged);
            }
        }

        private void ChangeOrder(OrderBy item)
        {
            if (item == actualOrder) isAscending = !isAscending;
            else
            {
                actualOrder = item;
                isAscending = true;
            }
            loading = true;
            StateHasChanged();
            try
            {
                Order(actualOrder, isAscending);
            }
            finally
            {
                loading = false;
                StateHasChanged();
            }
        }

        private void Order(OrderBy item, bool ascending)
        {
            switch (item)
            {
                case OrderBy.Unordered:                   
                    break;
                case OrderBy.Name:
                    if (ascending)
                        tasks = tasks.OrderBy(e => e.Name).ToList();
                    else
                        tasks = tasks.OrderByDescending(e => e.Name).ToList();
                    break;
                case OrderBy.State:
                    if (ascending)
                        tasks = tasks.OrderBy(e => e.TaskState).ToList();
                    else
                        tasks = tasks.OrderByDescending(e => e.TaskState).ToList();
                    break;
                case OrderBy.Priority:
                    if (ascending)
                        tasks = tasks.OrderBy(e => e.Priority).ToList();
                    else
                        tasks = tasks.OrderByDescending(e => e.Priority).ToList();
                    break;
                case OrderBy.Deadline:
                    if (ascending)
                        tasks = tasks.OrderBy(e => e.Deadline).ToList();
                    else
                        tasks = tasks.OrderByDescending(e => e.Deadline).ToList();
                    break;
                default:
                    break;
            }
        }

        private string OrderClass(OrderBy item)
        {
            if (item == actualOrder)
            {
                if (isAscending) return "page__table--asc";

                else return "page__table--desc";

            }
            else return "";
        }

        private enum OrderBy
        {
            Unordered,
            Name,
            State,
            Priority,
            Deadline,
        }
    }
}
