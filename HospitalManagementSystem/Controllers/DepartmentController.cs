using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Filtering;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models.DepartmentModels;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IGenericRepository<Department> _repo;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IGenericRepository<Department> repo, IMapper mapper,IDepartmentService departmentService)
        {
            _repo = repo;
            _mapper = mapper;
            _departmentService = departmentService;
        }

        [HttpGet("GetAllDepartmentsPagedFiltered")]
        public async Task<IActionResult> GetAllDepartmentsPagedFilteredAsync([FromQuery] DepartmentFiltering departmentFiltering, [FromQuery] PaginationParameters paginationParameters)
        {
            var departments = _departmentService.GetAllDepartmentsFiltered(d =>
            // فلترة ب Name
            (string.IsNullOrEmpty(departmentFiltering.Name) || ((Department)(object)d).Name == departmentFiltering.Name)&&
            // فلترة ب Location
            (string.IsNullOrEmpty(departmentFiltering.Location) || ((Department)(object)d).Location == departmentFiltering.Location)&&
            // فلترة ب OperationalStatus
            (string.IsNullOrEmpty(departmentFiltering.OperationalStatus) || ((Department)(object)d).OperationalStatus == departmentFiltering.OperationalStatus)
            );

            // Sorting
            departments = departmentFiltering.SortBy?.ToLower() switch
            {
                "name" => departmentFiltering.isDescending
                ? departments.OrderByDescending(d => d.Name)
                : departments.OrderBy(d => d.Name),

                "location" => departmentFiltering.isDescending
                ? departments.OrderByDescending(d => d.Location)
                : departments.OrderBy(d => d.Location),

                "operationalstatus" => departmentFiltering.isDescending
                ? departments.OrderByDescending(d => d.OperationalStatus)
                : departments.OrderBy(d => d.OperationalStatus),

                _ => departments.OrderBy(d => d.ID)
            };

            // Pagination
            var totaldepartments = departments.Count();
            var departmentsPaged = await _departmentService.GetAllDepartmentsPagedAsync(paginationParameters.PageNumber, paginationParameters.PageSize);
            var result = new PaginationResponse<DepartmentToReturnDTO>
                (
                    data: departments,
                    totalitems: totaldepartments,
                    pageNumber: paginationParameters.PageNumber,
                    pageSize: paginationParameters.PageSize
                );
            return Ok(result);
        }

        [HttpGet("GetDepartmentByID{id}")]
        public async Task<IActionResult> GetDepartmentByIDAsync(int departmentId)
        {
            var department = await _departmentService.GetDepartmentByIDAsync(departmentId);
            return Ok(department);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost("AddNewDepartment")]
        public async Task<IActionResult> AddNewDepartmentAsync(AddNewDepartmentModel addNewDepartment)
        {
            var addedDepartment = await _departmentService.AddNewDepartmentAsync(addNewDepartment);
            return Ok(addedDepartment);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPut("UpdateDepartmentInformation")]
        public async Task<IActionResult> UpdateDepartmentInformationAsync(int departmentId, UpdateDepartmentInformation updateDepartmentInformation)
        {
            var updatedDepartment = await _departmentService.UpdateDepartmentInformationAsync(departmentId, updateDepartmentInformation);
            return Ok(updatedDepartment);
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("DeleteDepartment")]
        public async Task DeleteDepartmentAsync(int departmentId)
        {
            await _departmentService.DeleteDepartmentAsync(departmentId);
        }
    }
}
