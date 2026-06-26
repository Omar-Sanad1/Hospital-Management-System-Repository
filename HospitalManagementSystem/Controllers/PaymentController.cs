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
    public class PaymentController : ControllerBase
    {
        private readonly IGenericRepository<Payment> _repo;
        private readonly IMapper _mapper;
        public PaymentController(IGenericRepository<Payment> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("GetAllPagedFiltered")]
        public IActionResult GetAllPagedFiltered([FromQuery] PaymentFiltering paymentFiltering , PaginationParameters paginationParameters)
        {
            var payments = _repo.GetAllFiltered(p =>
            // فلترة ب PaymentDate
            (!paymentFiltering.PaymentDate.HasValue || p.PaymentDate == paymentFiltering.PaymentDate)     
            );

            // Sorting
            payments = paymentFiltering.SortBy?.ToLower() switch
            {
                "paymentdate" => paymentFiltering.isDescending
                ? payments.OrderByDescending(p=>p.PaymentDate)
                : payments.OrderBy(p=>p.PaymentDate),

                "paymentsstatus" => paymentFiltering.isDescending
                ? payments.OrderByDescending(p=>p.PaymentStatus)
                : payments.OrderBy(p=>p.PaymentStatus),

                _ => payments.OrderBy(p=>p.ID)
            };

            // Pagination
            var totalPayments = payments.Count();
            var paymentsPaged = _repo.GetAllPaged(paginationParameters.PageNumber , paginationParameters.PageSize);
            var result = new PaginationResponse<PaymentToReturnDTO>
                (
                    data:_mapper.Map<IEnumerable<Payment> , IEnumerable<PaymentToReturnDTO>>(payments),
                    totalitems:totalPayments,
                    pageNumber:paginationParameters.PageNumber,
                    pageSize:paginationParameters.PageSize
                );
            return Ok(result);
        }

        [HttpGet("GetByID{id}")]
        public IActionResult GetByID(int id)
        {
            var payment = _repo.GetByID(id);
            var paymentMapping = _mapper.Map<Payment, PaymentToReturnDTO>(payment);
            return Ok(paymentMapping);
        }

        [HttpPost("add")]
        public void Add(Payment payment)
        {
            _repo.Add(payment);
        }

        [HttpPut("update")]
        public void Update(Payment payment)
        {
            _repo.Update(payment);
        }

        [HttpDelete("delete")]
        public void Delete(Payment payment)
        {
            _repo.Delete(payment);
        }
    }
}
