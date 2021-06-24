using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PragimTech.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{

    public class EditEmployeeBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public string PageHeader { get; set; }

        //protected async override Task OnInitializedAsync()
        //{
        //    int.TryParse(Id, out int employeeId);

        //    if (employeeId != 0)
        //    {
        //        PageHeader = "Edit Employee";
        //        Employee = await EmployeeService.GetEmployee(int.Parse(Id));
        //    }
        //    else
        //    {
        //        PageHeader = "Create Employee";
        //        Employee = new Employee
        //        {
        //            DepartmentId = 1,
        //            DateOfBirth = DateTime.Now,
        //            PhotoPath = "images/nophoto.jpg"
        //        };
        //    }
        //}
        private Employee Employee { get; set; } = new Employee();

        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();


        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/editEmployee/{Id}");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            int.TryParse(Id, out int employeeId);

            if (employeeId != 0)
            {
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg"
                };
            }
            Departments = (await DepartmentService.GetDepartments()).ToList();

            Mapper.Map(Employee, EditEmployeeModel);

            //EditEmployeeModel.EmployeeId = Employee.EmployeeId;
           // EditEmployeeModel.FirstName = Employee.FirstName;
            //EditEmployeeModel.LastName = Employee.LastName;
          //  EditEmployeeModel.Email = Employee.Email;
            //EditEmployeeModel.ConfirmEmail = Employee.Email;
            //EditEmployeeModel.DateOfBirth = Employee.DateOfBirth;
            //EditEmployeeModel.Gender = Employee.Gender;
            //EditEmployeeModel.PhotoPath = Employee.PhotoPath;
            //EditEmployeeModel.DepartmentId = Employee.DepartmentId;
            //EditEmployeeModel.Department = Employee.Department;
        }
        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);

            Employee result = null;

            if (Employee.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                result = await EmployeeService.CreateEmployee(Employee);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployeeService.DeleteEmployee(Employee.EmployeeId);
                NavigationManager.NavigateTo("/");
            }
        }
    }
}