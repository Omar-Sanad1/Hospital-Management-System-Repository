using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Filtering;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IGenericRepository<Patient> _repo;
        private readonly IMapper _mapper;
        public PatientController(IGenericRepository<Patient> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("GetAllPagedFiltered")]
        public IActionResult GetAllPagedFiltered([FromQuery] PatientFiltering patientFiltering, [FromQuery] PaginationParameters paginationParameters)
        {
            var patients = _repo.GetAllFiltered(p =>
            // فلترة ب FullName
            (string.IsNullOrEmpty(patientFiltering.FullName) || p.FullName == patientFiltering.FullName) &&
            // فلترة ب PhoneNumber
            (string.IsNullOrEmpty(patientFiltering.PhoneNumber) || p.PhoneNumber == patientFiltering.PhoneNumber) &&
            // فلترة ب Gender
            (string.IsNullOrEmpty(patientFiltering.Gender) || p.Gender == patientFiltering.Gender) &&
            // فلترة ب Address
            (string.IsNullOrEmpty(patientFiltering.Address) || p.Address == patientFiltering.Address)
            );

            // Sorting
            patients = patientFiltering.SortBy?.ToLower() switch
            {
                "fullname" => patientFiltering.isDescending
                ? patients.OrderByDescending(p=>p.FullName)
                : patients.OrderBy(p => p.FullName),

                "gender" => patientFiltering.isDescending
                ? patients.OrderByDescending(p => p.Gender)
                : patients.OrderBy(p => p.Gender),

                _ => patients.OrderBy(p => p.ID)
            };

            // Pagination
            var totaldepatients = patients.Count();
            var patinetsPaged = _repo.GetAllPaged(paginationParameters.PageNumber, paginationParameters.PageSize);
            var result = new PaginationResponse<PatientToReturnDTO>
                (
                    data: _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientToReturnDTO>>(patients),
                    totalitems: totaldepatients,
                    pageNumber: paginationParameters.PageNumber,
                    pageSize: paginationParameters.PageSize
                );
            return Ok(result);
        }

        [HttpGet("GetByID{id}")]
        public IActionResult GetByID(int id)
        {
            var patient = _repo.GetByID(id);
            var patientMapping = _mapper.Map<Patient, PatientToReturnDTO>(patient);
            return Ok(patientMapping);
        }

        [HttpPost("add")]
        public void Add(Patient patient)
        {
            _repo.Add(patient);
        }

        [HttpPut("update")]
        public void Update(Patient patient)
        {
            _repo.Update(patient);
        }

        [HttpDelete("delete")]
        public void Delete(Patient patient)
        {
            _repo.Delete(patient);
        }
    }
}
