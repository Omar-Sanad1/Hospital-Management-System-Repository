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
using Service.Models.BillModels;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAllBillsPagedFiltered")]
        public async Task<IActionResult> GetAllBillsPagedFilteredAsync([FromQuery] BillFiltering billFiltering , [FromQuery]PaginationParameters paginationParameters)
        {
            var bills = _billService.GetAllBillsFiltered(b =>
            // فلترة ب BillDate
            (!billFiltering.BillDate.HasValue || b.BillDate == billFiltering.BillDate)&&
            // فلترة ب PatientID
            (!billFiltering.PatientID.HasValue || b.PatientID == billFiltering.PatientID) 
            );

            // Sorting
            bills = billFiltering.SortBy?.ToLower() switch
            {
                "billdate" => billFiltering.isDescending
                ? bills.OrderByDescending(b => b.BillDate)
                : bills.OrderBy(b => b.BillDate),

                "totalamount" => billFiltering.isDescending
                ? bills.OrderByDescending(b => b.TotalAmount)
                : bills.OrderBy(b => b.TotalAmount),

                _ => bills.OrderBy(b => b.ID)
            };

            // Pagination
            var totalbills = bills.Count();
            var billsPaged = await _billService.GetAllBillsPagedAsync(paginationParameters.PageNumber , paginationParameters.PageSize);
            var result = new PaginationResponse<BillToReturnDTO>
                (
                    data:bills,
                    totalitems:totalbills,
                    pageNumber:paginationParameters.PageNumber,
                    pageSize:paginationParameters.PageSize
                );
            return Ok(result);
        }



        [HttpGet("GetBillByID{id}")]
        public async Task<IActionResult> GetBillByIDAsync(int id)
        {
            var bill = await _billService.GetBillByIDAsync(id);
            return Ok(bill);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost("CreateNewBill")]
        public async Task<IActionResult> CreateNewBillAsync(CreateNewBillModel createNewBill)
        {
            var createdBill = await _billService.CreateNewBillAsync(createNewBill);
            return Ok(createdBill);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPut("UpdateBillInformation")]
        public async Task<IActionResult> UpdateBillInformationAsync(int billId, UpdateBillInformationModel updateBillInformation)
        {
            var updatedBill = await _billService.UpdateBillInformationAsync(billId , updateBillInformation);
            return Ok(updatedBill);
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("DeleteBill")]
        public async Task DeleteBillAsync(int billId)
        {
            await _billService.DeleteBillAsync(billId);
        }

    }
}
