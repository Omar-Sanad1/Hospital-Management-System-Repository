using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Filtering;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryTestController : ControllerBase
    {
        private readonly IGenericRepository<LaboratoryTest> _repo;
        private readonly IMapper _mapper;
        public LaboratoryTestController(IGenericRepository<LaboratoryTest> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("GetAllPagedFiltered")]
        public IActionResult GetAllPagedFiltered([FromQuery] LaboratoryTestFiltering laboratoryTestFiltering, [FromQuery] PaginationParameters paginationParameters)
        {
            var laboratoryTests = _repo.GetAllFiltered(l =>
            // فلترة ب TestType
            (string.IsNullOrEmpty(laboratoryTestFiltering.TestType) || l.TestType == laboratoryTestFiltering.TestType) &&
            // فلترة ب ResultDate
            ((!laboratoryTestFiltering.ResultDate.HasValue) || l.ResultDate == laboratoryTestFiltering.ResultDate) &&
            // فلترة ب Status
            (string.IsNullOrEmpty(laboratoryTestFiltering.Status) || l.Status == laboratoryTestFiltering.Status)
            );

            // Sorting
            laboratoryTests = laboratoryTestFiltering.SortBy?.ToLower() switch
            {
                "testtype" => laboratoryTestFiltering.isDescending
                ? laboratoryTests.OrderByDescending(l=>l.TestType)
                : laboratoryTests.OrderBy(l => l.TestType),

                "patientid" => laboratoryTestFiltering.isDescending
                ? laboratoryTests.OrderByDescending(l => l.ResultDate)
                : laboratoryTests.OrderBy(l => l.ResultDate),

                "totalamount" => laboratoryTestFiltering.isDescending
                ? laboratoryTests.OrderByDescending(l => l.RequestDate)
                : laboratoryTests.OrderBy(l => l.RequestDate),

                _ => laboratoryTests.OrderBy(l => l.ID)
            };

            // Pagination
            var totallaboratoryTests = laboratoryTests.Count();
            var laboratoryTestsPaged = _repo.GetAllPaged(paginationParameters.PageNumber, paginationParameters.PageSize);
            var result = new PaginationResponse<LaboratoryTestToReturnDTO>
                (
                    data: _mapper.Map<IEnumerable<LaboratoryTest>, IEnumerable<LaboratoryTestToReturnDTO>>(laboratoryTests),
                    totalitems: totallaboratoryTests,
                    pageNumber: paginationParameters.PageNumber,
                    pageSize: paginationParameters.PageSize
                );
            return Ok(result);
        }

        [HttpGet("GetByID{id}")]
        public IActionResult GetByID(int id)
        {
            var laboratoryTest = _repo.GetByID(id);
            var laboratoryTestMapping = _mapper.Map<LaboratoryTest, LaboratoryTestToReturnDTO>(laboratoryTest);
            return Ok(laboratoryTestMapping);
        }

        [HttpPost("add")]
        public void Add(LaboratoryTest laboratoryTest)
        {
            _repo.Add(laboratoryTest);
        }

        [HttpPut("update")]
        public void Update(LaboratoryTest laboratoryTest)
        {
            _repo.Update(laboratoryTest);
        }

        [HttpDelete("delete")]
        public void Delete(LaboratoryTest laboratoryTest)
        {
            _repo.Delete(laboratoryTest);
        }
    }
}
