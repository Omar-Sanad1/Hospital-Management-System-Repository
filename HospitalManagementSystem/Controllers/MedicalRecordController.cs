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
    public class MedicalRecordController : ControllerBase
    {
        private readonly IGenericRepository<MedicalRecord> _repo;
        private readonly IMapper _mapper;
        public MedicalRecordController(IGenericRepository<MedicalRecord> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("GetAllPagedFiltered")]
        public IActionResult GetAllPagedFiltered([FromQuery] MedicalRecordFiltering medicalRecordFiltering, [FromQuery] PaginationParameters paginationParameters)
        {
            var medicalRecords = _repo.GetAllFiltered(m =>
            // فلترة ب PatientID
            ((!medicalRecordFiltering.PatientID.HasValue) || m.PatientID == medicalRecordFiltering.PatientID) &&
            // فلترة ب DoctorID
            ((!medicalRecordFiltering.DoctorID.HasValue) || m.DoctorID == medicalRecordFiltering.DoctorID) &&
            // فلترة ب VisitDate
            ((!medicalRecordFiltering.VisitDate.HasValue) || m.VisitDate == medicalRecordFiltering.VisitDate)
            );

            // Sorting
            medicalRecords = medicalRecordFiltering.SortBy?.ToLower() switch
            {
                "visitdate" => medicalRecordFiltering.isDescending
                ? medicalRecords.OrderByDescending(m => m.VisitDate)
                : medicalRecords.OrderBy(m => m.VisitDate),

                _ => medicalRecords.OrderBy(m => m.ID)
            };

            // Pagination
            var totalmedicalRecords = medicalRecords.Count();
            var medicalRecordsPaged = _repo.GetAllPaged(paginationParameters.PageNumber, paginationParameters.PageSize);
            var result = new PaginationResponse<MedicalRecordToReturnDTO>
                (
                    data: _mapper.Map<IEnumerable<MedicalRecord>, IEnumerable<MedicalRecordToReturnDTO>>(medicalRecords),
                    totalitems: totalmedicalRecords,
                    pageNumber: paginationParameters.PageNumber,
                    pageSize: paginationParameters.PageSize
                );
            return Ok(result);
        }

        [HttpGet("GetByID{id}")]
        public IActionResult GetByID(int id)
        {
            var medicalRecord = _repo.GetByID(id);
            var medicalRecordMapping = _mapper.Map<MedicalRecord, MedicalRecordToReturnDTO>(medicalRecord);
            return Ok(medicalRecordMapping);
        }

        [HttpPost("add")]
        public void Add(MedicalRecord medicalRecord)
        {
            _repo.Add(medicalRecord);
        }

        [HttpPut("update")]
        public void Update(MedicalRecord medicalRecord)
        {
            _repo.Update(medicalRecord);
        }

        [HttpDelete("delete")]
        public void Delete(MedicalRecord medicalRecord)
        {
            _repo.Delete(medicalRecord);
        }
    }
}
