using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Service.Interfaces;
using Service.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HospitalManagementSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        public DepartmentService(HospitalManagementSystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _dbContext.Departments.ToListAsync();

            return _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentToReturnDTO>>(departments);
        }

        public async Task<DepartmentToReturnDTO> GetDepartmentByIDAsync(int departmentId)
        {
            var specifiedDepartment = await _dbContext.Departments.FirstOrDefaultAsync(d=>d.ID == departmentId);
            if (specifiedDepartment is null)
                throw new ValidationException("This department id isn't exist.");

            return _mapper.Map<Department, DepartmentToReturnDTO>(specifiedDepartment);
        }

        public IEnumerable<DepartmentToReturnDTO> GetAllDepartmentsFiltered(Func<Department, bool> Filter)
        {
            var departmentsFiltered = _dbContext.Set<Department>()
                                      .Where(Filter)
                                      .ToList();

            return _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentToReturnDTO>>(departmentsFiltered);
        }

        public async Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsPagedAsync(int pageNumber, int pageSize)
        {
            var departmentsPaged = await _dbContext.Set<Department>()
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();
          
            return _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentToReturnDTO>>(departmentsPaged);
        }
        public async Task<DepartmentToReturnDTO> AddNewDepartmentAsync(AddNewDepartmentModel addNewDepartment)
        {
            var department = new Department
            {
                Name = addNewDepartment.Name,
                Location = addNewDepartment.Location,
                OperationalStatus = addNewDepartment.OperationalStatus,
                Description = addNewDepartment.Description
            };

            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Department , DepartmentToReturnDTO>(department);
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var specifiedDepartment = await _dbContext.Departments.FirstOrDefaultAsync(d => d.ID == departmentId);
            if (specifiedDepartment is null)
                throw new ValidationException("This department id isn't exist.");

            _dbContext.Departments.Remove(specifiedDepartment);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<DepartmentToReturnDTO> UpdateDepartmentInformationAsync(int departmentId, UpdateDepartmentInformation updateDepartmentInformation)
        {
            var specifiedDepartment = await _dbContext.Departments.FirstOrDefaultAsync(d => d.ID == departmentId);
            if (specifiedDepartment is null)
                throw new ValidationException("This department id isn't exist.");

            specifiedDepartment.Name = updateDepartmentInformation.Name;
            specifiedDepartment.Location = updateDepartmentInformation.Location;
            specifiedDepartment.Description = updateDepartmentInformation.Description;
            specifiedDepartment.OperationalStatus = updateDepartmentInformation.OperationalStatus;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Department, DepartmentToReturnDTO>(specifiedDepartment);
        }
    }
}
