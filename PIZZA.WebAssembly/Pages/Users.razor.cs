using Microsoft.AspNetCore.Components;
using PIZZA.Models.User;
using PIZZA.WebAssembly.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System;

namespace PIZZA.WebAssembly.Pages
{
    public partial class Users
    {
        [Inject]
        private IEmployeeService employeeService { get; set; }

        private OrderBy actualOrder = OrderBy.Unordered;
        private bool isAscending = false;
        private bool loading = false;

        private List<EmployeeModel> employees;
        private string queryString;

        private CancellationTokenSource QueryCancellationTokenSource =new CancellationTokenSource();

        private string QueryString
        {
            get => queryString;
            set
            {
                queryString = value;
                _=Query(value);
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
                employees = await employeeService.GetEmployees(QueryCancellationTokenSource.Token, query);
                //Order(actualOrder, isAscending);
            }
            finally
            {
                loading = false;
                _=InvokeAsync(StateHasChanged);
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
                        employees = employees.OrderBy(e => e.FirstName + e.FirstName).ToList();
                    else
                        employees = employees.OrderByDescending(e => e.FirstName + e.FirstName).ToList();
                    break;
                case OrderBy.Email:
                    if (ascending)
                        employees = employees.OrderBy(e => e.Email).ToList();
                    else
                        employees = employees.OrderByDescending(e => e.Email).ToList();
                    break;
                case OrderBy.Phone:
                    if (ascending)
                        employees = employees.OrderBy(e => e.PhoneNumber).ToList();
                    else
                        employees = employees.OrderByDescending(e => e.PhoneNumber).ToList();
                    break;
                case OrderBy.Admin:
                    if (ascending)
                        employees = employees.OrderBy(e =>
                                                        e.Roles.Where(r => r?.Equals("ADMIN", StringComparison.OrdinalIgnoreCase)??false).Count() > 0)
                                                        .ToList();
                    else
                        employees = employees.OrderByDescending(e =>
                                                        e.Roles.Where(r => r?.Equals("ADMIN", StringComparison.OrdinalIgnoreCase)??false).Count() > 0)
                                                        .ToList();
                    break;
                case OrderBy.Manager:
                    if (ascending)
                        employees = employees.OrderBy(e =>
                                                        e.Roles.Where(r => r?.Equals("MANAGER", StringComparison.OrdinalIgnoreCase)??false).Count() > 0)
                                                        .ToList();
                    else
                        employees = employees.OrderByDescending(e =>
                                                        e.Roles.Where(r => r?.Equals("MANAGER", StringComparison.OrdinalIgnoreCase)??false).Count() > 0)
                                                        .ToList();
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
            Email,
            Phone,
            Admin,
            Manager,
        }

    }
}
