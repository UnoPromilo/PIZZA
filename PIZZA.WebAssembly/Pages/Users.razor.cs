using Microsoft.AspNetCore.Components;
using PIZZA.Models.User;
using PIZZA.WebAssembly.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PIZZA.WebAssembly.Pages
{
    public partial class Users
    {
        [Inject]
        private IEmployeeService employeeService { get; set; }

        private OrderBy actualOrder = OrderBy.Unordered;
        private bool isAscending = false;
        private bool sorting = false;

        private List<EmployeeModel> employees;
        protected override async Task OnInitializedAsync()
        {
            employees = await employeeService.GetEmployees();
        }

        private void ChangeOrder(OrderBy item)
        {
            if (item == actualOrder) isAscending = !isAscending;
            else
            {
                actualOrder = item;
                isAscending = true;
            }
            sorting = true;
            StateHasChanged();
            try
            {
                switch (item)
                {
                    case OrderBy.Unordered:
                        break;
                    case OrderBy.Name:
                        if (isAscending)
                            employees = employees.OrderBy(e => e.FirstName + e.FirstName).ToList();
                        else
                            employees = employees.OrderByDescending(e => e.FirstName + e.FirstName).ToList();
                        break;
                    case OrderBy.Email:
                        if (isAscending)
                            employees = employees.OrderBy(e => e.Email).ToList();
                        else
                            employees = employees.OrderByDescending(e => e.Email).ToList();
                        break;
                    case OrderBy.Phone:
                        if (isAscending)
                            employees = employees.OrderBy(e => e.PhoneNumber).ToList();
                        else
                            employees = employees.OrderByDescending(e => e.PhoneNumber).ToList();
                        break;
                    case OrderBy.Admin:
                        if (isAscending)
                            employees = employees.OrderBy(e =>
                                                            e.Roles.Where(r => r.NormalizedName == "ADMIN").Count() > 0)
                                                            .ToList();
                        else
                            employees = employees.OrderBy(e =>
                                                            e.Roles.Where(r => r.NormalizedName == "ADMIN").Count() > 0)
                                                            .ToList();
                        break;
                    case OrderBy.Manager:
                        if (isAscending)
                            employees = employees.OrderBy(e =>
                                                            e.Roles.Where(r => r.NormalizedName == "MANAGER").Count() > 0)
                                                            .ToList();
                        else
                            employees = employees.OrderBy(e =>
                                                            e.Roles.Where(r => r.NormalizedName == "MANAGER").Count() > 0)
                                                            .ToList();
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                sorting = false;
                StateHasChanged();
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
