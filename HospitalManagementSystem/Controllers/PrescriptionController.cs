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
    public class PrescriptionController : ControllerBase
    {
        private readonly IGenericRepository<Prescription> _repo;
        private readonly IMapper _mapper;
        public PrescriptionController(IGenericRepository<Prescription> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("GetAllPagedFiltered")]
        public IActionResult GetAllPagedFiltered([FromQuery] PrescriptionFiltering prescriptionFiltering, [FromQuery] PaginationParameters paginationParameters)
        {
            var prescriptions = _repo.GetAllFiltered(p =>
            // فلترة ب PrescriptionDate
            (!prescriptionFiltering.PrescriptionDate.HasValue || p.PrescriptionDate == prescriptionFiltering.PrescriptionDate) &&
            // فلترة ب PatientID
            (!prescriptionFiltering.PatientID.HasValue || p.PatientID == prescriptionFiltering.PatientID) &&
            // فلترة ب DoctorID
            (!prescriptionFiltering.DoctorID.HasValue || p.DoctorID == prescriptionFiltering.DoctorID) 
            );

            // Sorting
            prescriptions = prescriptionFiltering.SortBy?.ToLower() switch
            {
                "prescriptiondate" => prescriptionFiltering.isDescending
                ? prescriptions.OrderByDescending(p => p.PrescriptionDate)
                : prescriptions.OrderBy(p => p.PrescriptionDate),

                _ => prescriptions.OrderBy(p => p.ID)
            };

            // Pagination
            var totalprescriptions = prescriptions.Count();
            var prescriptionsPaged = _repo.GetAllPaged(paginationParameters.PageNumber, paginationParameters.PageSize);
            var result = new PaginationResponse<PrescriptionToReturnDTO>
                (
                    data: _mapper.Map<IEnumerable<Prescription>, IEnumerable<PrescriptionToReturnDTO>>(prescriptions),
                    totalitems: totalprescriptions,
                    pageNumber: paginationParameters.PageNumber,
                    pageSize: paginationParameters.PageSize
                );
            return Ok(result);
        }

        [HttpGet("GetByID{id}")]
        public IActionResult GetByID(int id)
        {
            var prescription = _repo.GetByID(id);
            var prescriptionMapping = _mapper.Map<Prescription, PrescriptionToReturnDTO>(prescription);
            return Ok(prescriptionMapping);
        }

        [HttpPost("add")]
        public void Add(Prescription prescription)
        {
            _repo.Add(prescription);
        }

        [HttpPut("update")]
        public void Update(Prescription prescription)
        {
            _repo.Update(prescription);
        }

        [HttpDelete("delete")]
        public void Delete(Prescription prescription)
        {
            _repo.Delete(prescription);
        }

    }
}
