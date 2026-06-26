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
    public class DoctorController : ControllerBase
    {
        private readonly IGenericRepository<Doctor> _repo;
        private readonly IMapper _mapper;
        public DoctorController(IGenericRepository<Doctor> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("GetAllPagedFiltered")]
        public IActionResult GetAllPagedFiltered([FromQuery] DoctorFiltering doctorFiltering, [FromQuery] PaginationParameters paginationParameters)
        {
            var doctors = _repo.GetAllFiltered(d =>
            // فلترة ب FullName
            (string.IsNullOrEmpty(doctorFiltering.FullName) || d.FullName == doctorFiltering.FullName) &&
            // فلترة ب PhoneNumber
            (string.IsNullOrEmpty(doctorFiltering.PhoneNumber) || d .PhoneNumber == doctorFiltering.PhoneNumber) &&
            // فلترة ب Specialization
            (string.IsNullOrEmpty(doctorFiltering.Specialization) || d.Specialization == doctorFiltering.Specialization)&&
            // فلترة ب DepartmentID
            (!doctorFiltering.DepartmentID.HasValue || d.DepartmentID == doctorFiltering.DepartmentID)
            );

            // Sorting
            doctors = doctorFiltering.SortBy?.ToLower() switch
            {
                "fullname" => doctorFiltering.isDescending
                ? doctors.OrderByDescending(d => d.FullName)
                : doctors.OrderBy(d => d.FullName),

                "yearsofexperience" => doctorFiltering.isDescending
                ? doctors.OrderByDescending(d => d.YearsOfExperience)
                : doctors.OrderBy(d => d.YearsOfExperience),

                "hiringdate" => doctorFiltering.isDescending
                ? doctors.OrderByDescending(d => d.HiringDate)
                : doctors.OrderBy(d => d.HiringDate),

                _ => doctors.OrderBy(d => d.ID)
            };

            // Pagination
            var totaldoctors = doctors.Count();
            var doctorsPaged = _repo.GetAllPaged(paginationParameters.PageNumber, paginationParameters.PageSize);
            var result = new PaginationResponse<DoctorToReturnDTO>
                (
                    data: _mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorToReturnDTO>>(doctors),
                    totalitems: totaldoctors,
                    pageNumber: paginationParameters.PageNumber,
                    pageSize: paginationParameters.PageSize
                );
            return Ok(result);
        }

        [HttpGet("GetByID{id}")]
        public IActionResult GetByID(int id)
        {
            var doctor = _repo.GetByID(id);
            var doctorMapping = _mapper.Map<Doctor, DoctorToReturnDTO>(doctor);
            return Ok(doctorMapping);
        }

        [HttpPost("add")]
        public void Add(Doctor doctor)
        {
            _repo.Add(doctor);
        }

        [HttpPut("update")]
        public void Update(Doctor doctor)
        {
            _repo.Update(doctor);
        }

        [HttpDelete("delete")]
        public void Delete(Doctor doctor)
        {
            _repo.Delete(doctor);
        }
    }
}
