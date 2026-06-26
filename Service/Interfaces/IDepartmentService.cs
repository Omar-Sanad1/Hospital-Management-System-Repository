using Core.DTOs;
using Core.Entities;
using Service.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync();
        public Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsPagedAsync(int pageNumber , int pageSize);
        public IEnumerable<DepartmentToReturnDTO> GetAllDepartmentsFiltered(Func<Department,bool> Filter);
        public Task<DepartmentToReturnDTO> GetDepartmentByIDAsync(int departmentId);
        public Task<DepartmentToReturnDTO> AddNewDepartmentAsync(AddNewDepartmentModel addNewDepartment);
        public Task<DepartmentToReturnDTO> UpdateDepartmentInformationAsync(int departmentId , UpdateDepartmentInformation updateDepartmentInformation);
        public Task DeleteDepartmentAsync(int departmentId);


    }
}
