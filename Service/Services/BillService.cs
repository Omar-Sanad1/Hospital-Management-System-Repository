using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Service.Interfaces;
using Service.Models.BillModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BillService : IBillService
    {
        private readonly HospitalManagementSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        public BillService(HospitalManagementSystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<BillToReturnDTO>> GetAllBillsAsync()
        {
            var bills = await _dbContext.Bills.ToListAsync();

            return _mapper.Map<IEnumerable<Bill>, IEnumerable<BillToReturnDTO>>(bills);
        }

        public IEnumerable<BillToReturnDTO> GetAllBillsFiltered(Func<Bill, bool> Filter)
        {
            var billsFiltered = _dbContext.Set<Bill>()
                                .Where(Filter)
                                .ToList();

            return _mapper.Map<IEnumerable<Bill>, IEnumerable<BillToReturnDTO>>(billsFiltered);
        }

        public async Task<IEnumerable<BillToReturnDTO>> GetAllBillsPagedAsync(int pageNumber, int pageSize)
        {
            var billsPaged = await _dbContext.Set<Bill>()
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();

            return _mapper.Map<IEnumerable<Bill>, IEnumerable<BillToReturnDTO>>(billsPaged);
        }

        public async Task<BillToReturnDTO> GetBillByIDAsync(int billId)
        {
            var specifiedBill = await _dbContext.Bills.FirstOrDefaultAsync(b=>b.ID == billId);
            if (specifiedBill is null)
                throw new ValidationException("This bill id isn't exist.");

            return _mapper.Map<Bill, BillToReturnDTO>(specifiedBill);
        }

        public async Task<BillToReturnDTO> CreateNewBillAsync(CreateNewBillModel createNewBill)
        {
            var existsPatient = await _dbContext.Patients.FindAsync(createNewBill.PatientID);
            if (existsPatient is null)
                throw new ValidationException("This patient isn't avilable.");

            var bill = new Bill
            {
                BillDate = DateTime.Now,
                DueDate = createNewBill.DueDate,
                TotalAmount = createNewBill.TotalAmount,
                PaymentStatus = createNewBill.PaymentStatus,
                PatientID = createNewBill.PatientID
            };

            await _dbContext.Bills.AddAsync(bill);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Bill, BillToReturnDTO>(bill);
        }


        public async Task DeleteBillAsync(int billId)
        {
            var specifiedBill = await _dbContext.Bills.FirstOrDefaultAsync(b => b.ID == billId);
            if (specifiedBill is null)
                throw new ValidationException("This bill isn't available");

            _dbContext.Bills.Remove(specifiedBill);
        }


        public async Task<BillToReturnDTO> UpdateBillInformationAsync(int billId, UpdateBillInformationModel updateBillInformation)
        {
            var specifiedBill = await _dbContext.Bills.FirstOrDefaultAsync(b => b.ID == billId);
            if (specifiedBill is null)
                throw new ValidationException("This bill isn't available");

            specifiedBill.DueDate = updateBillInformation.DueDate;
            specifiedBill.PatientID = updateBillInformation.PatientID;
            specifiedBill.PaymentStatus = updateBillInformation.PaymentStatus;
            specifiedBill.TotalAmount = updateBillInformation.TotalAmount;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Bill, BillToReturnDTO>(specifiedBill);
        }
    }
}
