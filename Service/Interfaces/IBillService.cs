using Core.DTOs;
using Core.Entities;
using Service.Models.AppointmentModels;
using Service.Models.BillModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBillService
    {
        public Task<IEnumerable<BillToReturnDTO>> GetAllBillsAsync();
        public Task<IEnumerable<BillToReturnDTO>> GetAllBillsPagedAsync(int pageNumber, int pageSize);
        public IEnumerable<BillToReturnDTO> GetAllBillsFiltered(Func<Bill, bool> Filter);
        public Task<BillToReturnDTO> GetBillByIDAsync(int billId);
        public Task<BillToReturnDTO> CreateNewBillAsync(CreateNewBillModel createNewBill);
        public Task<BillToReturnDTO> UpdateBillInformationAsync(int billId , UpdateBillInformationModel updateBillInformation);
        public Task DeleteBillAsync(int billId);
    }
}
